using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.Helper;
using FlightWeb.MainService;

namespace FlightWeb {
    public partial class Reservation : System.Web.UI.Page {
        private MySession ses = MySession.Current;

        protected void Page_Load(object sender, EventArgs e) {

            Debug.WriteLine("#### Reservation ####");
            Debug.WriteLine("UrlReferrer: " + Request.UrlReferrer);
            Debug.WriteLine("SessionFlights: " + Session["flights"]);
            Debug.WriteLine("#### End ####");
            if (!IsPostBack) {
                Demo();
                if (Request.UrlReferrer != null) ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                gvFlights.DataSource = MySession.Current.Flights;
                gvFlights.DataBind();
            }
        }

        //TODO Remove this before deploy!
        private void Demo() {
            var today = DateTime.Now;
            ses.Flights = ses.GetNewResClient()
                .GetFlightsAsd(1, 3, 2, new DateTime(today.Year, today.Month, today.Day, 0, 1, 0));
            ses.Ticket = ses.ResClient.MakeSeatsOccupiedRandom();
            
            //MySession.ResServiceClient.Complete();

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