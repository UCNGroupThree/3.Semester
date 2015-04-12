using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    class AdministratorCtr {

        #region Create / Delete

        public Administrator CreateAdministrator(string username, string password) {

            Administrator administrator;

            try {
                administrator = new Administrator {
                    Username = username,
                    PasswordPlain = password
                };
                Console.WriteLine(administrator.PasswordPlain);
                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    administrator = client.AddAdministrator(administrator);
                }
            } catch (FaultException<AlreadyExistFault>) {
                throw new AlreadyExistException();
            } catch (FaultException<PasswordFormatFault>) {
                throw new PasswordFormatException();
            } catch (FaultException<DatabaseInsertFault> dbException) {
                throw new DatabaseException(dbException.Detail.Message);
            } catch (Exception ex) {
                Console.WriteLine(@"CreateAdministrator Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return administrator;
        }
        
        public void DeleteAdministrator(Administrator administrator) {
            try {
                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    client.DeleteAdministrator(administrator);
                }
            } catch (FaultException<NullPointerFault> ex) {
                throw new NullException(ex.Detail.Message);
            } catch (FaultException<DatabaseDeleteFault> ex) {
                throw new DatabaseException(ex.Detail.Message);
            } catch (Exception ex) {
                Console.WriteLine(@"DeleteAdministrator Exception: " + ex);
                //TODO Exception Handler
                throw;
            }
        }

        #endregion

        #region Update

        public Administrator UpdateAdministrator(Administrator administrator, string username) {

            Administrator temp = null;

            try {
                temp = administrator.GetCopy();

                administrator.Username = username;

                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    var updated = client.UpdateAdministrator(administrator);
                    administrator.SetToCopy(updated);
                }
            } catch (FaultException<AlreadyExistFault>) {
                administrator.SetToCopy(temp);
                throw new AlreadyExistException();
            } catch (FaultException<NotFoundFault>) {
                administrator.SetToCopy(temp);
                throw new NotFoundException();
            } catch (FaultException<DatabaseUpdateFault> ex) {
                administrator.SetToCopy(temp);
                throw new DatabaseException(ex.Detail.Message);
            
            } catch (Exception ex) {
                administrator.SetToCopy(temp);
                Console.WriteLine(@"UpdateAdministrator Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return administrator;
        }

        public Administrator UpdatePassword(Administrator administrator, string password) {
            Administrator temp = null;

            try {
                temp = administrator.GetCopy();

                administrator.PasswordPlain = password;

                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    var updated = client.UpdatePassword(administrator);
                    administrator.SetToCopy(updated);
                }
            } catch (FaultException<NotFoundFault>) {
                administrator.SetToCopy(temp);
                throw new NotFoundException();
            } catch (FaultException<PasswordFormatFault>) {
                administrator.SetToCopy(temp);
                throw new PasswordFormatException();
            } catch (FaultException<DatabaseUpdateFault> ex) {
                administrator.SetToCopy(temp);
                throw new DatabaseException(ex.Detail.Message);
            } catch (Exception ex) {
                administrator.SetToCopy(temp);
                Console.WriteLine(@"UpdateAdministrator Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return administrator;
        }   

        #endregion

        #region read

        public Administrator GetAdministrator(int id) {
            Administrator administrator;

            try {
                using (var client = new AdministratorServiceClient()) {

                    administrator = client.GetAdministrator(id);
                }
            } catch (Exception e) {
                Console.WriteLine(@"GetAdministrator Exception: " + e);
                throw new ConnectionException("WCF Service Exception", e);
            }

            return administrator;
        }

        public List<Administrator> GetAdministratorsByUsername(string username, bool equalsTo) {
            List<Administrator> list;

            try {
                using (var client = new AdministratorServiceClient()) {
                    list = client.GetAdministratorsByUsername(username, equalsTo);
                }
            } catch (Exception e) {
                Console.WriteLine(@"GetAdministratorsByUsername Exception: " + e);
                throw new ConnectionException("WCF Service Exception", e);
            }

            return list;
        }

        public List<Administrator> GetAllAdministrators() {
            List<Administrator> list;

            try {
                using (var client = new AdministratorServiceClient()) {
                    list = client.GetAllAdministrators();
                }
            } catch (Exception e) {
                Console.WriteLine(@"GetAllAdministrators Exception: " + e);
                throw new ConnectionException("WCF Service Exception", e);
            }

            return list;
        }

        #endregion

    }

    
}
