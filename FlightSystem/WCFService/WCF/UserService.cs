using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF
{
    public class UserService : IUserService {

        readonly FlightDB _db = new FlightDB();

        public int AddUser(User user) {
            _db.Users.Add(user);

            if (_db.Users.Any(x => x.Email == user.Email)) {
                return -1;
            }
            if (!_db.Postals.Any(p => p.PostCode == user.Postal.PostCode)) {
                _db.Postals.Add(user.Postal);
            } else {
                _db.Postals.Attach(user.Postal);
            }

            _db.SaveChanges();

            return user.ID;
        }

        public User UpdateUser(User user) {
            try {
                _db.Entry(user).State = EntityState.Modified;
                _db.Entry(user.Postal).State = EntityState.Modified;
                _db.SaveChanges();
            } catch (OptimisticConcurrencyException) {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            } catch (UpdateException) {
                throw new FaultException("The database was unable to update the record");
            }

            return user;
        }

        public void DeleteUser(User user) {
            _db.Users.Attach(user);
            _db.Users.Remove(user);             
            _db.SaveChanges(); 
        }

        public User GetUser(int id) {
            return _db.Users.Where(usr => usr.ID == id).Include(usr => usr.Postal).SingleOrDefault();
        }


        public List<User> GetUserByName(string name) {
            List<User> list = new List<User>();
            try {
                list = _db.Users.Where(x => x.Name.Contains(name)).ToList();
            } catch (Exception ex) {

                Console.WriteLine(ex.Message);

            }
            return list;
        }


        public List<User> GetAllUsers() {
            List<User> list = new List<User>();
          
            foreach (User user in _db.Users.ToList()) {
                int id = user.ID;
                list.Add(GetUser(id));
                                  
            }
            return list;
        }

    }
}