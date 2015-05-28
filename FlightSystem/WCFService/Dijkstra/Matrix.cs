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
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Faults;

namespace WCFService.Dijkstra {

    public class Matrix {

        #region Properties

        private readonly FlightDB _db = new FlightDB();
        private readonly RouteService _rs = new RouteService();
        private readonly HashSet<Edge<Airport>> _edges = new HashSet<Edge<Airport>>();
        private readonly HashSet<Airport> _airports;

        private static Matrix _instance;
        private static List<int> running = new List<int>();
        private int i = 0;

        private bool _locked = false;

        #endregion

        #region Instance

        private static object syncRoot = new Object();

        public static Matrix GetInstance() {
            if (_instance == null) {
                lock (syncRoot) {
                    if (_instance == null)
                        _instance = new Matrix();
                }
            }

            return _instance;
        }

        #endregion

        #region Add / Remove / Update

        public void Removed(object removedObj) {
            _locked = true;

            while (running.Count > 0) {
                Thread.Sleep(1000);
            }

            if (removedObj is Airport) {
                RemoveAirport((Airport)removedObj);
            } else if (removedObj is Route) {
                RemoveRoute((Route)removedObj);
            } else if (removedObj is Flight) {
                RemoveFlight((Flight)removedObj);
            }


            _locked = false;
        }

        public void Added(object addedObj) {
            _locked = true;

            while (running.Count > 0) {
                Thread.Sleep(1000);
            }

            if (addedObj is Route) {
                AddRoute((Route)addedObj);
            } else if (addedObj is Flight) {
                AddFlight((Flight)addedObj);
            }

            _locked = false;
        }

        public void Updated(object updatedObj) {
            _locked = true;

            while (running.Count > 0) {
                Thread.Sleep(100);
            }

            if (updatedObj is Airport) {
                UpdateAirport((Airport)updatedObj);
            } else if (updatedObj is Route) {
                UpdateRoute((Route)updatedObj);
            } else if (updatedObj is Flight) {
                UpdateFlight((Flight)updatedObj);
            }

            _locked = false;
        }


        #region Remove (Events)

        private void RemoveFlight(Flight flight) {
            try {
#if DEBUG
                // ####### Timing #######

                Trace.WriteLine("----------- RemoveFlight -----------");
                var watch = Stopwatch.StartNew();

                // ####### Timing End #######
#endif
                try {
                    var airport = _airports.Single(a => a.Routes.Any(r => r.Flights.Any(f => f.ID == flight.ID)));
                    var route = airport.Routes.Single(r => r.Flights.Any(f => f.ID == flight.ID));

                    var edge = _edges.Single(a => a.Data.Routes.Any(r => r.Flights.Any(f => f.ID == flight.ID)));
                    var edgeRoute = edge.Data.Routes.Single(r => r.Flights.Any(f => f.ID == flight.ID));

                    edgeRoute.Flights.RemoveAll(f => f.ID == flight.ID);
                    route.Flights.RemoveAll(f => f.ID == flight.ID);

                } catch (InvalidOperationException) {
                    return;
                }
#if DEBUG
                // ####### Timing #######

                watch.Stop();
                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- Remove Flight Ended ---------");

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

                Trace.WriteLine("----------- RemoveAirport -----------");
                var watch = Stopwatch.StartNew();

                // ####### Timing End #######
#endif

                _airports.RemoveWhere(a => a.ID == airport.ID);
                _edges.RemoveWhere(e => e.Data.ID == airport.ID);

                var edg = _edges.Where(e => e.Neighbors.Any(n => n.Data.ID == airport.ID)).ToList();
                edg.ForEach(e => e.Neighbors.RemoveAll(n => n.Data.ID == airport.ID));
#if DEBUG
                // ####### Timing #######

                watch.Stop();
                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- Remove Airport Ended ---------");

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

                Trace.WriteLine("----------- RemoveRoute -----------");
                var watch = Stopwatch.StartNew();

                // ####### Timing End #######
#endif
                var airport = _airports.First(a => a.Routes.Any(r => r.ID == route.ID));
                airport.Routes.RemoveAll(r => r.ID == route.ID);

                _edges.RemoveWhere(e => e.Data.ID == airport.ID);

                if (!airport.Routes.Any()) {
                    _airports.Remove(airport);
                } else {
                    var newEdge = GenerateEdge(airport);

                    _edges.Add(newEdge);
                }

                foreach (var edge in _edges) {
                    edge.Neighbors.RemoveAll(n => n.Data.ID == route.ToID);
                }
#if DEBUG
                // ####### Timing #######

                watch.Stop();
                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- RemoveRoute Ended ---------");

                // ####### Timing End #######
#endif
            } catch (Exception) {
                HardReset();
            }

        }

