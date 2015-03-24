using System;
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
            } catch (Exception) {
                //TODO Exception Handler
                throw;
            }

            return airport;
        }
        

    }
}