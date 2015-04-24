using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private static Matrix matrix = new Matrix();

        public List<Flight> DijkstraStuff(Airport from, Airport to, DateTime startTime) {
           // matrix = new Matrix();

            List<Flight> ret = matrix.GetShortestPath(from, to, startTime);


            return ret;
        }
    }
}