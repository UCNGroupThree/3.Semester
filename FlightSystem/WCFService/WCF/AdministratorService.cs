using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using Common;
using Common.Exceptions;
using WCFService.Helper;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    public class AdministratorService : IAdministratorService {

        private readonly FlightDB db = new FlightDB();

        #region Create / Update / Delete

        //[PrincipalPermission(SecurityAction.Demand, Role = "None")]
        public Administrator AddAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }            
            if (db.Administrators.Any(r => r.Username.Equals(administrator.Username, StringComparison.OrdinalIgnoreCase))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            try {
                administrator.PasswordHash = PasswordHelper.CreateHash(administrator.PasswordPlain);
                administrator.PasswordPlain = null;
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                if (ex is NullReferenceException || ex is PasswordFormatException) {
                    throw new FaultException<PasswordFormatFault>(new PasswordFormatFault());
                }
            }
            try {
                db.Administrators.Add(administrator);
                //Trace.WriteLine("Add Administrator Service! ID DON'T EMPTY: " + administrator.ID + "<--");
                //Trace.WriteLine("Add Administrator Service! Concurrency: DON'T EMPTY:" + administrator.Concurrency + "<--");
                db.SaveChanges();

            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault("administrator"));
            }

            return administrator;
        }
        
        public Administrator UpdateAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            if (db.Administrators.Any(a => a.ID != administrator.ID && a.Username.Equals(administrator.Username, StringComparison.OrdinalIgnoreCase))) {
                throw new FaultException<AlreadyExistFault>(new AlreadyExistFault());
            }
            /*Administrator orginal = GetAdministrator(administrator.ID);
            if (orginal == null) {
                throw new FaultException<NotFoundFault>(new NotFoundFault());
            }*/
            
            //Trace.WriteLine("admin: {0}, {1}, {2}", administrator.ID, administrator.Username, administrator.PasswordHash);
            //Trace.WriteLine("orginal: {0}, {1}, {2}", orginal.ID, orginal.Username, orginal.PasswordHash);
            try {
                bool changedPassword = false;
                if (administrator.PasswordPlain != null) {
                    try {
                        changedPassword = true;
                        administrator.PasswordHash = PasswordHelper.CreateHash(administrator.PasswordPlain);
                        administrator.PasswordPlain = null; //Try generate new hash
                    } catch (NullReferenceException ex) {
#if DEBUG
                        ex.DebugGetLine();
#endif
                        changedPassword = false;
                        if (administrator.PasswordHash == null) {
                            administrator.PasswordHash = "TempPa55w0rd"; //Set for Attaching to DBSet, for validation works.
                        }
                        //administrator.PasswordHash = orginal.PasswordHash; //Set same passwordHash as in database.
                    } catch (PasswordFormatException ex) {
#if DEBUG
                        ex.DebugGetLine();
#endif
                        throw new FaultException<PasswordFormatFault>(new PasswordFormatFault());
                    }
                }

                db.Administrators.Attach(administrator);
                db.Entry(administrator).State = EntityState.Modified;
                //db.Entry(administrator).Property(a => a.Username).IsModified = true;
                //Trace.WriteLine("ChangedPass: " + changedPassword);
                db.Entry(administrator).Property(a => a.PasswordHash).IsModified = changedPassword;
                db.SaveChanges();
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("administrator"));
            }
            return administrator;
        }
        
        /*
        public Administrator UpdatePassword(Administrator administrator) {
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
                Trace.WriteLine(ex.Message);
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault("administrator"));
            }
            return administrator;
        }
*/        
        public void DeleteAdministrator(Administrator administrator) {
            if (administrator == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());
            }
            try {
                db.Administrators.Attach(administrator);
                db.Entry(administrator).State = EntityState.Deleted;
                db.SaveChanges();
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault("administrator"));
            }
        }

        #endregion
        #region Read

        public Administrator GetAdministrator(int id) {
            Administrator ret;
            try {
                ret = db.Administrators.SingleOrDefault(a => a.ID == id);
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                ret = null;
            }
            return ret;
        }

        public List<Administrator> GetAdministratorsByUsername(string username, bool equalsTo) {
            List<Administrator> ret;
            try {
                if (equalsTo) {
                    ret =
                        db.Administrators.Where(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                } else {
                    ret = db.Administrators.Where(a => a.Username.Contains(username)).ToList();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                ret = new List<Administrator>();
            }
            return ret;
        }

        public List<Administrator> GetAllAdministrators() {
            List<Administrator> ret;
            try {
                ret = db.Administrators.ToList();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                ret = null;
            }
            return ret;
        }


        #endregion

        #region Login

        public bool ValidateLogin(string username, string password) {
            bool ret = false;
            try {
                var hash =
                    // ReSharper disable once PossibleNullReferenceException
                    db.Administrators.SingleOrDefault(
                        a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).PasswordHash;
                ret = PasswordHelper.ValidatePassword(password, hash);
            } catch (Exception) {
                //Empty
            }

            return ret;
        }

        #endregion
    }
}