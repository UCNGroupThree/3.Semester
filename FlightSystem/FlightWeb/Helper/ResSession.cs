using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using FlightWeb.MainService;

namespace FlightWeb.Helper {
    public class ResSession {

        public List<Flight> Flights { get; set; }
        public ReservationServiceClient ResClient { get; set; }
        public Ticket Ticket { get; set; }

        private ResSession() {

        }

        // Gets the current session.
        public static ResSession Current(HttpSessionState ses) {
            ResSession session = (ResSession)ses["ResSession"];
            if (session == null) {
                session = new ResSession();
                ses["ResSession"] = session;
            }
            return session;
        }

        public ReservationServiceClient GetNewResClient() {
            CloseResClient();
            return ResClient = new ReservationServiceClient();
        }

        public void CloseResClient() {
            //var resClient = ResServiceClient;
            if (ResClient != null) {
                Debug.WriteLine("ResSession.CloseResClient(): CLOSED CLIENT!");
                try {
                    ResClient.Cancel();
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