using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract (SessionMode = SessionMode.Required)]
    public interface IReservationService {
        //TODO Sætte IsInitiating og IsTermination på de manglende Operationer
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        [FaultContract(typeof(LockedFault))]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseFault))]
        List<Flight> GetFlightsAsd(int fromId, int toId, int seats, DateTime dateTime, User user);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(ArgumentFault))]
        [FaultContract(typeof(NotEnouthFault))]
        [FaultContract(typeof(DatabaseFault))]
        Ticket MakeSeatsOccupiedRandom();

        [OperationContract (IsInitiating = false, IsTerminating = true)]
        //TODO Måske fejl, hvis det ikke lykkes.
        void Cancel();

        [OperationContract (IsInitiating = false,IsTerminating = true)]
        [FaultContract(typeof(AlreadyExistFault))]
        [FaultContract(typeof(ArgumentFault))]
        [FaultContract(typeof(DatabaseFault))]
        void Complete();

    }
}