using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AdministratorService : IAdministratorService {

        private readonly FlightDB db = new FlightDB();

        public int AddAdministrator(Administrator administrator) {
            db.Administrators.Add(administrator);
            db.SaveChanges();

            return administrator.ID;
        }

        public Administrator UpdateAdministrator(Administrator administrator) {
            try {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
            } catch (OptimisticConcurrencyException) {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            } catch (UpdateException) {
                throw new FaultException("The database was unable to update the record");
            }

            return administrator;
        }

        public void DeleteAdministrator(Administrator administrator) {
            db.Administrators.Remove(administrator);
            db.SaveChanges();
        }

        public Administrator GetAdministrator(int id) {
            return db.Administrators.FirstOrDefault(admin => admin.ID == id);
        }
    }
}