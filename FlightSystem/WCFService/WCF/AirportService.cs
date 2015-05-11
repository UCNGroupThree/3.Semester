using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {

    public class AirportService : IAirportService {

        private readonly FlightDB db = new FlightDB();

        #region Add / Update / Delete

        public Airport AddAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Airports.Any(r => r.ShortName.Equals(airport.ShortName))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            ValidateTimeZone(airport);
            try {
                db.Airports.Add(airport);
                Debug.WriteLine("Add Airport Service! ID DON'T EMPTY: " + airport.ID + "<--");
                Debug.WriteLine("Add Airport Service! Concurrency DON'T EMPTY: " + airport.Concurrency + "<--");
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
                }*/
                Debug.WriteLine(ex);
                //TODO Håndtering af forskellige insert exception
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault("airport"));
            }

            return airport;
        }

        public Airport UpdateAirport(Airport airport) {
            //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            if (airport == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Airports.Any(a => a.ID != airport.ID && !string.IsNullOrEmpty(a.ShortName) && a.ShortName.Equals(airport.ShortName))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            //Debug.WriteLine("######");
            ValidateTimeZone(airport);
            try {
                db.Airports.Attach(airport);
                db.Entry(airport).State = EntityState.Modified;
                db.SaveChanges();


                // Running Async Update on Dijkstra Matrix
                new Task(() => Dijkstra.Updated(airport)).Start();
                //Debug.WriteLine("######");
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
                db.Airports.Attach(airport);
                db.Entry(airport).State = EntityState.Deleted;
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault("airport"));
            }
        }

        /// <summary>
        /// Validate TimeZone on airport
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="FaultException"></exception>
        private void ValidateTimeZone(Airport airport) {
            if (airport == null) {
                throw new ArgumentNullException("airport");
            }
            if (airport.TimeZone == null) {
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