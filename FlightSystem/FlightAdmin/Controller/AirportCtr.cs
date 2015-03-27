﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using FlightAdmin.Exceptions;
using FlightAdmin.GUI;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class AirportCtr {

        #region Create / Update / Delete

        public Airport CreateAirport(string name, string shortName, string city, string country, decimal latitude, decimal longtitude, int altitude, string timeZone) {
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
            } catch (FaultException<DatabaseInsertFault> dbException) {
                throw new DatabaseException(dbException.Message);
            } catch (Exception ex) {
                airport = null;
                Console.WriteLine(@"CreateAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return airport;
        }

        public Airport UpdateAirport(Airport airport, string name, string shortName, string city, string country, decimal latitude, decimal longtitude, int altitude, string timeZone) {
            try {
                airport.Name = name;
                airport.ShortName = shortName;
                airport.City = city;
                airport.Country = country;
                airport.Latitude = latitude;
                airport.Longtitude = longtitude;
                airport.Altitude = altitude;
                airport.TimeZone = timeZone;
                using (AirportServiceClient client = new AirportServiceClient()) {
                    airport = client.UpdateAirport(airport);
                }

            } catch (FaultException<NullPointerFault> ex) {
                throw new NullException(ex.Message);
            } catch (FaultException<DatabaseUpdateFault> ex) {
                throw new DatabaseException(ex.Message);
            } catch (Exception ex) {
                airport = null;
                Console.WriteLine(@"UpdateAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return airport;
        }

        public void DeleteAirport(Airport airport) {
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    client.DeleteAirport(airport);
                }
            } catch (FaultException<NullPointerFault> ex) {
                throw new NullException(ex.Message);
            } catch (FaultException<DatabaseDeleteFault> ex) {
                throw new DatabaseException(ex.Message);
            } catch (Exception ex) {
                Console.WriteLine(@"DeleteAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }
        }

        #endregion

        #region read

        public Airport GetAirport(int id) {
            Airport airport;
            try {
                using (var client = new AirportServiceClient()) {
                    airport = client.GetAirport(id);
                }
            } catch (Exception ex) {
                Console.WriteLine(@"GetAirport Exception: " + ex);
                throw new ConnectionException("WCF Service Exception", ex);
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
                Console.WriteLine(@"GetAirportsByCountry Exception: " + ex);
                throw new ConnectionException("WCF Service Exception", ex);
            }
            return list;
        }

        public List<Airport> GetAirportsByCity(string city) {
            List<Airport> list;
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    list = client.GetAirportsByCity(city);
                }
            } catch (Exception ex) {
                Console.WriteLine(@"GetAirportsByCity Exception: " + ex);
                throw new ConnectionException("WCF Service Exception", ex);
            }
            return list;
        }

        public List<Airport> GetAirportsByName(string name) {
            List<Airport> list;
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    list = client.GetAirportsByName(name);
                }
            } catch (Exception ex) {
                Console.WriteLine(@"GetAirportsByName Exception: " + ex);
                throw new ConnectionException("WCF Service Exception", ex);
            }
            return list;
        }

        public List<Airport> GetAirportsByShortName(string shortName) {
            List<Airport> list;
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    list = client.GetAirportsByShortName(shortName);
                }
            } catch (Exception ex) {
                Console.WriteLine(@"GetAirportsByShortName Exception: " + ex);
                throw new ConnectionException("WCF Service Exception", ex);
            }
            return list;
        }

        public List<string> GetCountries() {
            List<string> con;

            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    con = client.GetCountries();
                }
            } catch (Exception e) {
                Console.WriteLine(@"GetCountries Exception: " + e);
                throw new ConnectionException("WCF Service Exception", e);
            }

            return con;
        }

        #endregion

    }
}