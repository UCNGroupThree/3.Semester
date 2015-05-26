using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace FlightWeb.MainService {
    public partial class Flight {

        public TimeSpan TimeSpent {
            get { return ArrivalTime - DepartureTime; }
        }
        public decimal TotalReservationPrice {
            get {
                if (SeatReservations != null) {
                    return SeatReservations.Sum(sr => sr.Price);
                }
                return -1;
            }
        }
        public string NiceTimeSpent {
            get {
                try {
                    return TimeSpent.ToFineString();
                } catch (Exception) {
                    return null;
                }
            }
        }

    }
}