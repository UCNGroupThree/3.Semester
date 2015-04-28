using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    public interface IDijkstra {

        [OperationContract]
        List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime);

    }
}