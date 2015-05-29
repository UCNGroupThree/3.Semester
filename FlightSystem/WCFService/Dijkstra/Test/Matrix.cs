using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Newtonsoft.Json.Linq;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Faults;

namespace WCFService.Dijkstra.Test {

    public class Path {
        public Airport From { get; set; }
        public Airport To { get; set; }

        public Route Route { get; set; }

        public Flight FinalFlight { get; set; }

        public override string ToString() {
            return From.ToString() + " -> " + To.ToString();
        }
    } // class Path<T>

    /// <summary>
    /// Calculates the best route between various paths, using Dijkstra's algorithm
    /// </summary>
    /// <remarks>
    /// Copied the algorithm's implementation from <see cref="http://www.codeproject.com/Articles/22647/Dijkstra-Shortest-Route-Calculation-Object-Oriente"/>.
    /// </remarks>
    public class Matrix {

        private static object syncRoot = new Object();
        private static Matrix _instance;
        private List<Path> _paths = new List<Path>();

        public static Matrix GetInstance() {
            if (_instance == null) {
                lock (syncRoot) {
                    if (_instance == null)
                        _instance = new Matrix();
                }
            }

            return _instance;
        }

        private Matrix() {
            using (var db = new FlightDB()) {
                List<Airport> fromAirports = db.Airports.OrderBy(n => n.ID)
                    .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(p => p.Seats)))
                    .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                    .Include(a => a.Routes.Select(r => r.To)).ToList();

                HashSet<Airport> tmpAirports = fromAirports.ToHashSet();
                var toAirports = tmpAirports.SelectMany(r => r.Routes.Select(a => a.To)).ToHashSet();
                tmpAirports.UnionWith(toAirports);