        #endregion

        #region Add (Events)

        private void AddRoute(Route route) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######

                    Trace.WriteLine("----------- AddRoute -----------");
                    var watch = Stopwatch.StartNew();

                    // ####### Timing End #######
#endif
                    // ####### Add / Edit Matrix ######

                    route = db.Routes.Include(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats))
                        .Include(r => r.To)
                        .Include(r => r.Flights.Select(f => f.SeatReservations)).First(r => r.ID == route.ID);

                    var airport = route.From;
                    var edge = _edges.First(ed => ed.Data.ID == route.FromID);

                    if (route.To == null) {
                        route.To = _db.Airports.Single(a => a.ID == route.ToID);
                    }

                    Vertex<Airport> vertex = new Vertex<Airport>(route.To, route.Price);

                    if (edge == null) {
                        edge = new Edge<Airport>(airport);
                        edge.Neighbors.Add(vertex);
                    } else if (edge.Neighbors.Any(v => v.Data.ID == vertex.Data.ID)) {
                        edge.Neighbors[edge.Neighbors.FindIndex(e => e.Data.ID == route.To.ID)] = vertex;
                    } else {
                        edge.Neighbors.Add(vertex);
                    }

                    // ####### Add / Edit Matrix End ######

                    // ##### Add / Edit Airport List ####

                    var ap = _airports.First(a => a.ID == airport.ID);

                    if (ap == null) {
                        ap = db.Airports.Include(
                            a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats)))
                            .Include(a => a.Routes.Select(r => r.To))
                            .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                            .First(a => a.ID == airport.ID);



                        _airports.ReplaceWithHash(ap);

                    } else if (ap.Routes.Any(r => r.ID == route.ID)) {
                        ap.Routes[ap.Routes.FindIndex(r => r.ID == route.ID)] = route;
                    } else {
                        ap.Routes.Add(route);
                    }

                    // ##### Add / Edit Airport List End ####

#if DEBUG
                    // ####### Timing #######

                    watch.Stop();
                    Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                    Trace.WriteLine("-------- AddRoute Ended ---------");

                    // ####### Timing End #######
