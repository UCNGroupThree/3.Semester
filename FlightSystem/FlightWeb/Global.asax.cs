using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using FlightWeb.Helper;
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
            ResSession.Current(Session).CloseResClient();
        }

        
    }
}