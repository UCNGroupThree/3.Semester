using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IAirportService {

        [OperationContract]
        int AddAirport(Airport airport);

        [OperationContract]
        void UpdateAirport(Airport airport);

        [OperationContract]
        void DeleteAirport(Airport airport);

        [OperationContract]
        Airport GetAirport(int id);


    }
}