using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
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

            try {
                db.Routes.Add(route);
                db.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine(e.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault() { Message = e.Message });
            }

            return route;
        }

        public Route UpdateRoute(Route route) {
            Route retRoute = route;

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                db.Routes.Attach(route);
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
            } catch (OptimisticConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault() { Message = e.Message });
            } catch (Exception ex) {
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
                db.Routes.Remove(route);
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault() { Message = ex.Message });
            }
        }

        public Route GetRoute(int id) {
            Route route = db.Routes.SingleOrDefault(r => r.ID == id);

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return route;
        }

        public Route GetRoute(Airport from, Airport to) {
            Route route = db.Routes.SingleOrDefault(r => r.From.ID == from.ID && r.To.ID == to.ID);

            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return route;
        }

        public List<Route> GetRoutes(Airport from) {
            List<Route> routes = db.Routes.Where(r => r.From.ID == from.ID).ToList();

            if (!(routes.Count > 0)) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return routes;
        }
    }
}