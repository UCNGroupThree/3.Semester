using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using FlightWeb.MainService;

namespace FlightWeb.Helper {
    public class MySession {

        public List<Flight> Flights { get; set; }
        public ReservationServiceClient ResClient { get; set; }
        public Ticket Ticket { get; set; }

        private MySession() {
        }

        // Gets the current session.
        public static MySession Current {
            get {
                MySession session =
                  (MySession)HttpContext.Current.Session["__MySession__"];
                if (session == null) {
                    session = new MySession();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public ReservationServiceClient GetNewResClient() {
            CloseResClient();
            return ResClient = new ReservationServiceClient();
        }

        public void CloseResClient() {
            //var resClient = ResServiceClient;
            if (ResClient != null) {
                Debug.WriteLine("MySession.CloseResClient(): CLOSED CLIENT!");
                try {
                    ResClient.Close();
                } catch (Exception ex) {
                    ResClient.Abort();
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