using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using WCFService.Model;

namespace WCFService.Dijkstra {
    public class Vertex<T> {

        public T Data { get; set; }

        public decimal Price { get; set; }

        public Vertex(T data, decimal price) {
            Data = data;
            Price = price;
        }

        public override string ToString() {
            return "Vertex: " + Data.ToString();
        }
    }

    public class Edge<T> {
        public T Data { get; set; }

        public List<Vertex<T>> Neighbors { get; set; }

        public Edge(T data) {
            Data = data;
        }

        public override bool Equals(object obj) {
            var oo = obj.GetHashCode();

            return this.GetHashCode() == oo;
        }

        public override int GetHashCode() {
            return Data.GetHashCode();
        }

        public override string ToString() {
            return "Edge: " + Data.ToString() + " HashCode: " + GetHashCode();
        }
    }
}