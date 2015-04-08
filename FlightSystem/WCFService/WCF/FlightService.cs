using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Linq;
using WCFService.WCF.Interface;
using WCFService.Model;
using System.ServiceModel;
using WCFService.WCF.Faults;

namespace WCFService.WCF {
    public class FlightService : IFlightService {

        private readonly FlightDB db = new FlightDB();

        public int AddFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());  //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Flights.Add(flight);
                db.SaveChanges();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault(){Message = ex.Message});
            }
            return flight.ID;

        }

        public Flight UpdateFlight(Flight flight) {
            Flight retFlight = flight;

            if (flight == null){
                throw new FaultException<NullPointerFault>(new NullPointerFault()); //TODO vores egen Nullpointer Exception?
            }

            try {
                db.Flights.AddOrUpdate(flight);
                //db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
            } catch (OptimisticConcurrencyException e) {
                throw new FaultException<OptimisticConcurrencyFault>(new OptimisticConcurrencyFault(){Message = e.Message});
            }catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault(){Message = ex.Message});
            }

            return retFlight;
        }

        public void DeleteFlight(Flight flight)
        {
            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault()); //TODO vores egen Nullpointer Exception?
            }
            try {
                db.Flights.Remove(flight);
                db.SaveChanges();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault(){Message = ex.Message});
            }
        }

        public Flight GetFlight(int id) {
            Flight flight = db.Flights.SingleOrDefault(x => x.ID == id);

            if (flight == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault()); //TODO vores egen Nullpointer Exception?
            }

            return flight;
        }
    }
}