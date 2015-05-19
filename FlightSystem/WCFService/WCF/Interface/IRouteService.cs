using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IRouteService {

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseInsertFault))]
        [FaultContract(typeof(AlreadyExistFault))]
        Route AddRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        Route UpdateRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        Route AddOrUpdateFlights(Route route);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseDeleteFault))]
        [FaultContract(typeof(DeleteFault))]
        void DeleteRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        Route GetRoute(int id);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        Route GetRouteByAirports(Airport from, Airport to);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        List<Route> GetRoutesByAirport(Airport from);
    }
}