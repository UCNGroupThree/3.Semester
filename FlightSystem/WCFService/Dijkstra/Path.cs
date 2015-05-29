using WCFService.Model;

namespace WCFService.Dijkstra {
    public class Path {
        public Airport From { get; set; }
        public Airport To { get; set; }

        public Route Route { get; set; }

        public Flight FinalFlight { get; set; }

        public override string ToString() {
            return From.ToString() + " -> " + To.ToString();
        }
    } 
}