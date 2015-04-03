using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using FlightAdmin.Exceptions;
using FlightAdmin.GUI;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class AirportCtr {

        #region Create / Update / Delete

        /// <exception cref="DatabaseException" />
        /// <exception cref="AlreadyExistException" />
        /// <exception cref="TimeZoneException" />
        /// <exception cref="Exception" />
        public Airport CreateAirport(string name, string shortName, string city, string country, double latitude, double longitude, double altitude, TimeZoneInfo timeZone) {
            Airport airport;

            try {
                airport = new Airport {
                    Name = name,
                    ShortName = shortName,
                    City = city,
                    Country = country,
                    Latitude = latitude,
                    Longitude = longitude,
                    Altitude = altitude,
                    TimeZone = timeZone //TODO ASD ENTITY SEARCH
                };
                using (AirportServiceClient client = new AirportServiceClient()) {
                    airport.ID = client.AddAirport(airport);
                }
            } catch (FaultException<DatabaseInsertFault> dbException) {
                throw new DatabaseException(dbException.Detail.Message);
            } catch (FaultException<AlreadyExistFault>) {
                throw new AlreadyExistException();
            } catch (FaultException<TimeZoneFault> ex) {
                throw new TimeZoneException(ex.Detail.Message);
            } catch (Exception ex) {
                airport = null;
                Console.WriteLine(@"CreateAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return airport;
        }

        /// <exception cref="NullException" />
        /// <exception cref="DatabaseException" />
        /// <exception cref="AlreadyExistException" />
        /// <exception cref="TimeZoneException" />
        /// <exception cref="Exception" />
        public Airport UpdateAirport(Airport airport, string name, string shortName, string city, string country, double latitude, double longitude, double altitude, TimeZoneInfo timeZone) {
            try {
                airport.Name = name;
                airport.ShortName = shortName;
                airport.City = city;
                airport.Country = country;
                airport.Latitude = latitude;
                airport.Longitude = longitude;
                airport.Altitude = altitude;
                airport.TimeZone = timeZone;
                using (AirportServiceClient client = new AirportServiceClient()) {
                    airport = client.UpdateAirport(airport);
                }

            } catch (FaultException<NullPointerFault> ex) {
                throw new NullException(ex.Detail.Message);
            } catch (FaultException<DatabaseUpdateFault> ex) {
                throw new DatabaseException(ex.Detail.Message);
            } catch (FaultException<AlreadyExistFault>) {
                throw new AlreadyExistException();
            } catch (FaultException<TimeZoneFault> ex) {
                throw new TimeZoneException(ex.Detail.Message);
            } catch (Exception ex) {
                airport = null;
                Console.WriteLine(@"UpdateAirport Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return airport;
        }

        /// <exception cref="DatabaseException" />
        /// <exception cref="NullException" />
        /// <exception cref="Exception" />
        public void DeleteAirport(Airport airport) {
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    client.DeleteAirport(airport);
                }
            } catch (FaultException<NullPointerFault> ex) {
                throw new NullException(ex.Detail.Message);
            } catch (FaultException<DatabaseDeleteFault> ex) {
                throw new DatabaseException(ex.Detail.Message);
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

        public List<Airport> GetAirportsByShortName(string shortName, bool equalsTo) {
            List<Airport> list;
            try {
                using (AirportServiceClient client = new AirportServiceClient()) {
                    list = client.GetAirportsByShortName(shortName, equalsTo);
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