using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IRouteService { //TODO Jakob
        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseInsertFault))]
        Route AddRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        Route UpdateRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseDeleteFault))]
        void DeleteRoute(Route route);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        Route GetRoute(int id);
    }
}