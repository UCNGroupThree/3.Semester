using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IRouteService { //TODO Jakob
        [OperationContract]
        int AddRoute(Route route);

        [OperationContract]
        void UpdateRoute(Route route);

        [OperationContract]
        void DeleteRoute(Route route);

        [OperationContract]
        Route GetRoute(int id);
    }
}