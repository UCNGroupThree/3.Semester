using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService {
    public partial class Ticket {

        public decimal TotalPrice {
            get {
                try {
                    return SeatReservations.Sum(s => s.Price);
                } catch (Exception) {
                    return -1;
                }
            }
        }

        public TimeSpan TotalTravelTime {
            get {
                try {
                    var min = SeatReservations.Min(x => x.Flight.DepartureTime);
                    var max = SeatReservations.Max(x => x.Flight.ArrivalTime);
                    return max - min;
                } catch (Exception) {
                    return new TimeSpan(-1);
                }
                
            }
        }
    }
}