using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AdministratorService : IAdministratorService{

        readonly FlightDB _db = new FlightDB();

        public int AddAdministrator(Administrator administrator) {
            _db.Administrators.Add(administrator);
            _db.SaveChanges();

            return administrator.ID;
        }

        public Administrator UpdateAdministrator(Administrator administrator) {
            try {
                _db.Entry(administrator).State = EntityState.Modified;
                _db.SaveChanges();
            } catch (OptimisticConcurrencyException exception) {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            } catch (UpdateException exception) {
                throw new FaultException("The database was unable to update the record");
            }

            return administrator;
        }

        public void DeleteAdministrator(Administrator administrator) {
            _db.Administrators.Remove(administrator);
            _db.SaveChanges();
        }

        public Administrator GetAdministrator(int id) {
            return _db.Administrators.FirstOrDefault(admin => admin.ID == id);
        }
    }
}