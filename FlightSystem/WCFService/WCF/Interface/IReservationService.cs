using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract (SessionMode = SessionMode.Required)]
    public interface IReservationService {

        [OperationContract(IsInitiating = true, IsTerminating = false)]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(NotSameObjectFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(NotEnouthFault))]
        [FaultContract(typeof(DatabaseFault))]
        Ticket MakeSeatsOccupiedRandom(List<Flight> flights, int noOfSeats, User user);

        [OperationContract (IsInitiating = false, IsTerminating = true)]
        void Cancel();

        [OperationContract (IsInitiating = false,IsTerminating = true)]
        [FaultContract(typeof(AlreadyExistFault))]
        [FaultContract(typeof(ArgumentFault))]
        [FaultContract(typeof(DatabaseFault))]
        void Complete();
    }
}