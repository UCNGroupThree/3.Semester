using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.Exceptions;
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
                if (PreviousPage == null) {
                    Session["Dialog"] = new DialogHelper("Error", "You first need to find a flight on this page :(");
                    Response.Redirect("Default.aspx", true);
                }
                MakeSeatReservations();
                gvFlights.DataSource = ses.Ticket.SeatReservations.Select(x => x.Flight).ToHashSet();
                gvFlights.DataBind();

            }
        }

        private void MakeSeatReservations() {
            try {
                User user;
                try {
                    using (var userClient = new UserServiceClient()) {
                        var email = HttpContext.Current.User.Identity.Name;
                        if (string.IsNullOrEmpty(email)) {
                            throw new NullException("Please login before booking :)");
                        }
                        user = userClient.GetUsersByEmail(email, true).First();
                        if (user == null) {
                            throw new NullException("Your user details cound be found, try to sign in again");
                        }
                    }
                } catch (Exception ex) {
                    if (ex is NullException) {
                        throw;
                    }
                    throw new NullException("An Error happen in getting your user details", ex);
                }
                var resClient = ses.GetNewResClient();
                ses.Ticket = resClient.MakeSeatsOccupiedRandom(ses.Flights, ses.NoOfSeats, user);
                var ticket = ses.Ticket;
                lblName.Text = ticket.User.Name;
                lblAddress.Text = ticket.User.Address;
                lblPostalCode.Text = ticket.User.Postal.PostCode.ToString();
                lblCity.Text = ticket.User.Postal.City;
                lblTotalPrice.Text = ticket.TotalPrice.ToString("C");
                lblTotalTravelTime.Text = ticket.TotalTravelTime.ToFineString();

            } catch (NullException ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                Session["Dialog"] = new DialogHelper("Error", ex.Message);
                Response.Redirect("Default.aspx", true);
            } catch (FaultException<NotEnouthFault> ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                Session["Dialog"] = new DialogHelper("Error", "There are not enouth free seats to make the booking. :(");
                Response.Redirect("Default.aspx", true);
            } catch (FaultException<DatabaseFault> ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                Session["Dialog"] = new DialogHelper("Error", "An Database error has happen. Try again.");
                Response.Redirect("Default.aspx", true);
            } catch (Exception ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                Session["Dialog"] = new DialogHelper("Error", "An error have happen, maybe because of a timeout. Try again");
                Response.Redirect("Default.aspx", true);
            }
        }

        private bool IsAllowed() {
            bool allowed;
            try {
                allowed = ses.Flights.Count > 0 && ses.NoOfSeats > 0;
            } catch (Exception) {
                //If Flight or ses is null
                allowed = false;
            }
            return allowed;
        }

        //TODO Remove this before deploy!
        private void Demo() {
            if (!Page.IsPostBack) {
                var today = DateTime.Now;
                today = new DateTime(today.Year, today.Month, today.Day, 0, 1, 0);
                User user = null;
                using (var userClient = new UserServiceClient()) {
                    user = userClient.GetUser(22);
                    if (user == null || string.IsNullOrEmpty(user.Name)) {
                        throw new Exception("DEMO: USER WITH ID 22 NOT FOUND!");
                    }
                }
                using (var client = new DijkstraClient()) {
                    ses.NoOfSeats = 1;
                    ses.Flights = client.GetShortestPath(1, 3, ses.NoOfSeats, today);
                }
            }
        }

        protected void gvFlights_OnRowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int flightID = ((Flight)e.Row.DataItem).ID;
                GridView gvSeatReservations = e.Row.FindControl("gvSeatReservations") as GridView;
                //string flightID = gvFlights.DataKeys[e.Row.RowIndex].Value.ToString();
                Debug.WriteLine("Row: FlightID: " + flightID + ", #### " + gvFlights.DataKeys);

                Debug.Assert(gvSeatReservations != null, "gvSeatReservations != null");
                gvSeatReservations.DataSource = ses.Ticket.SeatReservations.Where(s => s.Flight_ID == flightID);
                gvSeatReservations.DataBind();
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e) {
            var pp = PreviousPage;
            var vs = ViewState["SearchPage"];
            Debug.WriteLine("PreviousPage: " + PreviousPage);

            Debug.WriteLine("PreviousPage: " + IsCrossPagePostBack);
            try {
                ses.CloseResClient();
                ses.Ticket = null;
                ses.NoOfSeats = 0;
                ses.Flights = null;
            } catch (Exception) {

            }
            Response.Redirect("Default.aspx", true);
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e) {
            string header;
            string text;
            try {
                ses.ResClient.Complete();
                ses.CloseResClient();
                ses.Ticket = null;
                ses.NoOfSeats = 0;
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