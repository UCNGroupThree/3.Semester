using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class RouteService : IRouteService {

        private readonly FlightDB db = new FlightDB();

        public RouteService() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            db.Database.Log = m => Debug.WriteLine(m);
        }

        public Route AddRoute(Route route) {
            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            if (db.Routes.Count(f => f.From.ID == route.From.ID && f.To.ID == route.To.ID) == 0) {

                try {
                    db.Routes.Add(route);
                    db.Airports.Attach(route.From);
                    db.Airports.Attach(route.To);
                    db.SaveChanges();

                    // Running Async Added on Dijkstra Matrix
                    new Task(() => Dijkstra.Added(route)).Start();
                } catch (Exception e) {
                    Console.WriteLine(e.Message); //TODO DEBUG MODE?
                    throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault() {Message = e.Message});
                }

            } else {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault(){Message = "Entity already exists in database"});
            }

            return route;
        }

        public Route UpdateRoute(Route route) {
            Route retRoute = route;

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                Debug.WriteLine(route.From.Name + " " + route.To.Name);
               // var oldRoute = db.Routes.Find(route.ID);
            //var s = db.ChangeTracker.Entries<Route>().First(x => x.Entity.ID == route.ID);

               // db.Entry(oldRoute).CurrentValues.SetValues(route);
            //oldRoute.From = route.From;
            //oldRoute.To = route.To;
               // oldRoute.Flights = route.Flights;
                db.Entry(route).State = EntityState.Modified;

                foreach (var flight in route.Flights) {
                    System.Diagnostics.Debug.WriteLine(flight.ID + "flight"); //TODO Remove after test
                    if (flight.ID > 0) {
                        //db.Flights.Attach(flight);
                        System.Diagnostics.Debug.WriteLine(flight.ID + "Set to modified"); //TODO Remove after test
                        db.Entry(flight).State = EntityState.Modified;
                    } else {
                        System.Diagnostics.Debug.WriteLine(flight.ID + "Set to added"); //TODO Remove after test
                        db.Flights.Add(flight);
                    }

                    if (flight.Plane.ID > 0) {
                        db.Planes.Attach(flight.Plane);
                    }

                }



                //db.Entry(route.Flights).State = EntityState.Added;

                db.SaveChanges();
                //DebugSaveChanges();

                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(route)).Start();

                
            } catch (OptimisticConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException) {
                var ctx = ((IObjectContextAdapter)db).ObjectContext;
                ctx.Refresh(RefreshMode.ClientWins, db.Flights);
                //db.SaveChanges();
                DebugSaveChanges();
                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(route)).Start();
            } catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            }

            return retRoute;
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

        public void DeleteRoute(Route route) {
            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                db.Routes.Attach(route);
                db.Entry(route).State = EntityState.Deleted;
                db.SaveChanges();

                // Running Async Remove on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(route)).Start();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message); //TODO DEBUG MODE?
                
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault() { Message = ex.Message });
            }
        }

        public Route GetRoute(int id) {
            Route route = db.Routes.Where(r => r.ID == id).Include(r => r.To).Include(r => r.From).Include(r => r.Flights).SingleOrDefault();

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return route;
        }

        public Route GetRouteByAirports(Airport from, Airport to) {
            Route route = db.Routes.Where(r => r.From.ID == from.ID && r.To.ID == to.ID).Include(r => r.To).Include(r => r.From).Include(r => r.Flights).SingleOrDefault();

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return route;
        }

        public List<Route> GetRoutesByAirport(Airport from) {
            List<Route> routes = db.Routes.Where(r => r.From.ID == from.ID).Include(r => r.To).Include(r => r.From).Include(r => r.Flights.Select(s => s.Plane)).ToList();

            if (!(routes.Count > 0)) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return routes;
        }
    }
}