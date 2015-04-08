using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class RouteService : IRouteService {

        private readonly FlightDB db = new FlightDB();

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

                Route oldRoute = db.Routes.Find(route.ID);

                db.Entry(oldRoute).CurrentValues.SetValues(route);
                oldRoute.Flights = route.Flights;
                db.Entry(oldRoute).State = EntityState.Modified;

                foreach (var flight in oldRoute.Flights) {
                    System.Diagnostics.Debug.WriteLine(flight.ID + "flight"); //TODO Remove after test
                    if (flight.ID > 0) {
                        //db.Flights.Attach(flight);
                        System.Diagnostics.Debug.WriteLine(flight.ID + "Set to modified"); //TODO Remove after test
                        db.Entry(flight).State = EntityState.Unchanged;
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
            } catch (OptimisticConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() {
                    Message = e.Message
                });
            } catch (DbUpdateException) {
                var ctx = ((IObjectContextAdapter)db).ObjectContext;
                ctx.Refresh(RefreshMode.ClientWins, db.Flights);
                db.SaveChanges();
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
                db.Routes.Attach(route);
                db.Entry(route).State = EntityState.Deleted;
                db.SaveChanges();
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