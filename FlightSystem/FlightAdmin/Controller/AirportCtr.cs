using System;
using System.Collections.Generic;
using FlightAdmin.GUI;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class AirportCtr {

        public Airport CreateAirport(string name, string shortName, string city, string country, decimal latitude, decimal longtitude, decimal altitude, string timeZone) {
            Airport airport;

            try {
                airport = new Airport {
                    Name = name,
                    ShortName = shortName,
                    City = city,
                    Country = country,
                    Latitude = latitude,
                    Longtitude = longtitude,
                    Altitude = altitude,
                    TimeZone = timeZone //TODO ASD ENTITY SEARCH
                };
                using (AirportServiceClient client = new AirportServiceClient()) {
                    airport.ID = client.AddAirport(airport);
                }
            } catch (Exception ex) {
                airport = null;
                Console.WriteLine(@"CreateAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return airport;
        }


        public List<Airport> GetAirportsByCountry(string country) {
            List<Airport> list;
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    list = client.GetAirportsByCountry(country);
                }
            } catch (Exception ex) {
                list = null;
                Console.WriteLine(@"GetAirportsByCountry Exception: " + ex);
                //TODO Exception Handler
                //throw;
            }
            return list;
        }

        

    }
}