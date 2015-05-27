using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using WCFService.WCF.Interface;
using WCFService.Model;
using System.ServiceModel;
using WCFService.WCF.Faults;
using System.Threading.Tasks;

namespace WCFService.WCF {
    public class FlightService : IFlightService {

        private readonly FlightDB db = new FlightDB();

        public int AddFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            try {
                db.Flights.Add(flight);
                db.SaveChanges();

                // Running Async Added on Dijkstra Matrix
                new Task(() => Dijkstra.Added(flight)).Start();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault(){Message = ex.Message});
            }
            return flight.ID;

        }

        public Flight UpdateFlight(Flight flight) {
            Flight retFlight = flight;

            if (flight == null){
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            try {
                db.Flights.AddOrUpdate(flight);
                db.SaveChanges();

                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(flight)).Start();
            } catch (OptimisticConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault(){Message = e.Message});
            }catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault(){Message = ex.Message});
            }

            return retFlight;
        }

        public void DeleteFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            
            if (db.SeatReservations.Any(s => s.Flight_ID == flight.ID)) {
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault("The Flight contains SeatReservations, and can therefor not be deleted!"), new FaultReason("The Flight contains SeatReservations, and can therefor not be deleted!"));
            }

            try {
                db.Flights.Attach(flight);
                db.Flights.Remove(flight);
                db.SaveChanges();

                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Removed(flight)).Start();
            }
            catch (Exception ex) {

                Trace.WriteLine(ex.Message);
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault(){Message = ex.Message});
            }
        }

        public Flight GetFlight(int id) {
            Flight flight = db.Flights.SingleOrDefault(x => x.ID == id);

            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }

            return flight;
        }

        public List<Flight> GetFlights(Airport from, Airport to) {
            RouteService rService = new RouteService();
            Route route = rService.GetRouteByAirports(from, to);
            if (route == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault(){Message = String.Format("A Route from {0} to {1} could not be found", from.Name, to.Name)});
            }
            if (route.Flights == null || !(route.Flights.Count > 0)) {
                throw new FaultException<NullPointerFault>(new NullPointerFault(){Message = String.Format("The Route: {0} -> {1} does not contain any flights", from.Name, to.Name)});
            }

            return route.Flights;
        } 
    }
}