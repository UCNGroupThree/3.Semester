using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    public interface IDijkstra {

        [OperationContract]
        [FaultContract(typeof(DijkstraFault))]
        List<Flight> GetShortestPath(int from, int to, int seats, DateTime dt);

    }
}