﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            List<Airport> nodes = _db.Airports.OrderBy(n => n.ID).Include(n => n.Routes).ToList();

            foreach (var airport in nodes) {
                var edge = new Edge<Airport>(airport);

                List<Route> routes = new List<Route>();

                try {
                    routes = rs.GetRoutesByAirport(airport);
                } catch(Exception){}

                if(routes.Count > 0){
                    var neighbors = new List<Vertex<Airport>>();

                    foreach (var route in routes) {
                        neighbors.Add(new Vertex<Airport>(route.To, 100)); //TODO price? how >.<
                    }

                    edge.Neighbors = neighbors;


                    edges.Add(edge);
                }
            }

        }

        public List<Route> GetShortestPath(Airport from, Airport to) {
            int id = from.ID;
            from = _db.Airports.Where(a => a.ID == id).Include(a => a.Routes).SingleOrDefault();
            var path = new List<Route>();
            var distance = new Dictionary<Airport, double>();
            var previous = new Dictionary<Airport, Airport>();
            var nodes = new List<Airport>();

            foreach (var edge in edges) {
                if (edge.Data.ID == from.ID) {
                    distance[edge.Data] = 1;
                } else {
                    distance[edge.Data] = double.MaxValue;
                }

                nodes.Add(edge.Data);
            }

            while (nodes.Count != 0) {
                nodes.Sort((x, y) => (int)Math.Round(distance[x]) - (int)Math.Round(distance[y]));

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest.ID == to.ID) {
                    if (previous.Count > 0) {
                        while (previous.ContainsKey(smallest)) {
                            Route route = previous[smallest].GetRouteTo(smallest); // rs.GetRouteByAirports(smallest, previous[smallest])
                            path.Add(route);
                            smallest = previous[smallest];
                        }
                    } else {
                        Route route = from.GetRouteTo(smallest);
                        path.Add(route);
                    }

                    break;
                }

                if (distance[smallest] == double.MaxValue) {
                    break;
                }

                foreach (var neighbor in edges.Single(e => e.Data.ID == smallest.ID).Neighbors) {
                    var alt = distance[smallest] + neighbor.Price;
                    try {
                        if (alt < distance[neighbor.Data]) {
                            distance[neighbor.Data] = alt;
                            previous[neighbor.Data] = smallest;
                        }
                    } catch (KeyNotFoundException) { }
                }
            }

            path.Reverse();

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