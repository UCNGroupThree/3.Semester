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
using Common.Exceptions;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ReservationService : IReservationService {

        private List<Flight> flights;
        private int noOfSeats = -1;
        private readonly FlightDB db = new FlightDB();
        private Ticket ticket;

        public ReservationService() {
            OperationContext.Current.InstanceContext.Closed += InstanceContext_Closed;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            db.Database.Log = m => Debug.WriteLine(m);
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
                //using (var db = new FlightDB()) {
                //db.Tickets.Remove(ticket);
                db.Database.Log = m => Debug.WriteLine(m);

                if (ticket != null && ticket.ID != 0) {
                    db.Tickets.Remove(ticket);
                    //db.Tickets.Attach(ticket);
                    //db.Entry(ticket).State = EntityState.Deleted;
                    //db.SaveChanges();
                    DebugSaveChanges();

                    noOfSeats = -1;
                    flights = null;
                    ticket = null;
                }

                Debug.WriteLine("Deleted");
                //}
                /*
                //1. Get student from DB
                List<SeatReservation> tempSeatResList = new List<SeatReservation>();
                using (var db = new FlightDB()) {
                    var SeatIds = seatReservations.Select(x => x.ID);
                    tempSeatResList = db.SeatReservations.Where(x => SeatIds.Contains(x.ID)).ToList();
                }

                //Create new context for disconnected scenario
                using (var newContext = new FlightDB()) {
                    seatReservations.ForEach(reservation => {
                        newContext.SeatReservations.Attach(reservation);
                        newContext.Entry(reservation).State = EntityState.Deleted;
                    });

                    newContext.SaveChanges();
                } */
            } catch (Exception) {
                if (throwException) {
                    throw;
                }
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
            return null;
        }

        private void CreateTicket() {
            if (ticket != null) {
                DeleteTicket(false);
            }
            ticket = new Ticket { OrderDate = DateTime.UtcNow, OrderState = TicketState.Pending };
        }

        public List<SeatReservation> MakeSeatsOccupiedRandom() {

            try {

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
                        "noOfSeats"));
                }

                //flights = new Dijkstra().DijkstraStuff(1, 3, noOfSeats, DateTime.Now);
                //List<Seat> seatList = new List<Seat>();
                //List<SeatReservation> seatResList = null;

                List<SeatReservation> oldSeatReservations = ticket.SeatReservations;
                try {
                    using (var db = new FlightDB()) {
                        db.Database.Log = m => Debug.WriteLine(m);
                        /*List<Flight> flights1 = new List<Flight> {new Flight {ID = 228}, new Flight {ID = 229}};
                            //TODO ændre dette
                        flights1.ForEach(x => flights.Add(db.Flights.Single(f => f.ID == x.ID)));
                    */
                        List<SeatReservation> seatReservations = GetRandomSeatReservations(flights, noOfSeats);

                        /*ticket.SeatReservations.ForEach(reservation => {
                            db.SeatReservations.re
                            //db.SeatReservations.Attach(reservation);
                            //db.Entry(reservation).State = EntityState.Deleted;
                        });
                        */


                        //db.SeatReservations.AddRange(seatReservations);
                        //db.Tickets.Attach(ticket);
                        ticket.SeatReservations = seatReservations;

                        if (ticket.ID == 0) {

                            /*
                            foreach (var sr in ticket.SeatReservations) {
                                //db.SeatReservations.Attach(sr);
                                db.Entry(sr).State = EntityState.Added;
                            }
                            //db.Tickets.Attach(ticket);
                            var entry = db.Entry(ticket);
                            entry.State = EntityState.Added;
                            */
                            //db.Tickets.Attach(ticket);
                            db.Tickets.Add(ticket);
                            /*
                            var entry = db.Entry(ticket);
                            entry.State = EntityState.Added;
                            foreach (var sr in entry.Entity.SeatReservations) {
                                db.Entry(sr.Seat).State = EntityState.Unchanged;
                                db.Entry(sr.Flight).State = EntityState.Unchanged;
                            }*/
                            /*foreach (var sr in entry.Entity.SeatReservations) {
                                foreach (var VARIABLE in sr.) {
                                    
                                }
                            }*/

                            /*foreach (var sr in ticket.SeatReservations) {
                                db.Entry(sr).State = EntityState.Added;
                            }*/


                            //var entry = db.ChangeTracker.Entries<Ticket>().SingleOrDefault();
                            //entry.Entity.SeatReservations.ForEach(x => x.Ticket = entry.Entity);

                            //var entry = db.ChangeTracker.Entries<SeatReservation>().Where(x => x.Entity.Ticket.ID == 0)
                            db.SaveChanges();
                            return ticket.SeatReservations;
                        } else {
                            //db.SeatReservations
                            //Delete existing seatreservations
                            //db.Tickets.Attach(ticket);

                            //db.Entry(ticket).Property(e => e.SeatReservations).IsModified = true;
                            //
                            //db.Entry(ticket).State = EntityState.Modified;

                            var existingSeatRes = db.SeatReservations.Where(x => x.Ticket.ID == ticket.ID).ToList();
                            db.SeatReservations.RemoveRange(existingSeatRes);
                            /*if (existingSeatRes.Any()) {
                                foreach (var sr in existingSeatRes) {
                                
                                    db.Entry(sr).State = EntityState.Deleted;
                                }
                            }*/


                            foreach (var sr in ticket.SeatReservations) {
                                //db.SeatReservations.Attach(sr);
                                db.Entry(sr).State = EntityState.Added;
                            }
                            db.Tickets.Attach(ticket);
                            db.Entry(ticket).State = EntityState.Unchanged;


                            //db.Entry(ticket).State = EntityState.Modified;

                            //mark teacher based on StandardId
                            //foreach (SeatReservation tchr in ticket.SeatReservations)
                            //    db.Entry(tchr).State = tchr.ID == 0 ? EntityState.Added : EntityState.Modified;


                            /*
                            db.SeatReservations.RemoveRange(existingSeatRes);
                            foreach (var sr in existingSeatRes) {
                                if (seatReservations.Any(x => x.Flight_ID == sr.Flight_ID && x.Seat_ID == sr.Seat_ID)) {
                                    db.Entry(sr).State = EntityState.Unchanged;
                                }
                            }*/
                        }
                        /*
                        foreach (var v in db.ChangeTracker.Entries<Seat>().ToList()) {
                            v.State = EntityState.Unchanged;
                        }
                        foreach (var v in db.ChangeTracker.Entries<Flight>().ToList()) {
                            v.State = EntityState.Unchanged;
                        }*/
                        /*
                        foreach (var seat in ticket.SeatReservations.Select(x => x.Seat)) {
                            //db.Seats.Attach(sr.Seat);
                        
                            /*if (db.Seats.Local.All(e => e.ID != seat.ID)) {
                            db.Entry(seat).State = EntityState.Unchanged;
                            } else {
                                var entity = db.ChangeTracker.Entries<Seat>().SingleOrDefault(x => x.Entity.ID == seat.ID);
                                if (entity != null) {
                                    entity.State = EntityState.Unchanged;    
                                }
                            }*/
                        //}
                        /*
                        flights.ForEach(r => {
                            //flights.Add(db.Flights.Find(r));
                            // ReSharper disable once SimplifyLinqExpression
                            if (!db.Flights.Local.Any(e => e.ID == r.ID)) {
                                db.Entry(r).State = EntityState.Unchanged;
                            } else {
                                var entity = db.ChangeTracker.Entries<Flight>().SingleOrDefault(x => x.Entity.ID == r.ID);
                                if (entity != null) {
                                    entity.State = EntityState.Unchanged;
                                    Debug.WriteLine("#####");
                                    Debug.WriteLine("#####");
                                    Debug.WriteLine(entity);
                                    Debug.WriteLine("#####");
                                    Debug.WriteLine("#####");
                                }
                            }
                        });
                        */

                        //Says to EntityFramework that Seats are not new.

                        //DebugSaveChanges();
                        db.ChangeTracker.DetectChanges();
                        var list = db.ChangeTracker.Entries().ToList();
                        foreach (var v in list) {
                            Debug.WriteLine("c: #" + list.IndexOf(v) + " - " +  v.Entity + " state: " + v.State);
                        }
                        //db.SaveChanges();
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
            } catch (Exception ex) {
                Debug.WriteLine("FEJL");
                var t = ex as FaultException<NullPointerFault>;
                if (t != null) {
                    Debug.WriteLine(t);
                    Debug.WriteLine(t.Detail.Message);
                    Debug.WriteLine(t.Detail.ParamenterName);
                }
            }
            return ticket.SeatReservations;
        }

        public void Cancel() {
            DeleteTicket(false);
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotEnouthException"></exception>
        private List<SeatReservation> GetRandomSeatReservations(List<Flight> flights2, int noOfSeats) {

            List<SeatReservation> ret = new List<SeatReservation>();

            foreach (var f in flights2) {
                //using (var db = new FlightDB()) {
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
                    SeatReservation seatRes = new SeatReservation {Ticket = ticket, Flight_ID = f.ID, Seat_ID = s.ID, State = SeatState.Occupied };
                    //TODO Ticket..
                    Debug.WriteLine("seatRes: flight: {0} Seat: {1} State: {2}", seatRes.Flight_ID, seatRes.Seat_ID,
                        seatRes.State);
                    ret.Add(seatRes);
                }
                //}
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
                //using (var db = new FlightDB()) {
                ticket.SeatReservations.ForEach(reservation => {
                    reservation.State = SeatState.Taken;
                    //db.SeatReservations.Attach(reservation);
                });
                ticket.OrderState = TicketState.Ordered;
                //db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                OperationContext.Current.InstanceContext.Closed -= InstanceContext_Closed;
                //}
            } catch (Exception ex) {
                throw new DatabaseException("Complete Error", ex);
            }

            Debug.WriteLine("Completed ended!");
        }

        private void DebugSaveChanges() {
            try {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            } catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}