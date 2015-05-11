using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        public static void Updated(object updatedObj) {
            Matrix.GetInstance().Updated(updatedObj);

            //####### Test #######
            Test();
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
                    Debug.WriteLine(flight.Route.From.ID + ":" + flight.Route.From.Name + " -> " + flight.Route.To.Name + ":" + flight.Route.To.ID + " - Price: " + flight.Route.Price);
                    dm += flight.Route.Price;
                }

                Debug.WriteLine("Total Price: " + dm);
            } else {
                Debug.WriteLine("Empty Result");
            }

            watch.Stop();
            Debug.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
        }
        //####### Test End #######

        public List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime) {
            var matrix = Matrix.GetInstance();

            List<Flight> ret = matrix.GetShortestPath(fromId, toId, seats, startTime);


            return ret;
        }
    }
}