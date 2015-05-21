using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService {
    public partial class Ticket {

        public decimal TotalPrice {
            get {
                try {
                    return SeatReservations.Sum(s => s.Flight.Price);
                } catch (Exception) {
                    return -1;
                }
            } //TODO Sætte price på seatReservation
        }

        public TimeSpan TotalTravelTime {
            get {
                try {
                    var min = SeatReservations.Max(x => x.Flight.ArrivalTime);
                    var max = SeatReservations.Min(x => x.Flight.DepartureTime);
                    return max - min;
                } catch (Exception) {
                    return new TimeSpan(-1);
                }
                
            }
        }
    }
}