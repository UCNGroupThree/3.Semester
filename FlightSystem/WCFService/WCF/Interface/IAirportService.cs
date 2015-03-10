using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IAirportService { //TODO Lasse

        [OperationContract]
        int AddAirport(Airport airport);

        [OperationContract]
        Airport UpdateAirport(Airport airport);

        [OperationContract]
        void DeleteAirport(Airport airport);

        [OperationContract]
        Airport GetAirport(int id);

        [OperationContract]
        List<Airport> GetAirportsByCountry(string country);

        [OperationContract]
        List<Airport> GetAirportsByCity(string city);

        [OperationContract]
        List<Airport> GetAirportsByName(string name);

    }
}