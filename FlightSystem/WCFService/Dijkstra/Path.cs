using System.Diagnostics;
using WCFService.Model;

namespace WCFService.Dijkstra {
    [DebuggerDisplay("{From.ToString()} -> {To.ToString()}")]
    public class Path {
        public Airport From { get; set; }
        public Airport To { get; set; }

        public Route Route { get; set; }

        public Flight FinalFlight { get; set; }
    } 
}