using System.Collections.Generic;
using System.Security.AccessControl;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private static Matrix matrix = new Matrix();

        public List<Route> DijkstraStuff(Airport from, Airport to) {
           // matrix = new Matrix();

            List<Route> ret = matrix.GetShortestPath(from, to);


            return ret;
        }
    }
}