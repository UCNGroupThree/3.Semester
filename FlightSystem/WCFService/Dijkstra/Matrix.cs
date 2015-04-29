using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Faults;

namespace WCFService.Dijkstra {

    public class Matrix {

        private readonly FlightDB _db = new FlightDB();
        private readonly RouteService rs = new RouteService();
        private List<Edge<Airport>> edges = new List<Edge<Airport>>();
        private List<Airport> airports;

        public Matrix() {
            IQueryable<Airport> fromAirports = _db.Airports
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.Plane).Select(s => s.Seats)))
                .Include(a => a.Routes.Select(r => r.To))
                .Include(a => a.Routes.Select(r => r.Flights.Select(f => f.SeatReservations)))
                .Where(a => a.Routes.Any(r => r.Flights.Any(f => f.SeatReservations.Count < f.Plane.Seats.Count)));

            //var toAirports = airports.Select(r => r.Routes.Select(a => a.To));
            airports = fromAirports.ToList();
            var toAirports = airports.SelectMany(r => r.Routes.Select(a => a.To)).Where(a => !airports.Contains(a)).ToList();
            airports.AddRange(toAirports);
            //IQueryable<Airport> query = fromAirports.Union(toAirports);
            //airports = query.ToList();

            Debug.WriteLine("airports: " + airports.ToArray());
            Debug.WriteLine("toAirports: " + toAirports);
            Debug.WriteLine("toAirports Count: " + toAirports.Count());
            //airports.AddRange();
            //IQueryable<Route> query = db.Routes..Where(route => route.Flights);

            //List<Airport> nodes = _db.Airports.OrderBy(n => n.ID).Include(n => n.Routes.Select(a => a.Flights.Select(f => f.Plane).Select(s => s.Seats))).ToList();
            //nodes = nodes.Where(n => n.Routes.Select(r => r.Flights).Any()).ToList();
            foreach (var airport in airports) {
                var edge = new Edge<Airport>(airport);

                List<Route> routes = new List<Route>();

                try {
                    routes = rs.GetRoutesByAirport(airport);
                } catch (Exception) { }

                if (routes.Count > 0) {
                    var neighbors = new List<Vertex<Airport>>();

                    foreach (var route in routes) {
                        neighbors.Add(new Vertex<Airport>(route.To, route.Price));
                    }

                    edge.Neighbors = neighbors;


                    edges.Add(edge);
                }
            }

        }

        public List<Flight> GetShortestPath(int fromId, int toId, int seats, DateTime startTime) {
            //var time = startTime;
            Airport from = airports.FirstOrDefault(a => a.ID == fromId);
            Debug.WriteLine("from: "+from);
            Debug.WriteLine("airports: " + airports.ToArray().ToString());
            Debug.WriteLine("airports count: " + airports.Count);
            if (from == null || toId < 0) {
                throw new FaultException<NullPointerFault>(new NullPointerFault() { Message = "Airports cant be null" });
            }

            var times = new Dictionary<Airport, DateTime>();
            times[from] = startTime;

            //int id = from.ID;
            //from = _db.Airports.Where(a => a.ID == id).Include(a => a.Routes).SingleOrDefault();
            var path = new List<Flight>();
            var distance = new Dictionary<Airport, decimal>();
            var previous = new Dictionary<Airport, Airport>();
            var routedFlights = new Dictionary<Route, Flight>();
            var nodes = new List<Airport>();

            foreach (var edge in edges) {
                if (edge.Data.ID == from.ID) {
                    distance[edge.Data] = 1;
                } else {
                    distance[edge.Data] = int.MaxValue;
                }

                nodes.Add(edge.Data);
            }
            while (nodes.Count != 0) {
                nodes.Sort((x, y) => (int)Math.Round(distance[x]) - (int)Math.Round(distance[y]));

                var smallest = nodes[0];
                Debug.WriteLine("smallest: {0}, toID: {1}", smallest.ID, toId);
                nodes.Remove(smallest);
                //LasseGO
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

                foreach (var neighbor in edges.Single(e => e.Data.ID == smallest.ID).Neighbors) {
                    var alt = distance[smallest] + neighbor.Price;
                    Flight flight = null;


                    try {

                        var time = times[smallest];
                        Debug.WriteLine(smallest.ID + " -> " + neighbor.Data.ID);
                        //if (smallest.ID == 5) {
                        //    Console.Write("hi");
                        //}
                        Route route = smallest.GetRouteTo(neighbor.Data);
                        List<Flight> flights = route.Flights;
                        flights.Sort(new Comparison<Flight>((x, y) => DateTime.Compare(x.DepartureTime, y.DepartureTime)));
                        flight = flights.FirstOrDefault(dep => dep.DepartureTime.TimeOfDay > time.TimeOfDay);
                        Debug.WriteLine("Flight: not null? = " + flight);
                        Debug.WriteLine("Current Time: " + time.TimeOfDay);
                        Debug.WriteLine("Departure Time: " + flight.DepartureTime.TimeOfDay);
                        Debug.WriteLine("Arrival Time: " + flight.ArrivalTime.TimeOfDay);


                        //if (flight != null) {

                        if (alt < distance[neighbor.Data]) {
                            times[neighbor.Data] = flight.ArrivalTime;
                            distance[neighbor.Data] = alt;
                            previous[neighbor.Data] = smallest;
                            routedFlights[route] = flight;
                        }

                        //}

                    } catch
                        (KeyNotFoundException) { } catch
                        (NullReferenceException) { }

                }
            }
            Debug.WriteLine("path: " + path.ToArray().ToString());
            Debug.WriteLine("path count: " + path.Count);

            path.Reverse();

            InsertSeatReservation(path);

            return path;
        }

        private void InsertSeatReservation(List<Flight> path) {

            path.ForEach(t => Debug.WriteLine("#{0} PlaneID: {1}, seatCount: {2}", t.ID, t.Plane.ID, t.Plane.Seats.Count));
        //    try {
        //        Seat seat = path.First().Plane.Seats.First();
        //        Debug.WriteLine("Seat: #" + seat.ID);
        //        SeatReservation sr = new SeatReservation(SeatState.Taken, "Hans", seat, path.First());
        //        path.First().SeatReservations.Add(sr);
        //        //_db.SeatReservations.Attach(sr);
        //        _db.SaveChanges();
        //    } catch (Exception e) {
        //        Debug.WriteLine(e);
        //    }
        //    Debug.WriteLine("### DONE ####");
        }
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
                throw new Exception("Locked"); //TODO Locked Exception?
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