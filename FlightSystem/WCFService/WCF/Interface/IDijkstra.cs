﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    public interface IDijkstra {

        [OperationContract]
        [FaultContract(typeof(LockedFault))]
        List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime);

        [OperationContract]
        List<Flight> DijkstraTest(int from, int to, int seats, DateTime dt);

    }
}