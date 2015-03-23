using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Faults;

namespace WCFService.Dijkstra {

    public class Matrix {

        private readonly FlightDB _db = new FlightDB();
        private readonly RouteService rs = new RouteService();
        private List<Edge<Airport>> edges = new List<Edge<Airport>>();

        public Matrix() {
            List<Airport> nodes = _db.Airports.OrderBy(n => n.ID).ToList();

            foreach (var airport in nodes) {
                var edge = new Edge<Airport>(airport);
                var routes = rs.GetRoutesByAirport(airport);
                var neighbors = new List<Vertex<Airport>>();

                foreach (var route in routes) {
                    neighbors.Add(new Vertex<Airport>(route.To, 100)); //TODO price? how >.<
                }

                edge.Neighbors = neighbors;

                edges.Add(edge);
            }
        }

        public List<Airport> GetShortestPath(Airport from, Airport to) {
            var path = new List<Airport>();
            var distance = new Dictionary<Airport, double>();
            var previous = new Dictionary<Airport, Airport>();
            var nodes = new List<Airport>();

            foreach (var edge in edges) {
                if (edge.Data == from) {
                    distance[edge.Data] = 0;
                } else {
                    distance[edge.Data] = double.MaxValue;
                }

                nodes.Add(edge.Data);
            }

            while (nodes.Count != 0) {
                nodes.Sort((x, y) => (int)Math.Round(distance[x]) - (int)Math.Round(distance[y]));

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == to) {
                    while (previous.ContainsKey(smallest)) {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distance[smallest] == double.MaxValue) {
                    break;
                }

                foreach (var neighbor in edges.Single(e => e.Data == smallest).Neighbors) {
                    var alt = distance[smallest] + neighbor.Price;
                    if (alt < distance[neighbor.Data]) {
                        distance[neighbor.Data] = alt;
                        previous[neighbor.Data] = smallest;
                    }
                }
            }


            return path;
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