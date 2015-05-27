using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IFlightService {

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseInsertFault))]
        int AddFlight(Flight flight);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        Flight UpdateFlight(Flight flight);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseDeleteFault))]
        void DeleteFlight(Flight flight);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        Flight GetFlight(int id);

        [OperationContract]
        [FaultContract(typeof (NullPointerFault))]
        List<Flight> GetFlights(Airport from, Airport to);

    }
}