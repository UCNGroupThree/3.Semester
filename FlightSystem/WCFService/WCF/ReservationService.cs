using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
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

        //private List<Flight> flights;
        //private int noOfSeats = -1;
        //private readonly FlightDB db = new FlightDB();
        private Ticket ticket;

        public ReservationService() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        private void InstanceContext_Closed(object sender, EventArgs e) {
            // Session closed here
            DeleteTicket(false);
            Trace.WriteLine(String.Format("End session: {0}, {1}", sender, e));
        }

        private void DeleteTicket(bool throwException) {
            try {
                using (var db = new FlightDB()) {
                    db.Database.Log = m => Trace.WriteLine(m);

                    if (ticket != null && ticket.ID != 0) {
                        var clone = ticket.Clone();

                        db.Tickets.Attach(ticket);
                        db.Tickets.Remove(ticket);
                        //DetectChanges(db);
                        db.SaveChanges();

                        ticket = clone;
                        UpdateDijkstra();

                        ticket = null;
                    }
                }
                Trace.WriteLine("Deleted");
            } catch (Exception ex) {
                if (throwException) {
                    throw;
                }
                Trace.WriteLine("DeleteTicket: ignored exception of type: " + ex);
            }
        }

        /// <exception cref="NullException"></exception>
        private void CreateTicket(User user) {
            if (ticket != null) {
                DeleteTicket(false);
            }
            using (var db = new FlightDB()) {
                db.DebugLog();
                try {
                    user = db.Users.Include(x => x.Postal).Single(u => u.ID == user.ID);
                } catch (Exception) {
                    throw new NullException("User not valid!");
                }
            }
            ticket = new Ticket { OrderDate = DateTime.UtcNow, OrderState = TicketState.Pending, User = user, User_ID = user.ID };
        }

        public Ticket MakeSeatsOccupiedRandom(List<Flight> flights, int noOfSeats, User user) {
            if (ticket == null) {
                try {
                    CreateTicket(user);
                } catch (NullException) {
                    throw new FaultException<NullPointerFault>(
                        new NullPointerFault("user is not valid or not found in database", "user"));
                }
            } else {
                if (ticket.User.ID != user.ID) {
                    throw new FaultException<NotSameObjectFault>(new NotSameObjectFault(new NotSameObjectException("Added user is not the same as first run")), new FaultReason("added user is not the same as first run"));
                }
            }
            if (flights == null || flights.Count == 0) {
                throw new FaultException<NullPointerFault>(new NullPointerFault("flights is not valid", "flights"), new FaultReason("flights is not valid parameter"));
            }
            if (noOfSeats < 1) {
                throw new FaultException<NullPointerFault>(new NullPointerFault("noOfSeats is not valid", "noOfSeats"),new FaultReason("noOfSeats is not valid"));
            }

            OperationContext.Current.InstanceContext.Closed -= InstanceContext_Closed;
            OperationContext.Current.InstanceContext.Closed += InstanceContext_Closed;

            Ticket oldTicket = ticket.Clone();
            try {
                using (var db = new FlightDB()) {

                    flights = GetFlights(flights, db);

                    // ReSharper disable once PossibleNullReferenceException
                    ticket.SeatReservations = GetRandomSeatReservations(flights, noOfSeats, db);

                    if (ticket.ID == 0) {
                        db.Users.Attach(ticket.User);
                        db.Tickets.Add(ticket);

                        db.SaveChanges();
                    } else {
                        var tempUser = ticket.User;
                        ticket.User = null;
                        var existingSeatRes = db.SeatReservations.Where(x => x.Ticket.ID == ticket.ID).ToList();
                        db.SeatReservations.RemoveRange(existingSeatRes);

                        foreach (var sr in ticket.SeatReservations) {
                            db.Entry(sr).State = EntityState.Added;
                        }

                        db.Tickets.Attach(ticket);
                        db.Entry(ticket).State = EntityState.Unchanged;

                        db.SaveChanges();
                        ticket.User = tempUser;
                    }

                    UpdateDijkstra();
                }
            } catch (NotFoundException ex) {
                throw new FaultException<NotFoundFault>(new NotFoundFault(ex), new FaultReason(ex.Message));
            } catch (NotEnouthException ex) {
                throw new FaultException<NotEnouthFault>(new NotEnouthFault(ex), new FaultReason(ex.Message));
            } catch (Exception ex) {
                ticket = oldTicket;
                Trace.WriteLine(ex);
                Trace.WriteLine(ex.Message);
                throw new FaultException<DatabaseFault>(new DatabaseFault("MakeSeatOccupiedRandom Error"), new FaultReason("MakeSeatOccupiedRandom Error"));
            }
            return ticket;
        }

        /// <exception cref="NotFoundException"></exception>
        private List<Flight> GetFlights(List<Flight> flights, FlightDB db) {
            var count = flights.Count;
            //using (var db = new FlightDB()) {
            db.DebugLog();
            var listOfIds = flights.Select(f => f.ID);
            var query = db.Flights
                .Include(f => f.Plane)
                .Include(f => f.Route.From)
                .Include(f => f.Route.To)
                .Where(f => listOfIds.Contains(f.ID));
            var ret = query.ToList();
            var foundCount = ret.Count;
            if (count != foundCount) {
                throw new NotFoundException("Mismatch in requested flights and found flights in database");
            }
            return ret;
            //}
        }

        public void Cancel() {
            DeleteTicket(false);
        }

        /// <exception cref="NotEnouthException"></exception>
        private List<SeatReservation> GetRandomSeatReservations(List<Flight> flights, int noOfSeats, FlightDB db) {
            List<SeatReservation> ret = new List<SeatReservation>();

            foreach (var f in flights) {
                //using (var db = new FlightDB()) {
                IQueryable<Seat> freeSeats =
                    db.Seats.Where(x =>
                        x.Plane.Flights.Any(y => y.ID == f.ID) &&
                        !x.SeatReservations.Any(sr => sr.Seat.ID == x.ID && sr.Flight.ID == f.ID));
                //.Include(x => x.Plane);

                List<Seat> seatsToRes = freeSeats.OrderBy(s => Guid.NewGuid()).Take(noOfSeats).ToList();
                if (seatsToRes.Count() < noOfSeats) {
                    //Trace.WriteLine("Not Enough seats free");
                    throw new NotEnouthException("Not Enough seats free");
                }

                foreach (var s in seatsToRes) {
                    //Trace.WriteLine("inside seat loop"); //(ticket, SeatState.Occupied, s, f)
                    //SeatReservation seatRes = new SeatReservation(ticket, SeatState.Occupied, s, f) {Seat_ID = s.ID, Flight_ID = f.ID };
                    //SeatReservation seatRes = new SeatReservation { Flight = f, Flight_ID = f.ID, Seat = s, Seat_ID = s.ID, State = SeatState.Occupied };
                    SeatReservation seatRes = new SeatReservation { Ticket = ticket, Flight = f, Seat = s, Flight_ID = f.ID, Seat_ID = s.ID, State = SeatState.Occupied, Price = f.Route.Price };
                    //TODO Ticket..
                    Trace.WriteLine(String.Format("seatRes: flight: {0} Seat: {1} State: {2}", seatRes.Flight_ID, seatRes.Seat_ID,
                        seatRes.State));
                    ret.Add(seatRes);
                }
                //}
            }
            return ret;
        }

        public void Complete() {
            Trace.WriteLine("Completed started!");
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

            Trace.WriteLine("Completed ended!");
        }


        private void UpdateDijkstra() {
            HashSet<int> ids = ticket.SeatReservations.Select(s => s.Flight_ID).ToHashSet();

            Task updateTask = Task.Run(() => {
                Parallel.ForEach(ids, (i) => Dijkstra.Updated(new Flight() { ID = i }));
                //foreach (var id in ids) {
                //    Dijkstra.Updated(new Flight() { ID = id });
                //}
            });
        }
    }
}