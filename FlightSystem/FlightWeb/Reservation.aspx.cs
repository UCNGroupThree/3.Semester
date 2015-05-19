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
                Session["Error"] = "You need to find a flight first, or maybe your session has timeout! :(";
                Response.Redirect("Search.aspx", true);
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
                Session["Error"] = "There are not enouth free seats to make the booking. :(";
                Response.Redirect("Search.aspx", true);
            } catch (FaultException<DatabaseFault> ex) {
                Session["Error"] = "An Database error has happen. Try again.";
                Response.Redirect("Search.aspx", true);
            } catch (Exception ex) {
                Session["Error"] = "An error have happen, maybe because of a timeout. Try again";
                Response.Redirect("Search.aspx", true);
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
                .GetFlightsAsd(1, 3, 2, new DateTime(today.Year, today.Month, today.Day, 0, 1, 0));
            ses.Ticket = ses.ResClient.MakeSeatsOccupiedRandom();
            
            //ResSession.ResServiceClient.Complete();

        }

        protected void Button1_OnClick(object sender, EventArgs e) {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        /*
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                gvOrders.DataSource = GetData(string.Format("select top 3 * from Orders where CustomerId='{0}'", customerId));
                gvOrders.DataBind();
            }
        }*/

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
    }
}