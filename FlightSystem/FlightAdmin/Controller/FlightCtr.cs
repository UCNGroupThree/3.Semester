using System;
using FlightAdmin.FlightService;

namespace FlightAdmin.Controller {
    public class FlightCtr {

        public Flight CreateFlight(DateTime arrival, DateTime departure, Plane plane, decimal price) {
            Flight flight = null;

            if (FlightValidation(arrival, departure, plane, price)) {
                flight = new Flight {ArrivalTime = arrival, DepartureTime = departure, Plane = plane, Price = price};

                try {
                    using (var client = new FlightServiceClient()) {
                        flight.ID = client.AddFlight(flight);
                    }
                } catch (Exception e) {
                    throw new Exception("WCF Service Exception"); //TODO Better Exception
                }

            } else {
                throw new Exception("FlightValidation Exception"); //TODO Better Exception
            }

            return flight;
        }

        public Flight UpdateFlight(Flight flight, DateTime arrival, DateTime departure, Plane plane, decimal price) {
           
            using (var client = new FlightServiceClient()) {
            
            }

            return flight;

        }



        private bool FlightValidation(DateTime arrival, DateTime departure, Plane plane, decimal price) {
            bool ret = (plane != null);

            return ret;
        }
    }
}