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
using WCFService.Dijkstra.Test;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Faults;

namespace WCFService.Dijkstra {

    /// <summary>
    /// Calculates the best route between various paths, using Dijkstra's algorithm
    /// </summary>
    /// <remarks>
    /// Based on inspiration from <see cref="http://www.codeproject.com/Articles/22647/Dijkstra-Shortest-Route-Calculation-Object-Oriente"/>.
    /// </remarks>
    public class Matrix {

        private static object eventLock = new Object();

        #region Removed / Added / Updated

        public void Removed(object removedObj) {
            lock (eventLock) {
                if (removedObj is Airport) {
                    RemoveAirport((Airport)removedObj);
                } else if (removedObj is Route) {
                    RemoveRoute((Route)removedObj);
                } else if (removedObj is Flight) {
                    RemoveFlight((Flight)removedObj);
                }
            }
        }

        public void Added(object addedObj) {
            lock (eventLock) {
                if (addedObj is Route) {
                    AddRoute((Route) addedObj);
                } else if (addedObj is Flight) {
                    AddFlight((Flight) addedObj);
                }
            }
        }

        public void Updated(object updatedObj) {
            lock (eventLock) {
                if (updatedObj is Airport) {
                    UpdateAirport((Airport) updatedObj);
                } else if (updatedObj is Route) {
                    UpdateRoute((Route) updatedObj);
                } else if (updatedObj is Flight) {
                    UpdateFlight((Flight) updatedObj);
                }
            }
        }

        #endregion

        #region Remove (Events)

        private void RemoveFlight(Flight flight) {
            try {
#if DEBUG
                // ####### Timing #######
                var watch = Stopwatch.StartNew();
                // ####### Timing End #######
#endif
                var route = _paths.Select(p => p.Route).FirstOrDefault(r => r.Flights.Contains(flight));

                if (route != null)
                    route.Flights.Remove(flight);
#if DEBUG
                // ####### Timing #######
                watch.Stop();
                Trace.WriteLine("RemoveFlight Time: " + watch.ElapsedMilliseconds + "ms");
                // ####### Timing End #######
#endif
            } catch (Exception) {
                HardReset();
            }
        }

        private void RemoveAirport(Airport airport) {
            try {
#if DEBUG
                // ####### Timing #######
                var watch = Stopwatch.StartNew();
                // ####### Timing End #######
#endif
                _paths.RemoveAll(p => p.From.Equals(airport) || p.To.Equals(airport));
#if DEBUG
                // ####### Timing #######
                watch.Stop();
                Trace.WriteLine("RemoveAirport Time: " + watch.ElapsedMilliseconds + "ms");
                // ####### Timing End #######
#endif
            } catch (Exception) {
                HardReset();
            }
        }

        private void RemoveRoute(Route route) {
            try {
#if DEBUG
                // ####### Timing #######
                var watch = Stopwatch.StartNew();
                // ####### Timing End #######
#endif
                _paths.RemoveAll(p => p.Route.ID == route.ID);
#if DEBUG
                // ####### Timing #######
                watch.Stop();
                Trace.WriteLine("RemoveRoute Time: " + watch.ElapsedMilliseconds + "ms");
                // ####### Timing End #######
#endif
            } catch (Exception) {
                HardReset();
            }

        }

        #endregion

        #region Update

