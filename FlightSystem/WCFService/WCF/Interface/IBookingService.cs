using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IBookingService {

        [OperationContract]
        int AddOne();

        [OperationContract]
        List<Flight> GetFlights(int fromId, int toId, int seats, DateTime dateTime);

        [OperationContract]
        List<SeatReservation> MakeSeatsOccupiedRandom(List<Flight> flights, int seats);

        [OperationContract]
        void Complete();

    }
}