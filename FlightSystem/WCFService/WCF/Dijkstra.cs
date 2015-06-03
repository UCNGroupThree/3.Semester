using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Common.Exceptions;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private const bool test = false;

        public static void Updated(object updatedObj) {
            //Trace.WriteLine("###### Dijkstra Update ######");
            if (!test) {
                DijkstraPath.GetInstance().Updated(updatedObj);
            }



            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Added(object addedObj) {
            //Trace.WriteLine("###### Dijkstra Added ######");
            if (!test) {
                DijkstraPath.GetInstance().Added(addedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public static void Removed(object removedObj) {
            //Trace.WriteLine("###### Dijkstra Removed ######");
            if (!test) {
                DijkstraPath.GetInstance().Removed(removedObj);
            }
            //####### Test #######
            //Test();
            //####### Test End #######
        }

        public List<Flight> GetShortestPath(int from, int to, int seats, DateTime dt) {

            if (from == to) {
                throw new FaultException<DijkstraFault>(new DijkstraFault("From and To Airport can't be the same"), new FaultReason("From and To Airport can't be the same"));
            }

            //Trace.WriteLine("----------- DijkstraTest -----------");
            var watch = Stopwatch.StartNew();
            

            var retFlights = new List<Flight>();

            try {
                var list = DijkstraPath.GetInstance()
                    .CalculateShortestPathBetween(new Airport() {ID = @from}, new Airport() {ID = to}, seats, dt);

                using (var db = new FlightDB()) {

                    retFlights = db.Flights
                    .Include(f => f.Plane)
                    .Include(f => f.Route.From)
                    .Include(f => f.Route.To)
                    .Where(f => list.Contains(f.ID)).OrderBy(f => f.DepartureTime).ToList();

                }

            } catch (AirportNotFoundException airportNotFoundException) {
                throw new FaultException<DijkstraFault>(new DijkstraFault(airportNotFoundException.Message), new FaultReason(airportNotFoundException.Message));
            } catch (NoValidPathException noValidPathException) {
                throw new FaultException<DijkstraFault>(new DijkstraFault(noValidPathException.Message), new FaultReason(noValidPathException.Message));
            }

            Trace.WriteLine(String.Format("{0} -> {1} | Time: {2} ms",from, to, watch.ElapsedMilliseconds));
            //Trace.WriteLine("-------- DijkstraTest Ended ---------");

            /*

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
            */

            return retFlights;
        }
    }
}