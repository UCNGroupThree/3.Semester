using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ReservationService : IReservationService {

        private List<Flight> flights;
        private int noOfSeats = -1;
        //private readonly FlightDB db = new FlightDB();
        private Ticket ticket;

        public ReservationService() {
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            //db.Database.Log = m => Debug.WriteLine(m);
        }
        /*
        static ReservationService() {
            var thread = new Thread(LookForDeadTickets);
            //thread.Start();
        }

        private static void LookForDeadTickets() {
            while (true) {
                Debug.WriteLine("Ping");
                Debug.WriteLine("Pong");
                Thread.Sleep(10000);
            }
        }  
        */
        private void InstanceContext_Closed(object sender, EventArgs e) {
            // Session closed here
            DeleteTicket(false);
            Debug.WriteLine("End session: {0}, {1}", sender, e);
        }

        private void DeleteTicket(bool throwException) {
            try {
                using (var db = new FlightDB()) {
                    db.Database.Log = m => Debug.WriteLine(m);

                    if (ticket != null && ticket.ID != 0) {
                        db.Tickets.Attach(ticket);
                        db.Tickets.Remove(ticket);
                        //DetectChanges(db);
                        db.SaveChanges();
                        RemoveInDijkstra();
                        //DebugSaveChanges();

                        noOfSeats = -1;
                        flights = null;
                        ticket = null;
                    }
                }
                Debug.WriteLine("Deleted");
            } catch (Exception ex) {
                if (throwException) {
                    throw;
                }
                Debug.WriteLine("DeleteTicket: ignored exception of type: " + ex);
            }
        }



        public List<Flight> GetFlightsAsd(int fromId, int toId, int seats, DateTime dateTime) {
            try {
                //TODO måske tjek på om flights er tom?
                CreateTicket();
                //DeleteTicket(false);
                //TODO måske byttes om, men vær opmærksom på flight = null i CreateTicket
                flights = new Dijkstra().DijkstraStuff(fromId, toId, seats, dateTime);


                //List<Flight> flights1;
                //    using (var tempDb = new FlightDB()) {
                //        tempDb.Database.Log = m => Debug.WriteLine(m);
                //        flights = tempDb.Flights.Where(x => x.ID == 228 || x.ID == 229)
                //            .Include(x => x.Route.From)
                //            .Include(x => x.Route.To)
                //            .Include(x=> x.Plane)
                //            .ToList();
                //    }
                //flights = new List<Flight>();

                //flights = flights1;


                noOfSeats = seats;

                return flights;
            } catch (Exception ex) {
                //TODO FejlHåndtering
                throw new FaultException<DatabaseFault>(new DatabaseFault("GetFlightsAsd Error"));
                //throw;
            }
            //return null;
        }

        private void CreateTicket() {
            if (ticket != null) {
                DeleteTicket(false);
            }
            ticket = new Ticket { OrderDate = DateTime.UtcNow, OrderState = TicketState.Pending };
        }


        
        public Ticket MakeSeatsOccupiedRandom() {
            /*if (ticket == null) {
                CreateTicket(); //TODO fjernes!
            }*/
            //noOfSeats = 1;
            //flights = new List<Flight>();

            if (ticket == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault("Run GetFlightsAsd first", "ticket"));
            }
            // ReSharper disable once PossibleNullReferenceException
            if (flights == null && flights.Count == 0) {
                throw new FaultException<NullPointerFault>(new NullPointerFault("flights is not valid", "flights"));
            }
            if (noOfSeats < 1) {
                throw new FaultException<NullPointerFault>(new NullPointerFault("noOfSeats is not valid",
                    "noOfSeats"), new FaultReason("noOfSeats is not valid"));
            }
            OperationContext.Current.InstanceContext.Closed -= InstanceContext_Closed;
            OperationContext.Current.InstanceContext.Closed += InstanceContext_Closed;

            List<SeatReservation> oldSeatReservations = ticket.SeatReservations;
            try {
                using (var db = new FlightDB()) {
                    db.Database.Log = m => Debug.WriteLine(m);

                    ticket.SeatReservations = GetRandomSeatReservations(flights, noOfSeats);

                    if (ticket.ID == 0) {
                        db.Tickets.Add(ticket);
                    } else {
                        var existingSeatRes = db.SeatReservations.Where(x => x.Ticket.ID == ticket.ID).ToList();
                        db.SeatReservations.RemoveRange(existingSeatRes);

                        foreach (var sr in ticket.SeatReservations) {
                            db.Entry(sr).State = EntityState.Added;
                        }
                        db.Tickets.Attach(ticket);
                        db.Entry(ticket).State = EntityState.Unchanged;
                    }

                    db.SaveChanges();

                    UpdateDijkstra();

                }
            } catch (Exception ex) {
                ticket.SeatReservations = oldSeatReservations;
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                var detail = ex as ArgumentException;
                if (detail != null) {
                    throw new FaultException<ArgumentFault>(new ArgumentFault(detail));
                }
                var detail2 = ex as NotEnouthException;
                if (detail2 != null) {
                    throw new FaultException<NotEnouthFault>(new NotEnouthFault(detail2));
                }
                throw new FaultException<DatabaseFault>(new DatabaseFault("MakeSeatOccupiedRandom Error"));
            }

            return ticket;
        }

        public void Cancel() {
            DeleteTicket(false);
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotEnouthException"></exception>
        private List<SeatReservation> GetRandomSeatReservations(List<Flight> flights2, int noOfSeats) {

            List<SeatReservation> ret = new List<SeatReservation>();

            foreach (var f in flights2) {
                using (var db = new FlightDB()) {
                    db.Configuration.LazyLoadingEnabled = false;
                    IQueryable<Seat> freeSeats =
                        db.Seats.Where(x =>
                            x.Plane.Flights.Any(y => y.ID == f.ID) &&
                            !x.SeatReservations.Any(sr => sr.Seat.ID == x.ID && sr.Flight.ID == f.ID));
                    //.Include(x => x.Plane);

                    List<Seat> seatsToRes = freeSeats.OrderBy(s => Guid.NewGuid()).Take(noOfSeats).ToList();
                    if (seatsToRes.Count() < noOfSeats) {
                        //Debug.WriteLine("Not Enough seats free");
                        throw new NotEnouthException("Not Enough seats free");
                    }

                    foreach (var s in seatsToRes) {
                        //Debug.WriteLine("inside seat loop"); //(ticket, SeatState.Occupied, s, f)
                        //SeatReservation seatRes = new SeatReservation(ticket, SeatState.Occupied, s, f) {Seat_ID = s.ID, Flight_ID = f.ID };
                        //SeatReservation seatRes = new SeatReservation { Flight = f, Flight_ID = f.ID, Seat = s, Seat_ID = s.ID, State = SeatState.Occupied };
                        SeatReservation seatRes = new SeatReservation { Ticket = ticket, Flight_ID = f.ID, Seat_ID = s.ID, State = SeatState.Occupied };
                        //TODO Ticket..
                        Debug.WriteLine("seatRes: flight: {0} Seat: {1} State: {2}", seatRes.Flight_ID, seatRes.Seat_ID,
                            seatRes.State);
                        ret.Add(seatRes);
                    }
                }
            }
            return ret;
        }

        public void Complete() {
            Debug.WriteLine("Completed started!");
            try {
                if (ticket.OrderState != TicketState.Pending) {
                    throw new FaultException<AlreadyExistFault>(new AlreadyExistFault("Ticket is already completed"));
                }
                if (ticket.SeatReservations.Count == 0) {
                    throw new Exception();
                }
            } catch (Exception ex) {
                var detail = ex as AlreadyExistException;
                if (detail != null) {
                    throw new FaultException<AlreadyExistFault>(new AlreadyExistFault(detail));
                }
                throw new FaultException<ArgumentFault>(new ArgumentFault("Complete is not ready to be executed!"));
            }
            try {
                using (var db = new FlightDB()) {
                    db.Database.Log = s => Debug.WriteLine(s);
                    ticket.SeatReservations.ForEach(reservation => {
                        reservation.State = SeatState.Taken;
                        //db.SeatReservations.Attach(reservation);
                    });
                    ticket.OrderState = TicketState.Ordered;
                    db.Tickets.Attach(ticket);
                    
                    db.Entry(ticket).State = EntityState.Modified;
                    ticket.SeatReservations.ForEach(s => db.Entry(s).State = EntityState.Modified);
                    
                    db.SaveChanges();
                    
                    OperationContext.Current.InstanceContext.Closed -= InstanceContext_Closed;
                }
            } catch (Exception ex) {
                throw new FaultException<DatabaseFault>(new DatabaseFault("Complete Error: " + ex.Message));
            }

            Debug.WriteLine("Completed ended!");
        }
        
        private void RemoveInDijkstra() {
            HashSet<int> ids = ticket.SeatReservations.Select(s => s.Flight_ID).ToHashSet();

            Task updateTask = Task.Run(() => {
                foreach (var id in ids) {
                    Dijkstra.Removed(new Flight() { ID = id });
                }
            });
        }

        private void UpdateDijkstra() {
            HashSet<int> ids = ticket.SeatReservations.Select(s => s.Flight_ID).ToHashSet();

            Task updateTask = Task.Run(() => {
                foreach (var id in ids) {
                    Dijkstra.Updated(new Flight() { ID = id });
                }
            });
        }
    }
}