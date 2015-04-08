using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using WCFService.Exceptions;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AdministratorService : IAdministratorService {

        private readonly FlightDB db = new FlightDB();

        public int AddAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }            
            if (db.Administrators.Any(r => r.Username.Equals(administrator.Username, StringComparison.OrdinalIgnoreCase))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            try {
                administrator.GenerateHash();
            } catch (Exception ex) {
                if (ex is NullReferenceException || ex is PasswordFormatException) {
                    throw new FaultException<PasswordFormatFault>(new PasswordFormatFault());
                }
            }
            try {
                db.Administrators.Add(administrator);
                db.SaveChanges();

            } catch (Exception ex) {
                //Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault("administrator"));
            }

            return administrator.ID;
        }

        public void UpdateAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Administrators.Any(a => a.ID != administrator.ID && a.Username.Equals(administrator.Username, StringComparison.OrdinalIgnoreCase))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            Administrator orginal = GetAdministrator(administrator.ID);
            if (orginal == null) {
                throw new FaultException<NotFoundFault>(new NotFoundFault());
            }
            try {
                administrator.PasswordHash = orginal.PasswordHash;

                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("administrator"));
            }
        }

        public void UpdatePassword(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            try {
                administrator.GenerateHash();
            } catch (Exception ex) {
                if (ex is NullReferenceException || ex is PasswordFormatException) {
                    throw new FaultException<PasswordFormatFault>(new PasswordFormatFault());
                }
            }
            Administrator orginal = GetAdministrator(administrator.ID);
            if (orginal == null) {
                throw new FaultException<NotFoundFault>(new NotFoundFault());
            }
            try {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("administrator"));
            }
        }
        
        public void DeleteAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            try {
                db.Administrators.Attach(administrator);
                db.Entry(administrator).State = EntityState.Deleted;
                db.SaveChanges();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault("administrator"));
            }
        }

        public Administrator GetAdministrator(int id) {
            return db.Administrators.FirstOrDefault(admin => admin.ID == id);
        }

        public List<Administrator> GetAllAdministrators() {
            return db.Administrators.ToList();
        }

    }
}