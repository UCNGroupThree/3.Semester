using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using FlightWeb.MainService;

namespace FlightWeb {
    public class Global : HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Session_OnStart() {
            Debug.WriteLine("START SESSION");
            Debug.WriteLine("START SESSION");
            Debug.WriteLine("START SESSION");
            Debug.WriteLine("START SESSION");
            
        }
        public void Session_OnEnd() {
            Debug.WriteLine("END SESSION");
            Debug.WriteLine("END SESSION");
            Debug.WriteLine("END SESSION");
            Debug.WriteLine("END SESSION");
            var resClient = Session["resClient"] as ReservationServiceClient;
            if (resClient != null) {
                Debug.WriteLine("CLOSED CLIENT!");
                try {
                    resClient.Close();
                } catch (Exception ex) {
                    resClient.Abort();
                    Debug.WriteLine("Handled Exception:");
                    Debug.WriteLine(ex);
                    //Debug.WriteLine(ex.Message);
                    Debug.WriteLine("#####");
                    //Timeout
                }
            }
        }
    }
}