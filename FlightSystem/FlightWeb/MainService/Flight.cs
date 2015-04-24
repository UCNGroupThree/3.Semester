using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService {
    public partial class Flight {
        public Airport From {
            get { return Route.From; }
        }

        public Airport To {
            get { return Route.To; }
        }

        public TimeSpan TimeSpent {
            get { return ArrivalTime - DepartureTime; }
        }

        public decimal Price {
            get { return Route.Price; }
        }
    }
}