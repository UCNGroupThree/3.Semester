using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.WCF.Interface;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF
{
    class PostalService : IPostalService
    {
        FlightDB dbContext = new FlightDB();

        public int AddPostal(Postal postal)
        {
            if (postal == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());  
            }
            try {
                dbContext.Postals.Add(postal);
                dbContext.SaveChanges();
            } catch (Exception e) {

                Console.WriteLine(e.Message); 
                throw new FaultException<DatabaseInsertFault>(new DatabaseInsertFault() { Message = e.Message });
            }
            return postal.PostCode;
        }

        public Postal UpdatePostal(Postal postal)
        {
            if (postal == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());  
            }
            try {
                dbContext.Postals.Attach(postal);
                dbContext.Entry(postal).State = EntityState.Modified;
                dbContext.SaveChanges();
            } catch (Exception e) {

                Console.WriteLine(e.Message);
                throw new FaultException<DatabaseUpdateFault>(new DatabaseUpdateFault() { Message = e.Message });
            }
            return postal;
        }

        public int DeletePostal(Postal postal)
        {
            if (postal == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault());  
            }
            try {
                dbContext.Postals.Remove(postal);
                dbContext.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine(e.Message); //TODO DEBUG MODE?
                throw new FaultException<DatabaseDeleteFault>(new DatabaseDeleteFault() { Message = e.Message });
            }
            return postal.PostCode;
        }

        public Postal GetPostal(int postalNumber) {

            Postal postal = dbContext.Postals.SingleOrDefault(x => x.PostCode == postalNumber);
           
            if (postal == null) {
                throw new FaultException<NullPointerFault>(new NullPointerFault()); 
            }
           return postal;
        }
    }
}
