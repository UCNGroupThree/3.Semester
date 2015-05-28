using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.ServiceModel;
using Common;
using Common.Exceptions;
using WCFService.Dijkstra;
using WCFService.Dijkstra.Test;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;
using Matrix = WCFService.Dijkstra.Matrix;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private const bool test = false;

        public static void Updated(object updatedObj) {
            if (!test) {
                Matrix.GetInstance().Updated(updatedObj);
            }



            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Added(object addedObj) {
            if (!test) {
                Matrix.GetInstance().Added(addedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Removed(object removedObj) {
            if (!test) {
                Matrix.GetInstance().Removed(removedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        //####### Test #######
        private static void Test() {
            decimal dm = 0;
            var watch = Stopwatch.StartNew();
            //Airport a1;
            //Airport a2;
            //using (AirportServiceClient client = new AirportServiceClient()) {
            //    a1 = client.GetAirport(id1);
            //    a2 = client.GetAirport(id2);
            //}
            DateTime dateTime = DateTime.Now.AddHours(-10);
            List<Flight> aps = new Dijkstra().DijkstraStuff(1, 3, 1, dateTime);

            if (aps != null && aps.Count > 0) {
                foreach (var flight in aps) {
                    Trace.WriteLine(flight.Route.From.ID + ":" + flight.Route.From.Name + " -> " + flight.Route.To.Name + ":" + flight.Route.To.ID + " - Price: " + flight.Route.Price);
                    dm += flight.Route.Price;
                }

                Trace.WriteLine("Total Price: " + dm);
            } else {
                Trace.WriteLine("Empty Result");
            }

            watch.Stop();
            Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
        }
        //####### Test End #######


        /// <exception cref="LockedFault">Thrown when Dijkstra has updated for more than 15 seconds</exception>
        public List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime) {
            try {
                var matrix = WCFService.Dijkstra.Matrix.GetInstance();
                List<Flight> ret = matrix.GetShortestPath(fromId, toId, seats, startTime);
                return ret;
            } catch (LockedException e) {
                throw new FaultException<LockedFault>(new LockedFault() { Description = e.Message, Message = e.Message }, new FaultReason(e.Message));
            }
        }

        /*
         * IQueryable<Airport> fromAirports = _db.Airports
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats)))
                .Include(a => a.Routes.Select(r => r.To))
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                .Where(a => a.Routes.Any(r => r.Flights.Any(f => f.SeatReservations.Count < f.Plane.Seats.Count)));
         */

        public void DijkstraTest(int from, int to, int seats, DateTime dt) {
#if DEBUG
            Trace.WriteLine("----------- Constructor -----------");
            var watch = Stopwatch.StartNew();
#endif

            var list = WCFService.Dijkstra.Test.Matrix.GetInstance().CalculateShortestPathBetween(new Airport() { ID = from }, new Airport() { ID = to }, seats, dt).ToList();

#if DEBUG
            Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
            Trace.WriteLine("-------- Constructor Ended ---------");
#endif

            decimal total = 0;
            foreach (var path in list) {
                Debug.Write(path + " | ");
                if (path.FinalFlight != null) {
                    Debug.Write(path.FinalFlight.DepartureTime + " | " + path.FinalFlight.ArrivalTime);
                }
                Debug.WriteLine("");
                total += path.Route.Price;
            }
            Debug.WriteLine("Total Cost: " + total);
        }
    }
}