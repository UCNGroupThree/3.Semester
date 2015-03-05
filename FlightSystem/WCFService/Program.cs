using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
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
    }
}
