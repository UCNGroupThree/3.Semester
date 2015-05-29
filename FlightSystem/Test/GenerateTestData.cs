using System;
using System.Collections.Generic;
using System.ServiceModel;
using Test.MainService;
namespace Test {
    public class GenerateTestData {

        private List<Airport> _airports;
        private Random rand;
        private int[] planeIDs = new[] {21, 22, 23, 24};

        public GenerateTestData() {
            rand = new Random();
            using (AirportServiceClient client = new AirportServiceClient()) {
                _airports = client.GetAirportsByCountry("Denmark");
            }


            for (int i = 0; i < 30; i++) {
                Airport a1 = GetRandomAirport();
                Airport a2 = GetRandomAirport();

                while (a1.ID == a2.ID) {
                    a2 = GetRandomAirport();
                }

                using (RouteServiceClient client = new RouteServiceClient()) {
                    try {
                        var routes = client.GetRouteByAirports(a1, a2);
                    } catch (FaultException<NullPointerFault>) {
                        var route = GenerateRoute(a1, a2);
                        try {
                            route = client.AddRoute(route);
                            route.Flights = GenerateFlights(route);

                            client.AddOrUpdateFlights(route);
                        } catch (Exception) {
                        }
                    }
                }
            }
        }

        private List<Flight> GenerateFlights(Route route) {
            List<Flight> flights = new List<Flight>();
            DateTime arr = new DateTime(2015, 05, 27, 10, 00, 00);
            DateTime dep = new DateTime(2015, 05, 27, 09, 00, 00);

            for (int i = 0; i < 6; i++) {
                flights.Add(new Flight() {
                    DepartureTime = dep,
                    ArrivalTime = arr,
                    PlaneID = planeIDs[rand.Next(0,4)],
                    RouteID = route.ID
                });

                arr = arr.AddHours(1);
                dep = dep.AddHours(1);
            }

            return flights;
        }

        private Route GenerateRoute(Airport from, Airport to) {
            Route r = new Route {FromID = from.ID, ToID = to.ID, Price = rand.Next(125, 1000)};

            return r;
        }

        private Airport GetRandomAirport() {
            int i = _airports.Count;
            int randInt = rand.Next(0, i);

            return _airports[randInt];
        }


    }
}