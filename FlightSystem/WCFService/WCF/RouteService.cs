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
using Common;
using WCFService.Helper;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class RouteService : IRouteService {

        private readonly FlightDB db = new FlightDB();

        public RouteService() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            
        }

        public Route AddRoute(Route route) {
            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            if (db.Routes.Count(f => f.From.ID == route.From.ID && f.To.ID == route.To.ID) == 0) {

                try {
                    route.FromID = route.From.ID;
                    route.ToID = route.To.ID;
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
            Route retRoute = route.Clone();

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                Debug.WriteLine("UpdateRoute");
                
                Debug.WriteLine(route.From.Name + " " + route.To.Name);

                if (route.ToID == 0 && route.To != null) {
                    route.ToID = route.To.ID;
                    retRoute.ToID = route.To.ID;
                    route.To = null;
                } else if (route.ToID != 0) {
                    route.To = null;
                }

                if (route.FromID == 0 && route.From != null) {
                    route.FromID = route.From.ID;
                    retRoute.FromID = route.From.ID;
                    route.From = null;
                } else if (route.FromID != 0) {
                    route.From = null;
                }

                if (route.Flights.Count > 0) {
                    route.Flights = null;
                }

                db.Entry(route).State = EntityState.Modified;

                //DetectChanges(db);

                db.SaveChanges();

                retRoute.Concurrency = route.Concurrency;

                //DebugSaveChanges();

                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(retRoute)).Start();
            } catch (DbUpdateConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException ex) {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            } catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            }

            return retRoute;
        }



        public Route AddOrUpdateFlights(Route route) {
            Route retRoute = route.Clone();

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                Debug.WriteLine("AddOrUpdateFlights");
                Debug.WriteLine(route.From.Name + " " + route.To.Name);


                foreach (var flight in route.Flights) {
                    System.Diagnostics.Debug.WriteLine(flight.ID + "flight"); //TODO Remove after test
                    if (flight.Plane != null) {
                        flight.PlaneID = flight.Plane.ID;
                    }
                    flight.Plane = null;
                    
                    if (flight.ID > 0) {
                        System.Diagnostics.Debug.WriteLine(flight.ID + "Set to modified"); //TODO Remove after test
                        db.Entry(flight).State = EntityState.Modified;
                    } else {
                        System.Diagnostics.Debug.WriteLine(flight.ID + "Set to added"); //TODO Remove after test
                        db.Flights.Add(flight);
                    }
                }

                //db.DebugDetectChanges();

                db.SaveChanges();
                retRoute.Concurrency = route.Concurrency;
                //db.DebugSaveChanges();
                
                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(retRoute)).Start();


            } catch (DbUpdateConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException ex) {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            } catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            }

            return retRoute;
        }

        

        public void DeleteRoute(Route route) {
            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {

                route = db.Routes.Include(r => r.To).Include(r => r.Flights.Select(f => f.SeatReservations)).First(r => r.ID == route.ID);

                if (route.Flights.Any(f => f.SeatReservations.Any(s => s.State == SeatState.Taken || s.State == SeatState.Occupied))) {
                    throw new FaultException<DeleteFault>(new DeleteFault("The Route can't be deleted, Flights are in use."));
                }
                
                //db.Routes.Attach(route);
                db.Routes.Remove(route);
                //db.Entry(route).State = EntityState.Deleted;
                db.SaveChanges();

                // Running Async Remove on Dijkstra Matrix
                new Task(() => Dijkstra.Removed(route)).Start();
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

            if (routes.Count == 0) {
                throw new FaultException<NullPointerFault>(new NullPointerFault() {Message = "The Airport has no Routes"});
            }

            return routes;
        }

        
    }
}