#endif
                } catch (Exception) {
                    HardReset();
                }
            }
        }

        private void AddFlight(Flight flight) {
            try {
#if DEBUG
                // ####### Timing #######

                Trace.WriteLine("----------- AddFlight -----------");
                var watch = Stopwatch.StartNew();

                // ####### Timing End #######
#endif
                // ##### Add / Edit Airport List #####

                var newFlight =
                    _db.Flights.Include(f => f.Plane)
                        .Include(f => f.Plane.Seats)
                        .Include(f => f.SeatReservations)
                        .Include(f => f.Route)
                        .Include(f => f.Route.To)
                        .First(f => f.ID == flight.ID);

                var airport = _airports.First(a => a.Routes.Select(r => r.ID == newFlight.Route.ID).Any());
                var route = airport.Routes[airport.Routes.FindIndex(r => r.ID == newFlight.Route.ID)];

                if (route.Flights.Any(f => f.ID == newFlight.ID)) {
                    route.Flights[route.Flights.FindIndex(f => f.ID == newFlight.ID)] = newFlight;
                } else {
                    route.Flights.Add(newFlight);
                }

                // ##### Add / Edit Airport List End #####

                // ###### Add / Edit Matrix #####

                //var edgeIndex = _edges.FindIndex(e => e.Data.ID == airport.ID);
                var edge = GenerateEdge(airport);
                var oldEdge = _edges.SingleOrDefault(e => e.Data.ID == edge.Data.ID);

                if (oldEdge != null) {
                    _edges.Remove(oldEdge);
                }

                _edges.Add(edge);


                // ##### ADD / Edit Matrix End #####
#if DEBUG
                // ####### Timing #######

                watch.Stop();
                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- AddFlight Ended ---------");

                // ####### Timing End #######
#endif
            } catch (Exception) {
                HardReset();
            }
        }

        #endregion

        #region Update (Events)

        private void UpdateFlight(Flight flight) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######

                    Trace.WriteLine("----------- UpdateFlight -----------");
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

                    var airport = _airports.Single(a => a.Routes.Any(r => r.ID == newFlight.Route.ID));
                    var route = airport.Routes[airport.Routes.FindIndex(r => r.ID == newFlight.Route.ID)];
                    route.Flights[route.Flights.FindIndex(f => f.ID == newFlight.ID)] = newFlight;

                    //var edgeIndex = _edges.FindIndex(e => e.Data.ID == airport.ID);
                    var edge = GenerateEdge(airport);

                    if (edge != null) {
                        _edges.ReplaceWithHash(edge);
                    } else {
                        _edges.RemoveWhere(e => e.Data.ID == airport.ID);
                    }
#if DEBUG
                    // ####### Timing #######

                    watch.Stop();
                    Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                    Trace.WriteLine("-------- UpdateFlight Ended ---------");

                    // ####### Timing End #######
#endif
                } catch (Exception) {
                    HardReset();
                }
            }
        }

        private void UpdateRoute(Route route) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######

                    Trace.WriteLine("----------- UpdateRoute -----------");
                    var watch = Stopwatch.StartNew();

                    // ####### Timing End #######
#endif
                    //var airport = route.From;
                    var edge = _edges.First(ed => ed.Data.ID == route.FromID);

                    if (edge != null) {

                        Vertex<Airport> vertex = new Vertex<Airport>(route.To, route.Price);
                        edge.Neighbors[edge.Neighbors.FindIndex(e => e.Data.ID == route.To.ID)] = vertex;

                        route = db.Routes.Include(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats))
                            .Include(r => r.To)
                            .Include(r => r.Flights.Select(f => f.SeatReservations)).First(r => r.ID == route.ID);

                        var ap = _airports.First(a => a.ID == route.FromID);
                        ap.Routes[ap.Routes.FindIndex(r => r.ID == route.ID)] = route;

                    }

#if DEBUG
                    // ####### Timing #######

                    watch.Stop();
                    Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                    Trace.WriteLine("-------- UpdateRoute Ended ---------");

                    // ####### Timing End #######
