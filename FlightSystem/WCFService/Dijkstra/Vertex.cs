using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace WCFService.Dijkstra {
    public class Vertex<T> {

        public T Data { get; set; }

        public double Price { get; set; }

        public Vertex(T data, double price) {
            Data = data;
            Price = price;
        } 
    }

    public class Edge<T> {
        public T Data { get; set; }

        public List<Vertex<T>> Neighbors { get; set; }

        public Edge(T data) {
            Data = data;
        } 
    }
}