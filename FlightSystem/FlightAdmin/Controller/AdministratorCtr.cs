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

        #region Create / Update / Delete

        public Administrator CreateAdministrator(string username, string password) {

            Administrator administrator;

            try {
                administrator = new Administrator {
                    Username = username,
                    PasswordPlain = password
                };

                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    administrator.ID = client.AddAdministrator(administrator);
                    administrator.PasswordPlain = null;
                }
            } catch (FaultException<NullPointerFault>) {
                throw new NullException();
            } catch (FaultException<AlreadyExistFault>) {
                throw new AlreadyExistException();
            } catch (FaultException<PasswordFormatFault>) {
                throw new PasswordFormatException();
            } catch (FaultException<DatabaseInsertFault> dbException) {
                throw new DatabaseException(dbException.Detail.Message);

            } catch (Exception ex) {
                administrator = null;
                Console.WriteLine(@"CreateAdministrator Exception: " + ex);
                //TODO Exception Handler
                throw;
            }

            return administrator;
        }

        public Administrator UpdateAdministrator(Administrator administrator, string username) {

            Administrator updatedAdministrator = null;

            try {

                updatedAdministrator = administrator;
                updatedAdministrator.Username = username;

                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    client.UpdateAdministrator(updatedAdministrator);
                }
            } catch (Exception e) {

                Console.WriteLine(e.Message);
            }
            // TODO: more exceptions

            return administrator;
        }

        public void DeleteAdministrator(Administrator administrator) {
            try {
                using (AdministratorServiceClient client = new AdministratorServiceClient()) {
                    client.DeleteAdministrator(administrator);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region read

        public Administrator GetAdministrator(int id) {

            Administrator administrator = null;

            try {
                using (var client = new AdministratorServiceClient()) {

                    administrator = client.GetAdministrator(id);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return administrator;
        }

        #endregion
    }
}