#endif

                } catch (Exception) {
                    HardReset();
                }
            }
        }

        private void UpdateAirport(Airport airport) {
            using (var db = new FlightDB()) {
                try {
#if DEBUG
                    // ####### Timing #######

                    Trace.WriteLine("----------- UpdateAirport -----------");
                    var watch = Stopwatch.StartNew();

                    // ####### Timing End #######
#endif

                    var newAirport = db.Airports.Include(
                        a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats)))
                        .Include(a => a.Routes.Select(r => r.To))
                        .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                        .First(a => a.ID == airport.ID);

                    if (!_airports.Contains(newAirport)) {
                        _airports.Add(newAirport);
                    } else {
                        _airports.ReplaceWithHash(newAirport);
                    }



                    foreach (var ap in _airports) {
                        var toRoutes = ap.Routes.Where(r => r.To.ID == newAirport.ID).ToList();
                        if (toRoutes.Any()) {
                            foreach (var toRoute in toRoutes) {
                                toRoute.To = newAirport;
                            }
                        }

                        var fromRoutes = ap.Routes.Where(r => r.From.ID == newAirport.ID).ToList();
                        if (fromRoutes.Any()) {
                            foreach (var fromRoute in fromRoutes) {
                                fromRoute.From = newAirport;
                            }
                        }
                    }

                    var edge = GenerateEdge(newAirport);

                    if (edge != null) {
                        _edges.ReplaceWithHash(edge);
                    }

#if DEBUG
                    // ####### Timing #######

                    watch.Stop();
                    Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                    Trace.WriteLine("-------- UpdateAirport Ended ---------");

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

        #endregion

        #region Constructor

        private Matrix() {
#if DEBUG
            Trace.WriteLine("----------- Constructor -----------");
            var watch = Stopwatch.StartNew();
#endif

            IQueryable<Airport> fromAirports = _db.Airports
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats)))
                .Include(a => a.Routes.Select(r => r.To))
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                .Where(a => a.Routes.Any(r => r.Flights.Any(f => f.SeatReservations.Count < f.Plane.Seats.Count)));

            HashSet<Airport> tmpAirports = fromAirports.ToHashSet();
            var toAirports = tmpAirports.SelectMany(r => r.Routes.Select(a => a.To)).ToHashSet();
            tmpAirports.UnionWith(toAirports);

            _airports = tmpAirports;

#if DEBUG
            Trace.WriteLine("airports: " + _airports.ToArray());
            Trace.WriteLine("toAirports: " + toAirports);
            Trace.WriteLine("toAirports Count: " + toAirports.Count());
#endif

            GenerateEdges();

#if DEBUG
            Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
            Trace.WriteLine("-------- Constructor Ended ---------");
