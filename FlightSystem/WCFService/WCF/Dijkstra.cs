using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.ServiceModel;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using WCFService.Dijkstra;
using WCFService.Dijkstra.Test;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;
using WCFService.Dijkstra;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private const bool test = false;

        public static void Updated(object updatedObj) {
            //Trace.WriteLine("###### Dijkstra Update ######");
            if (!test) {
                Matrix.GetInstance().Updated(updatedObj);
            }



            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Added(object addedObj) {
            //Trace.WriteLine("###### Dijkstra Added ######");
            if (!test) {
                Matrix.GetInstance().Added(addedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Removed(object removedObj) {
            //Trace.WriteLine("###### Dijkstra Removed ######");
            if (!test) {
                Matrix.GetInstance().Removed(removedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public List<Flight> GetShortestPath(int from, int to, int seats, DateTime dt) {

            Trace.WriteLine("----------- DijkstraTest -----------");
            var watch = Stopwatch.StartNew();


            var list = WCFService.Dijkstra.Test.Matrix.GetInstance().CalculateShortestPathBetween(new Airport() { ID = from }, new Airport() { ID = to }, seats, dt);
            
            var retFlights = new List<Flight>();

            using (var db = new FlightDB()) {

                retFlights = db.Flights
                .Include(f => f.Plane)
                .Include(f => f.Route.From)
                .Include(f => f.Route.To)
                .Where(f => list.Contains(f.ID)).OrderBy(f => f.DepartureTime).ToList();

            }
            
            new Task(() => {

                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- DijkstraTest Ended ---------");


                decimal total = 0;
                foreach (var path in retFlights) {
                    Debug.Write(path + " | ");
                    if (path != null) {
                        Debug.Write(path.DepartureTime + " | " + path.ArrivalTime);
                    }
                    Debug.WriteLine("");
                    total += path.Route.Price;
                }
                Debug.WriteLine("Total Cost: " + total);
            }).Start();

            return retFlights;
        }
    }
}