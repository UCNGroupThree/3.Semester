using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AirportService : IAirportService {

        private readonly FlightDB db = new FlightDB();

        public int AddAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            if (db.Airports.Any(r => r.ShortName.Equals(airport.ShortName))) {
                throw new FaultException("ShortName allready used in database"); //TODO Insert Exception?
            }
            try {
                db.Airports.Add(airport);
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to insert the record");
            }

            return airport.ID;
        }

        public Airport UpdateAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            if (db.Airports.Any(r => !r.Equals(airport) && r.ShortName.Equals(airport.ShortName))) {
                throw new FaultException("The new shortName allready used in database"); //TODO Insert/Update Exception?
            }
            try {
                db.Airports.Attach(airport);
                db.Entry(airport).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to update the record");
            }
            return airport;
        }

        public void DeleteAirport(Airport airport) {
            if (airport == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Airports.Remove(airport);
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to update the record");
            }
        }

        public Airport GetAirport(int id) {
            return db.Airports.SingleOrDefault(a => a.ID == id);
        }

        public List<Airport> GetAirportsByCountry(string country) {
            return db.Airports.Where(a => a.Country.Equals(country, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Airport> GetAirportsByCity(string city) {
            return db.Airports.Where(a => a.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Airport> GetAirportsByName(string name) {
            return db.Airports.Where(a => a.City.Contains(name)).ToList();
        }
    }
}