#endif
        }

        #endregion

        #region EdgeGeneration

        private Edge<Airport> GenerateEdge(Airport airport) {
            var edge = new Edge<Airport>(airport);
            List<Route> routes = new List<Route>();

            try {
                routes = _rs.GetRoutesByAirport(airport);
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
            }
#if DEBUG
            if (routes.Count == 0) {
                Trace.WriteLine("Route Count == 0, at " + airport.Name + " GenerateEdge");
            }
#endif

            if (routes.Count > 0) {
                edge.Neighbors = routes.Select(route => new Vertex<Airport>(route.To, route.Price)).ToList();
                return edge;
            }

            return null;
        }

        private void GenerateEdges() {
            foreach (var airport in _airports) {
                var edge = new Edge<Airport>(airport);
                List<Route> routes = new List<Route>();

                try {
                    routes = _rs.GetRoutesByAirport(airport);
                } catch (Exception) { }

                if (routes.Count > 0) {
                    edge.Neighbors = routes.Select(route => new Vertex<Airport>(route.To, route.Price)).ToList();
                    _edges.Add(edge);
                }
            }
        }

        #endregion

        #region ShortestPath

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="LockedException"> Thrown if the Matrix is locked over 15 seconds. </exception>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="seats"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public List<Flight> GetShortestPath(int fromId, int toId, int seats, DateTime startTime) {
            long t = 0;


            int timer = 0;
            while (_locked) {
                Thread.Sleep(1000);
                timer += 1000;

                if (timer > 15000) {
                    throw new LockedException("The Matrix is being updated");
                }
            }
            var inc = i++;
            running.Add(inc);

            Airport from = _airports.FirstOrDefault(a => a.ID == fromId);

#if DEBUG
            Trace.WriteLine(String.Format("From Airport: {0} | To AirportID: {1} | AirportCount: {2}", from, toId, _airports.Count));
#endif

            if (from == null || toId < 0) {
                throw new FaultException<NullPointerFault>(new NullPointerFault() { Message = "Airports cant be null" });
            }

            var times = new ConcurrentDictionary<Airport, DateTime>();
            times[from] = startTime;

            var path = new List<Flight>();
            var distance = new ConcurrentDictionary<Airport, decimal>();
            var previous = new ConcurrentDictionary<Airport, Airport>();
            var routedFlights = new ConcurrentDictionary<Route, Flight>();
            var nodes = new List<Airport>();

            SetDefaultEdgeValue(distance, nodes, from);

            while (nodes.Count != 0) {
                nodes.Sort((x, y) => (int)Math.Round(distance[x]) - (int)Math.Round(distance[y]));

                var smallest = nodes[0];

#if DEBUG
                Trace.WriteLine(String.Format("smallestID: {0} | toID: {1}", smallest.ID, toId));
#endif

                nodes.Remove(smallest);

                if (smallest.ID == toId) {
                    if (previous.Count > 0) {
                        while (previous.ContainsKey(smallest)) {
                            Route route = previous[smallest].GetRouteTo(smallest);

                            path.Add(routedFlights[route]);
                            smallest = previous[smallest];
                        }
                    } else {
                        Route route = from.GetRouteTo(smallest);
                        if (route != null) { // && routedFlights.Count > 0
                            path.Add(routedFlights[route]);
                        }
                    }

                    break;
                }

                if (distance[smallest] == decimal.MaxValue) {
                    break;
                }


#if DEBUG
                // ####### Timing #######

                Trace.WriteLine("----------- ShortestPath -----------");
                var watch = Stopwatch.StartNew();

                // ####### Timing End #######
#endif

                #region Parallel (Incomplete)
                /*
                Parallel.ForEach(_edges.Single(e => e.Data.ID == smallest.ID).Neighbors, (neighbor, loopState) => {
                    var alt = distance[smallest] + neighbor.Price;

                    try {
                        var time = times[smallest];
#if DEBUG
                        Trace.WriteLine(String.Format("From {0} -> To {1}", smallest, neighbor.Data));
#endif
                        Route route = smallest.GetRouteTo(neighbor.Data);
                        List<Flight> flights = route.Flights;
                        flights.Sort(new Comparison<Flight>((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                        var flight = flights.FirstOrDefault(dep => dep.DepartureTime.TimeOfDay > time.TimeOfDay);
#if DEBUG
                        if (flight != null) {
                            Trace.WriteLine(String.Format("Flight: {0} | Current Time: {1} | Departure Time: {2} | Arrival Time: {3}", flight.ID, time.TimeOfDay, flight.DepartureTime.TimeOfDay, flight.ArrivalTime.TimeOfDay));
                        }
#endif

                        if ((flight.SeatReservations.Count + seats) < flight.Plane.Seats.Count) {

                            if (alt < distance[neighbor.Data]) {
                                times[neighbor.Data] = flight.ArrivalTime;
                                distance[neighbor.Data] = alt;
                                previous[neighbor.Data] = smallest;
                                routedFlights[route] = flight;
                                loopState.Stop();
                            }
                        }
#if DEBUG
 else {
                            Trace.WriteLine(String.Format("SeatReservations: {0} | Flight Seats: {1} | Not enough Seats on {2}", (flight.SeatReservations.Count + seats), flight.Plane.Seats.Count, flight.ID));
                        }
#endif


                    } catch
                        (KeyNotFoundException) { } catch
                        (NullReferenceException) { }

                });
                */
                #endregion

                foreach (var neighbor in _edges.Single(e => e.Data.ID == smallest.ID).Neighbors) {
                    var alt = distance[smallest] + neighbor.Price;


                    try {
                        if (alt < distance[neighbor.Data]) {
                            var time = times[smallest];
#if DEBUG
                            Trace.WriteLine(String.Format("From {0} -> To {1}", smallest, neighbor.Data));
#endif
                            Route route = smallest.GetRouteTo(neighbor.Data);
                            List<Flight> flights = route.Flights;
                            flights.Sort(new Comparison<Flight>((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                            var flight = flights.FirstOrDefault(dep => dep.DepartureTime.TimeOfDay > time.TimeOfDay);
#if DEBUG
                            if (flight != null) {
                                Trace.WriteLine(String.Format("Flight: {0} | Current Time: {1} | Departure Time: {2} | Arrival Time: {3}", flight.ID, time.TimeOfDay, flight.DepartureTime.TimeOfDay, flight.ArrivalTime.TimeOfDay));
                            }
#endif

                            if ((flight.SeatReservations.Count + seats) < flight.Plane.Seats.Count) {


                                times[neighbor.Data] = flight.ArrivalTime;
                                distance[neighbor.Data] = alt;
                                previous[neighbor.Data] = smallest;
                                routedFlights[route] = flight;


                            }

#if DEBUG
 else {
                                Trace.WriteLine(String.Format("SeatReservations: {0} | Flight Seats: {1} | Not enough Seats on {2}", (flight.SeatReservations.Count + seats), flight.Plane.Seats.Count, flight.ID));
                            }
#endif

                        }
                    } catch
                        (KeyNotFoundException) { } catch
                        (NullReferenceException) { }


                }

#if DEBUG
                // ####### Timing #######

                watch.Stop();
                Trace.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
                Trace.WriteLine("-------- ShortestPath Ended ---------");
                t += watch.ElapsedMilliseconds;
                // ####### Timing End #######
#endif
            }
#if DEBUG
            Trace.WriteLine("path: " + path.ToArray().ToString());
            Trace.WriteLine("path count: " + path.Count);
#endif
            path = CleanUp(path);
            running.Remove(inc);

            Trace.WriteLine("\nTime: " + t + "ms\n");
            Trace.WriteLine("-------- ShortestPath Ended ---------");

            return path;
        }

        private void SetDefaultEdgeValue(ConcurrentDictionary<Airport, decimal> distance, List<Airport> nodes, Airport from) {
            foreach (var edge in _edges) {
                if (edge.Data.ID == from.ID) {
                    distance[edge.Data] = 1;
                } else {
                    distance[edge.Data] = int.MaxValue;
                }

                nodes.Add(edge.Data);
            }
        }

        private List<Flight> CleanUp(List<Flight> flights) {
            using (var db = new FlightDB()) {
                var listOfIds = flights.Select(f => f.ID);

                var retFlights = db.Flights
                .Include(f => f.Plane)
                .Include(f => f.Route.From)
                .Include(f => f.Route.To)
                .Where(f => listOfIds.Contains(f.ID)).OrderBy(f => f.DepartureTime).ToList();

                return retFlights;
            }
        }

        #endregion
    }

    /*

    public class Matrix {

        private readonly FlightDB _db = new FlightDB();
        private List<Vertex<Airport>> edges = new List<Vertex<Airport>>();
        private int[,] _matrix;
        private static Matrix _instance;

        public bool Initializing { get; set; }

        private Matrix() {
            Initializing = true;
            Initialize();
            Initializing = false;
        }

        public static Matrix GetInstance() {
            return _instance ?? (_instance = new Matrix());
        }

        public int[,] GetMatrix() {
            if (Initializing) {
                return _matrix;
            } else {
                throw new Exception("Locked");
            }
        }

        #region Init

        private void Initialize() {

            List<Airport> nodes = _db.Airports.OrderBy(n => n.ID).ToList();

            _matrix = new int[nodes.Count, nodes.Count];

            int i = 0;
            foreach (var airport in nodes) {
                edges.Add(new Vertex<Airport>(i, airport.ID, airport));
                i++;
            }

            for (int j = 0; j < nodes.Count; j++) {
                Airport from = edges[j].Data;
                for (int k = 0; k < nodes.Count; k++) {
                    Airport to = edges[k].Data;
                    _matrix[j, k] = HasRoute(from, to);
                }
            }

            
        }

        private int HasRoute(Airport from, Airport to) {
            int ret = 0;

            RouteService rs = new RouteService();
            try {
                var r = rs.GetRouteByAirports(from, to);
                ret = 1;
            } catch (FaultException<NullPointerFault> fault) {}


            return ret;
        }

        #endregion

    }
     * */

}