                foreach (var fromAirport in tmpAirports) {
                    foreach (var route in fromAirport.Routes) {
                        route.Flights.Sort(((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                        Path path = new Path() {
                            From = fromAirport,
                            To = route.To,
                            Route = route
                        };

                        _paths.Add(path);
                    }
                }
            }
        }

        //private static Dictionary<Tuple<Airport, DateTime>, Dictionary<Airport, LinkedList<Path>>> pathDictionary = new Dictionary<Tuple<Airport, DateTime>, Dictionary<Airport, LinkedList<Path>>>();

        public LinkedList<int> CalculateShortestPathBetween(Airport from, Airport to, int seats, DateTime dt) {
            LinkedList<Path> paths = CalculateFrom(from, seats, dt)[to];
            LinkedList<int> intPaths = new LinkedList<int>(paths.Select(p => p.FinalFlight.ID));

            return intPaths;
        }

        private Dictionary<Airport, LinkedList<Path>> CalculateFrom(Airport from, int seats, DateTime dt) {

            //if (pathDictionary.ContainsKey(new Tuple<Airport, DateTime>(source, dt))) {
            //    Debug.WriteLine("Using old dictionary for: " + source);
            //    return pathDictionary[new Tuple<Airport, DateTime>(source, dt)];
            //}

            // keep track of the shortest paths identified
            Dictionary<Airport, KeyValuePair<decimal, LinkedList<Path>>> ShortestPaths = new Dictionary<Airport, KeyValuePair<decimal, LinkedList<Path>>>();

            // Airports that has been processed
            List<Airport> locationsProcessed = new List<Airport>();

            // include all possible steps, with Int.MaxValue cost
            _paths.SelectMany(p => new Airport[] { p.From, p.To })              
                    .Distinct()                                                        // instead of toHashset :)
                    .ToList()                                                          
                    .ForEach(s => ShortestPaths.Set(s, Int32.MaxValue, null));         // Set MaxValue cost

            //Sets "From" to 0 (Doesent search for it)
            ShortestPaths.Set(from, 0, null);

            //caching the key count
            var locationCount = ShortestPaths.Keys.Count;

            while (locationsProcessed.Count < locationCount) {

                Airport locationToProcess = default(Airport);
                

                //Search for the nearest location that havent been worked
                foreach (Airport location in ShortestPaths.OrderBy(p => p.Value.Key).Select(p => p.Key).ToList()) {
                    if (!locationsProcessed.Contains(location)) {
                        if (ShortestPaths[location].Key == Int32.MaxValue) {
                            //Dictionary<Airport, LinkedList<Path>> pp = ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);

                            //pathDictionary.Add(new Tuple<Airport, DateTime>(source, dt), pp);

                            return ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);
                        }

                        locationToProcess = location;
                        
                        break;
                    }
                } // end foreach


                //Gets the paths to process, from the earlier selected "locationToProcess"
                var selectedPaths = _paths.Where(p => p.From.Equals(locationToProcess));

                //Processes the selectedPaths.
                foreach (Path path in selectedPaths) {
                    if (ShortestPaths[path.To].Key > path.Route.Price + ShortestPaths[path.From].Key) {

                        //Getting last arrivaltime (From previous processed location relative to this path)
                        try {
                            var pa = ShortestPaths[path.From].Value.Last();
                            if (pa != null && pa.FinalFlight != null) {
                                dt = pa.FinalFlight.ArrivalTime;
                            }
                        } catch (InvalidOperationException) { }

                        //Gets a flight from current path, that depart later than the given DateTime, and has enough seats
                        var currFlight = path.Route.Flights.FirstOrDefault(f => f.DepartureTime.TimeOfDay > dt.TimeOfDay && (f.SeatReservations.Count + seats) < f.Plane.Seats.Count);
                        
                        //If its not null(Success) then we add it to the dictionary
                        if (currFlight != null) {
                            path.FinalFlight = currFlight;
                            ShortestPaths.Set(
                                path.To,
                                path.Route.Price + ShortestPaths[path.From].Key,
                                ShortestPaths[path.From].Value.Union(new Path[] { path }).ToArray());
                        }
                    }
                } // end foreach

                //Add to processed list
                locationsProcessed.Add(locationToProcess);

            } // end while

            //Tuple<Airport, DateTime> tup = new Tuple<Airport, DateTime>(source, dt);
            //Dictionary<Airport, LinkedList<Path>> pp2 = ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);

            //pathDictionary.Add(tup, pp2);

            return ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);

        }
    } 


    public static class ExtensionMethods {
        /*
        public static LinkedList<Path> CleanLinkedList(this LinkedList<Path> paths) {
            try {
                LinkedList<Path> newPath = new LinkedList<Path>();
                foreach (var oldPath in paths) {
                    var path = new Path( {
                        FinalFlight = new Flight() {
                             ID = oldPath.FinalFlight.ID,
                             ArrivalTime = oldPath.FinalFlight.ArrivalTime,
                             DepartureTime = oldPath.FinalFlight.DepartureTime,
                             Plane = new Plane() {
                                 ID = oldPath.FinalFlight.PlaneID,
                                 Name = oldPath.FinalFlight.Plane.Name
                             },
                             Route = new Route() {
                                 From = new Airport() {
                                     
                                 }
                             }
                        }
                    });
                    path.From.Routes = null;
                    path.To.Routes = null;
                    path.FinalFlight.SeatReservations = null;
                    path.FinalFlight.Plane.Seats = null;
                    path.Route.Flights = null;
                    path.FinalFlight.Route = null;
                    path.FinalFlight.Plane.Flights = null;

                    newPath.AddLast(path);
                }

                return newPath;
            } catch (Exception) {
                throw new Exception("Invalid LinkedList");
            }
        }
         * */
        /// <summary>
        /// Adds or Updates the dictionary to include the destination and its associated cost and complete path (and param arrays make paths easier to work with)
        /// </summary>
        public static void Set<T>(this Dictionary<T, KeyValuePair<decimal, LinkedList<Path>>> dictionary, T destination, decimal Cost, params Path[] paths) {
            var completePath = paths == null ? new LinkedList<Path>() : new LinkedList<Path>(paths);
            dictionary[destination] = new KeyValuePair<decimal, LinkedList<Path>>(Cost, completePath);
        }
    } 

}