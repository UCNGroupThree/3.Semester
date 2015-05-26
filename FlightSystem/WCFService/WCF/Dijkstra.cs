using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.ServiceModel;
using Common.Exceptions;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        public static void Updated(object updatedObj) {
            Matrix.GetInstance().Updated(updatedObj);

            

            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Added(object addedObj) {
            Matrix.GetInstance().Added(addedObj);

            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Removed(object removedObj) {
            Matrix.GetInstance().Removed(removedObj);

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
                var matrix = Matrix.GetInstance();
                List<Flight> ret = matrix.GetShortestPath(fromId, toId, seats, startTime);
                return ret;
            } catch (LockedException e) {
                throw new FaultException<LockedFault>(new LockedFault(){Description = e.Message, Message = e.Message});
            }
        }
    }
}