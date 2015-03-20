using System.Data.Entity;
using System.Data.Entity.Core;
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
            _db.SaveChanges();

            return user.ID;
        }

        public User UpdateUser(User user) {
            try {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            } catch (OptimisticConcurrencyException) {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            } catch (UpdateException) {
                throw new FaultException("The database was unable to update the record");
            }

            return user;
        }

        public void DeleteUser(User user) {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        public User GetUser(int id) {
            return _db.Users.FirstOrDefault(usr => usr.ID == id);
        }
    }
}