using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.Helper;
using FlightWeb.MainService;

namespace FlightWeb {
    public partial class Reservation : System.Web.UI.Page {
        private ResSession ses;

        protected void Page_Load(object sender, EventArgs e) {
            ses = ResSession.Current(Session);
            //Demo();
            if (!IsAllowed()) {
                Session["Dialog"] = new DialogHelper("Error", "You need to find a flight first, or maybe your session has timeout! :(");
                Response.Redirect("Default.aspx", true);
            }

            if (!IsPostBack) {
                MakeSeatReservations();
                gvFlights.DataSource = ses.Flights;
                gvFlights.DataBind();
            }
        }

        private void MakeSeatReservations() {
            try {
                ses.Ticket = ses.ResClient.MakeSeatsOccupiedRandom();
            } catch (FaultException<NotEnouthFault> ex) {
                Session["Dialog"] = new DialogHelper("Error", "There are not enouth free seats to make the booking. :(");
                Response.Redirect("Default.aspx", true);
            } catch (FaultException<DatabaseFault> ex) {
                Session["Dialog"] = new DialogHelper("Error", "An Database error has happen. Try again.");
                Response.Redirect("Default.aspx", true);
            } catch (Exception ex) {
                Session["Dialog"] = new DialogHelper("Error", "An error have happen, maybe because of a timeout. Try again");
                Response.Redirect("Default.aspx", true);
            }
        }

        private bool IsAllowed() {
            bool allowed;
            try {
                allowed = ses.ResClient != null && ses.Flights != null && ses.Flights.Count > 0;
            } catch (Exception) {
                allowed = false;
            }
            return allowed;
        }

        //TODO Remove this before deploy!
        private void Demo() {
            var today = DateTime.Now;
            ses.Flights = ses.GetNewResClient()
                .GetFlightsAsd(1, 3, 2, new DateTime(today.Year, today.Month, today.Day, 0, 1, 0), user: new User());
            ses.Ticket = ses.ResClient.MakeSeatsOccupiedRandom();
            
            //ResSession.ResServiceClient.Complete();

        }
        
        protected void gvFlights_OnRowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int flightID = ((Flight) e.Row.DataItem).ID;
                GridView gvSeatReservations = e.Row.FindControl("gvSeatReservations") as GridView;
                //string flightID = gvFlights.DataKeys[e.Row.RowIndex].Value.ToString();
                Debug.WriteLine("Row: FlightID: " + flightID + ", #### " + gvFlights.DataKeys);

                Debug.Assert(gvSeatReservations != null, "gvSeatReservations != null");
                gvSeatReservations.DataSource = ses.Ticket.SeatReservations.Where(s => s.Flight_ID == flightID);
                gvSeatReservations.DataBind();

            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect("Default.aspx", true);
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e) {
            string header;
            string text;
            try {
                ses.ResClient.Complete();
                ses.CloseResClient();
                ses.Ticket = null;
                ses.Flights = null;
                header = "Ticket is Saved";
                text = "Your Ticket has been saved! Have a nice travel :)";
            } catch (Exception) {
                header = "Error";
                text = "There happen an error, maybe because you are too slow.. Try again";
            }
            Session["Dialog"] = new DialogHelper(header, text);

            Response.Redirect("Default.aspx", true);
        }
    }
}