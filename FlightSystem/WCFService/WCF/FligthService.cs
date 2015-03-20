using System;
using System.Data.Entity;
using System.Linq;
using WCFService.WCF.Interface;
using WCFService.Model;
using System.ServiceModel;

namespace WCFService.WCF {
    public class FligthService : IFlightService {
        private readonly FlightDB db = new FlightDB();

        public int AddFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Flights.Add(flight);
                db.SaveChanges();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to insert the record");
            }
            return flight.ID;

        }

        public void UpdateFlight(Flight flight)
        {
            if (flight == null)
            {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Flights.Attach(flight);
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to update the record");
            }
        }

        public void DeleteFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Flights.Remove(flight);
                db.SaveChanges();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to update the record");
            }
        }

        public Flight GetFlight(int id) {
            return db.Flights.SingleOrDefault(x => x.ID == id);
        }
    }
}