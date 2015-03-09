using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Interface;

namespace WCFService {
    class Program {

        
        static void Main(string[] args) {

            var httpUrl = new Uri("http://localhost:9494/");

            //Create ServiceHost
            var host = new ServiceHost(typeof(UserService), httpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IUserService), new WSHttpBinding(), "");

            //Enable metadata exchange
            var smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("Service is host at " + DateTime.Now);
            Console.WriteLine("Host is running... Press <Enter> key to stop");

            Console.ReadLine();
        }


        protected virtual void Help() {

            using (FlightDB db = new FlightDB()) {
                // Hele Admin Listen
                List<Administrator> admins = db.Administrators.ToList();

                // Finde en admin ud fra ID
                Administrator ad = db.Administrators.FirstOrDefault(adm => adm.ID == 1);


                // Finde en liste af admins ud fra username
                List<Administrator> admins2 = db.Administrators.Where(adm => adm.Username.Equals("Lasse")).ToList();

                //Tilføje en Administrator
                db.Administrators.Add(ad);
                db.SaveChanges();


                // Rette en administrator
                ad.Username = "Lasse2";
                db.Administrators.Attach(ad);
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();



            }
        }
        
    }
}
