using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AirportService : IAirportService {

        private readonly FlightDB db = new FlightDB();

        #region Add / Update / Delete

        public int AddAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Airports.Any(r => r.ShortName.Equals(airport.ShortName))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            ValidateTimeZone(airport);
            try {
                db.Airports.Add(airport);
                db.SaveChanges();
            } catch (Exception ex) {
                /*
                if (ex is System.Data.Entity.Validation.DbEntityValidationException) {
                    foreach (var v in ((System.Data.Entity.Validation.DbEntityValidationException) ex).EntityValidationErrors) {
                        foreach (var va in v.ValidationErrors) {
                            Debug.WriteLine(va.ErrorMessage);
                        }
                    }
                    Debug.WriteLine("#####");
                    ;
                }
                Debug.WriteLine(ex);*/
                //TODO Håndtering af forskellige insert exception
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault("airport"));
            }

            return airport.ID;
        }

        public Airport UpdateAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Airports.Any(a => a.ID != airport.ID && a.ShortName.Equals(airport.ShortName))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            ValidateTimeZone(airport);
            try {
                db.Airports.Attach(airport);
                db.Entry(airport).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("airport"));
            }
            return airport;
        }

        public void DeleteAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            try {
                db.Airports.Remove(airport);
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault("airport"));
            }
        }

        /// <summary>
        /// Validate TimeZone on airport
        /// </summary>
        /// <exception cref="OutOfMemoryException" />
        /// <exception cref="ArgumentException" />
        /// <exception cref="TimeZoneNotFoundException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="InvalidTimeZoneException" />
        private void ValidateTimeZone(Airport airport) {
            try {
                var i = airport.TimeZone;
            } catch (Exception) {
                throw new FaultException<TimeZoneFault>(new TimeZoneFault());
            }
        }

        #endregion

        #region Get methods

        public Airport GetAirport(int id) {
            Airport ret;
            try {
                ret = db.Airports.Where(a => a.ID == id).Include(a => a.Routes).SingleOrDefault();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                ret = null;
            }
            return ret;
        }

        public List<Airport> GetAirportsByCountry(string country) {
            List<Airport> ret;
            try {
                ret = db.Airports.Where(a => a.Country.Contains(country)).ToList();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                ret = new List<Airport>();
            }
            return ret;
        }

        public List<Airport> GetAirportsByCity(string city) {
            List<Airport> ret;
            try {
                ret = db.Airports.Where(a => a.City.Contains(city)).ToList();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                ret = new List<Airport>();
            }
            return ret;
        }

        public List<Airport> GetAirportsByName(string name) {
            List<Airport> ret;
            try {
                ret = db.Airports.Where(a => a.Name.Contains(name)).ToList();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                ret = new List<Airport>();
            }
            return ret;
        }

        public List<Airport> GetAirportsByShortName(string shortName, bool equalsTo) {
            List<Airport> ret;
            try {
                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s); //TODO DEBUG EF
                if (equalsTo) {
                    ret =
                        db.Airports.Where(a => a.ShortName.Equals(shortName, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                } else {
                    ret = db.Airports.Where(a => a.ShortName.Contains(shortName)).ToList();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                ret = new List<Airport>();
            }
            return ret;
        }


        public List<string> GetCountries() {
            List<string> con;

            try {
                con = db.Airports.Select(c => c.Country).Distinct().ToList();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                con = new List<string>();
            }

            return con;
        }

        #endregion
    }
}