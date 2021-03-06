﻿using System;
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

            if (db.Routes.Count(f => f.FromID == route.FromID && f.ToID == route.ToID) == 0) {

                try {
                    if (route.From != null) {
                        route.FromID = route.From.ID;
                        db.Entry(route.From).State = EntityState.Unchanged;
                    }

                    if (route.To != null) {
                        route.ToID = route.To.ID;
                        db.Entry(route.To).State = EntityState.Unchanged;
                    }

                    db.Routes.Add(route);
                    db.SaveChanges();

                    // Running Async Added on Dijkstra Matrix
                    new Task(() => Dijkstra.Added(route)).Start();
                } catch (Exception e) {
#if DEBUG
                    e.DebugGetLine();
#endif
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
                Trace.WriteLine("UpdateRoute");
                
                Trace.WriteLine(route.From.Name + " " + route.To.Name);

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

                db.SaveChanges();

                retRoute.Concurrency = route.Concurrency;

                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(retRoute)).Start();
            } catch (DbUpdateConcurrencyException e) {
#if DEBUG
                e.DebugGetLine();
#endif
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
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
                Trace.WriteLine("AddOrUpdateFlights");
                Trace.WriteLine(route.FromID + " " + route.ToID);


                foreach (var flight in route.Flights) {
                    System.Diagnostics.Trace.WriteLine(flight.ID + "flight"); //TODO Remove after test
                    if (flight.Plane != null) {
                        flight.PlaneID = flight.Plane.ID;
                    }
                    flight.Plane = null;
                    
                    if (flight.ID > 0) {
                        if (db.SeatReservations.Any(s => s.Flight_ID == flight.ID)) {
                            throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("The Flight contains SeatReservations, and can therefor not be changed!"), new FaultReason("The Flight contains SeatReservations, and can therefor not be changed!"));
                        }
                        System.Diagnostics.Trace.WriteLine(flight.ID + "Set to modified"); //TODO Remove after test
                        db.Entry(flight).State = EntityState.Modified;
                    } else {
                        System.Diagnostics.Trace.WriteLine(flight.ID + "Set to added"); //TODO Remove after test
                        db.Flights.Add(flight);
                    }
                }

                db.SaveChanges();
                retRoute.Concurrency = route.Concurrency;
                
                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(retRoute)).Start();


            } catch (DbUpdateConcurrencyException e) {
#if DEBUG
                e.DebugGetLine();
#endif
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = ex.Message });
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
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
#if DEBUG
                ex.DebugGetLine();
#endif
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

            Trace.WriteLine(String.Format("Getting Route From {0} -> {1}", from.ID, to.ID));

            Route route = db.Routes.Where(r => r.From.ID == from.ID && r.To.ID == to.ID).Include(r => r.To).Include(r => r.From).Include(r => r.Flights).SingleOrDefault();

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return route;
        }

        public List<Route> GetRoutesByAirport(Airport from) {
            using (var _db = new FlightDB()) {
                List<Route> routes = _db.Routes.Where(r => r.From.ID == from.ID).Include(r => r.To).Include(r => r.From).Include(r => r.Flights.Select(s => s.Plane)).ToList();
                
                if (routes.Count == 0) {
                    throw new FaultException<NullPointerFault>(new NullPointerFault() { Message = "The Airport has no Routes" });
                }

                return routes;
            }
        }

        
    }
}