using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using WCFService.Dijkstra;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class Dijkstra : IDijkstra {

        private static Matrix matrix = new Matrix();

        public List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime) {
           // matrix = new Matrix();

            List<Flight> ret = matrix.GetShortestPath(fromId, toId, seats, startTime);


            return ret;
        }
    }
}