        private void UpdateFlight(Flight flight) {
            using (var db = new FlightDB()) {
                //  try {
#if DEBUG
                // ####### Timing #######
                var watch = Stopwatch.StartNew();
                // ####### Timing End #######
#endif
                var newFlight =
                    db.Flights.Include(f => f.Plane)
                        .Include(f => f.Plane.Seats)
                        .Include(f => f.SeatReservations)
                        .Include(f => f.Route.From)
                        .Include(f => f.Route.To)
                        .First(f => f.ID == flight.ID);

                var path = _paths.SingleOrDefault(p => p.Route.Flights.Any(f => f.ID == newFlight.ID));

                if (path != null) {
                    path.Route.Flights.Replace(newFlight);
                    path.Route.Flights.Sort(((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                }
#if DEBUG
                // ####### Timing #######
                watch.Stop();
                Trace.WriteLine("UpdateFlight Time: " + watch.ElapsedMilliseconds + "ms");
                // ####### Timing End #######
#endif
                //} catch (Exception) {
                //    HardReset();
                //}
            }
        }

        private void UpdateRoute(Route route) {
            using (var db = new FlightDB()) {
                //try {
#if DEBUG
                // ####### Timing #######
                var watch = Stopwatch.StartNew();
                // ####### Timing End #######
#endif
                var newRoute = db.Routes.Include(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats))
                    .Include(r => r.To)
                    .Include(r => r.Flights.Select(f => f.SeatReservations)).First(r => r.ID == route.ID);

                var path = _paths.SingleOrDefault(p => p.Route.Equals(newRoute));
                if (path != null) {
                    path.Route = newRoute;
                    path.Route.Flights.Sort(((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                }
#if DEBUG
                // ####### Timing #######
                watch.Stop();
                Trace.WriteLine("UpdateRoute Time: " + watch.ElapsedMilliseconds + "ms");
                // ####### Timing End #######
#endif
                //} catch (Exception) {
                //    HardReset();
                //}
            }
        }

        private void UpdateAirport(Airport airport) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######
                    var watch = Stopwatch.StartNew();
                    // ####### Timing End #######
#endif
                    var newAirport = db.Airports.First(a => a.ID == airport.ID);

                    _paths.Where(p => p.From.Equals(newAirport)).ToList().ForEach(p => p.From = newAirport);
                    _paths.Where(p => p.To.Equals(newAirport)).ToList().ForEach(p => p.To = newAirport);
#if DEBUG
                    // ####### Timing #######
                    watch.Stop();
                    Trace.WriteLine("UpdateAirport Time: " + watch.ElapsedMilliseconds + "ms");
                    // ####### Timing End #######
#endif
                } catch (Exception) {
                    HardReset();
                }
            }
        }

        #endregion

        #region Add (Events)

        private void AddRoute(Route route) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######
                    var watch = Stopwatch.StartNew();
                    // ####### Timing End #######
#endif
                    var newRoute = db.Routes.Include(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats))
                        .Include(r => r.To)
                        .Include(r => r.Flights.Select(f => f.SeatReservations)).First(r => r.ID == route.ID);

                    route.Flights.Sort(((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                    Path path = new Path() {
                        From = route.From,
                        To = route.To,
                        Route = route
                    };

                    _paths.Add(path);
#if DEBUG
                    // ####### Timing #######
                    watch.Stop();
                    Trace.WriteLine("AddRoute Time: " + watch.ElapsedMilliseconds + "ms");
                    // ####### Timing End #######
#endif
                } catch (Exception) {
                    HardReset();
                }
            }
        }

        private void AddFlight(Flight flight) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######
                    var watch = Stopwatch.StartNew();
                    // ####### Timing End #######
#endif
                    var newFlight =
                        db.Flights.Include(f => f.Plane)
                            .Include(f => f.Plane.Seats)
                            .Include(f => f.SeatReservations)
                            .Include(f => f.Route)
                            .Include(f => f.Route.To)
                            .First(f => f.ID == flight.ID);

                    var path = _paths.FirstOrDefault(p => p.Route.ID == newFlight.ID);

                    if (path != null) {
                        path.Route.Flights.Add(newFlight);
                        path.Route.Flights.Sort(((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                    }
#if DEBUG
                    // ####### Timing #######
                    watch.Stop();
                    Trace.WriteLine("AddFlight Time: " + watch.ElapsedMilliseconds + "ms");
                    // ####### Timing End #######
#endif
                } catch (Exception) {
                    HardReset();
                }
            }
        }

        #endregion

        private void HardReset() {
            _instance = new Matrix();
        }


        private static object syncRoot = new Object();
        private static Matrix _instance;
        private List<Path> _paths = new List<Path>();

        //private DotGenerator _dot = new DotGenerator();

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
                db.Routes.Load();
                db.Airports.Load();
                db.Flights.Load();
                db.SeatReservations.Load();
                db.Planes.Load();
                db.Seats.Load();
                var query = db.Airports;
                var fromAirports = query.ToList();
                //List<Airport> fromAirports = db.Airports.OrderBy(n => n.ID)
                //    .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(p => p.Seats)))
                //    .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                //    .Include(a => a.Routes.Select(r => r.To)).ToList();
                /*
                HashSet<Airport> tmpAirports = fromAirports.ToHashSet();
                var toAirports = tmpAirports.SelectMany(r => r.Routes.Select(a => a.To)).ToHashSet();
                tmpAirports.UnionWith(toAirports);
                */
                foreach (var fromAirport in fromAirports) {
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
                //_dot.Airports = fromAirports;
                //_dot.Paths = _paths;
            }
        }

        //private static Dictionary<Tuple<Airport, DateTime>, Dictionary<Airport, LinkedList<Path>>> pathDictionary = new Dictionary<Tuple<Airport, DateTime>, Dictionary<Airport, LinkedList<Path>>>();

        public LinkedList<int> CalculateShortestPathBetween(Airport from, Airport to, int seats, DateTime dt) {
            LinkedList<Path> paths = CalculateFrom(from, seats, dt)[to];

            //_dot.CalcPath = paths;
            //_dot.GenerateDots();

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
        /// <summary>
        /// Adds or Updates the dictionary to include the destination and its associated cost and complete path (and param arrays make paths easier to work with)
        /// </summary>
        public static void Set<T>(this Dictionary<T, KeyValuePair<decimal, LinkedList<Path>>> dictionary, T destination, decimal Cost, params Path[] paths) {
            var completePath = paths == null ? new LinkedList<Path>() : new LinkedList<Path>(paths);
            dictionary[destination] = new KeyValuePair<decimal, LinkedList<Path>>(Cost, completePath);
        }

        public static void Replace<T>(this List<T> list, T obj) {
            list[list.IndexOf(obj)] = obj;
        }
    }

}