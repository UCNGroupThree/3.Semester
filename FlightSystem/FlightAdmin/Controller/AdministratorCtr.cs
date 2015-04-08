using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller
{
    class AdministratorCtr
    {

        #region Create

        public Administrator CreateAdministrator(string username) {

            Administrator administrator;

            try 
            {

                administrator = new Administrator();

                administrator.Username = username;

                using (AdministratorServiceClient client = new AdministratorServiceClient()) 
                {
                   client.AddAdministrator(administrator);
                }
                
            } catch(Exception e) {
                administrator = null;
                Console.WriteLine(e.Message);
            } // TODO: more exceptions

            return administrator;
        }
        #endregion

        #region update

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

        #endregion

        #region delete

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
