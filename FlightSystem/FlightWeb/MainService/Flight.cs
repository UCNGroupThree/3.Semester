using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService {
    public partial class Flight {
        public Airport From {
            get {
                if (Route != null) {
                    return Route.From;
                }
                return null;
            }
        }

        public Airport To {
            get {
                if (Route != null) {
                    return Route.To;
                }
                return null;
            }
        }

        public TimeSpan TimeSpent {
            get { return ArrivalTime - DepartureTime; }
        }

        public decimal Price {
            get {
                if (Route != null) {
                    return Route.Price;
                }
                return -1;
            }
        }
    }
}