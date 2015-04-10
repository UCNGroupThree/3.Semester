using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IAirportService { //TODO Lasse

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseInsertFault))]
        [FaultContract(typeof(TimeZoneFault))]
        [FaultContract(typeof(AlreadyExistFault))]
        Airport AddAirport(Airport airport);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        [FaultContract(typeof(TimeZoneFault))]
        [FaultContract(typeof(AlreadyExistFault))]
        Airport UpdateAirport(Airport airport);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseDeleteFault))]
        void DeleteAirport(Airport airport);

        [OperationContract]
        Airport GetAirport(int id);

        [OperationContract]
        List<Airport> GetAirportsByCountry(string country);

        [OperationContract]
        List<Airport> GetAirportsByCity(string city);

        [OperationContract]
        List<Airport> GetAirportsByName(string name);

        [OperationContract]
        List<Airport> GetAirportsByShortName(string shortName, bool equalsTo);

        [OperationContract]
        List<string> GetCountries();

    }
}