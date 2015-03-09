using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IFlightService {

        [OperationContract]
        int AddFlight(Flight flight);

        [OperationContract]
        void UpdateFlight(Flight flight);

        [OperationContract]
        void DeleteFlight(Flight flight);

        [OperationContract]
        Flight GetFlight(int id);

    }
}