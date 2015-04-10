using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Common.Exceptions;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller
{
    class CustomerCtr
    {
        #region Create

        public User CreateUser(string name, string address,Postal postal, string phoneNumber, string email) {
            

            User user = new User {
                Name = name,
                Address = address,
                Postal = postal,
                PhoneNumber = phoneNumber,
                Email = email
            };

            try {
                using (var client = new UserServiceClient()) {
                    client.AddUser(user);
                }

            } catch (FaultException<DatabaseInsertFault> dbException) {
                throw new Exception(dbException.Message);
            } catch (Exception e) {
                throw new Exception("WCF Service Exception", e);
            }

            return user;
        }

        #endregion

        #region read

        public User GetUser(int id) {
            User user = null;

            using (var client = new UserServiceClient()) {

                try {
                    user = client.GetUser(id);
                } catch (FaultException<NullPointerFault> nullException) {

                    throw new Exception(nullException.Message);
                } catch (Exception e) {
                    throw new ConnectionException("WCF Service Exception", e);
                }                  
            }

            return user;
        }

        public List<User> GetUserByName(string name) {

            List<User> userList;
            try {
                using (UserServiceClient client = new UserServiceClient()) {
                    userList = client.GetUserByName(name);
                 
                }
            } catch (Exception ex) {

                throw new ConnectionException("WCF Service Exception", ex);
            }
            return userList;
        } 

        #endregion

        #region Update

        public User UpdateUser(User user, string name, string address, Postal postal, string phoneNumber, string email)
        {

            using (var client = new UserServiceClient()) {
                User userUpdate = null;

                try {
                    user.Name = name;
                    user.Address = address;
                    user.Postal = postal;
                    user.PhoneNumber = phoneNumber;
                    user.Email = email;

                    userUpdate = client.UpdateUser(user);
                    } catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
                        throw new Exception(concurrencyException.Message);
                    } catch (FaultException<DatabaseUpdateFault> updateException) {
                        throw new Exception(updateException.Message);
                    } catch (Exception e) {
                        throw new ConnectionException("WCF Service Exception 1", e);
                    }
                return userUpdate;
                
            }
            
        } 

        #endregion

        #region Delete

        public void DeleteUser(User user) {

            using (var client = new UserServiceClient()) {

                try
                {
                    client.DeleteUser(user);
                    
                } catch (FaultException<NullPointerFault> nullException) {
                    throw new Exception(nullException.Message);
                } catch (FaultException<DatabaseDeleteFault> dbException) {
                    throw new Exception(dbException.Message);
                } catch (Exception ex) {
                    throw new ConnectionException(ex.Message);
                }

            }
        }

        #endregion


    }
}
