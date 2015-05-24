using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Caching;
using System.Web.Services;
using System.Web.UI.WebControls;
using FlightWeb.MainService;

namespace FlightWeb {
    public partial class WizardTest : System.Web.UI.Page {

        public List<Flight> flights {
            get {
                return (List<Flight>)Session["flights"];
            }
            set { Session["flights"] = value; }
        }

        public ReservationServiceClient ResServiceClient {
            get {
                return (ReservationServiceClient) Session["resClient"];
                /*
                var temp = Session["resClient"] as ReservationServiceClient;
                if (temp != null) {
                    return temp;
                }
                var ret = new ReservationServiceClient();
                Session["resClient"] = ret;
                return ret;
                 * */
            }
            set { Session["resClient"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                FirstLoad();

                Demo();
            }

        }

        //TODO Remove this before deploy!
        private void Demo() {
            ddlCountryFrom.SelectedValue = ddlCountryFrom.Items.FindByText("Denmark").Value;
            ddlCountryFrom_SelectedIndexChanged(null, null);
            ddlFrom.SelectedValue = ddlFrom.Items.FindByValue("1").Value;
            ddlCountryTo_SelectedIndexChanged(null, null);
            ddlTo.SelectedValue = ddlFrom.Items.FindByValue("3").Value;

        }

        private void FirstLoad() {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var list = client.GetCountries();
                var countries = list.Select(ListItem.FromString).ToList();
                countries.Insert(0, new ListItem("--- Please select ---", "-1"));

                ddlCountryFrom.Items.AddRange(countries.ToArray());
                ddlCountryTo.Items.AddRange(countries.ToArray());
            }

            for (int i = 1; i <= 10; i++) {
                ddlPersons.Items.Add(new ListItem(i.ToString()));
            }
            var today = DateTime.Now;
            txtDepart.Text = new DateTime(today.Year, today.Month, today.Day, 0,1,0).ToString("g");
        }


        protected static List<ListItem> GetAirportsFromCountry(string country) {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var airports = client.GetAirportsByCountry(country);
                var list = airports.ConvertAll(a => new ListItem(a.ToString(), a.ID.ToString()));
                return list;
            }
        }

        protected void ddlCountryFrom_SelectedIndexChanged(object sender, EventArgs e) {
            AddCountries(ddlCountryFrom, ddlFrom);

            UpdatePanelFrom.Update();
        }

        protected void ddlCountryTo_SelectedIndexChanged(object sender, EventArgs e) {
            AddCountries(ddlCountryTo, ddlTo);

            UpdatePanelTo.Update();
        }

        private void AddCountries(DropDownList country, DropDownList airport) {
            if (country.SelectedIndex != 0) {
                var sel = airport.Items[0];
                airport.Items.Clear();
                airport.Items.Insert(0, sel);
                airport.Items.AddRange(GetAirportsFromCountry(country.SelectedValue).ToArray());
                airport.Enabled = true;
            }
            else {
                airport.SelectedIndex = 0;
                airport.Enabled = false;
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e) {
            switch (e.CurrentStepIndex) {
                case 0:
                    ChangeFromFlightSearch(e);
                    break;
                case 1:
                    ChangeFromFlightSelect(e);
                    break;
            }
        }
        
        private void ChangeFromFlightSearch(WizardNavigationEventArgs e) {
            Page.Validate("FindFligtValidator");
            if (!Page.IsValid) {
                e.Cancel = true;
                return;
            }
            int fromId = int.Parse(ddlFrom.SelectedValue);
            int toId = int.Parse(ddlTo.SelectedValue);
            int seats = int.Parse(ddlPersons.SelectedValue);
            DateTime date = DateTime.Parse(txtDepart.Text);
            
            
            //Debug.WriteLine("from: {0} - to: {1}", fromId, toId);
            //Debug.WriteLine("date: {0}", date);

            try {
                //TODO skal erstattes med et andet endpoint

                ClearSession();
                using (var client = new ReservationServiceClient()) {
                    //var list = client.GetFlightsAsd(fromId, toId, seats, date, user: new User());
                    List<Flight> list = null;
                    if (list != null && list.Count > 0) {
                        flights = list;
                        var first = flights[0];
                        var stops = flights.Count - 1;
                        var last = flights[stops];

                        var price = flights.Sum(x => x.Route.Price);
                        var travelTime = last.DepartureTime - first.ArrivalTime;

                        lblStep2From.Text = first.Route.From.ToString();
                        lblStep2To.Text = last.Route.To.ToString();
                        if (stops > 0) {
                            lblStep2Stops.Text = stops.ToString();
                        } else {
                            lblStep2Stops.Text = "directly";
                        }
                        lblStep2Price.Text = string.Format("{0:C}", price);
                        lblStep2Time.Text = string.Format("{0:g}", travelTime);
                    } else {
                        ShowWarning("No Flight", "We are sorry, but we coundn't find any available flights :(");
                        e.Cancel = true;
                    }
                }
            } catch (Exception ex) {
                //TODO bedre håndtering af fejl
                ShowWarning("Validation Error!", "Body");
                
                e.Cancel = true;

                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
            }
        }

        private void ClearSession() {
            flights = null;
            if (ResServiceClient != null) {
                try {
                    ResServiceClient.Close();
                } catch (Exception) {
                    ResServiceClient.Abort();
                    ResServiceClient = null;
                }
            }
        }

        private void ShowWarning(string title, string body) {
            lblModalTitle.Text = title;
            lblModalBody.Text = body;
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divModal", "$('#divModal').modal();", true);
            upModal.Update();
        }

        private void ChangeFromFlightSelect(WizardNavigationEventArgs wizardNavigationEventArgs) {
            //Session["wcfClient"] = new ReservationServiceClient();
            Debug.Write("list: " + flights);
            //if (list != null) {
            Debug.WriteLineIf(flights != null, " , Count: " + flights.Count.ToString());
            //}
            GridViewFlights.DataSource = flights;
            GridViewFlights.DataBind();
            var price = flights.Sum(x => x.Route.Price);
            var travelStart = flights.Min(x => x.DepartureTime);
            var travelEnd = flights.Max(x => x.ArrivalTime);
            var travelTime = travelEnd - travelStart;
            lblTotalPrice.Text = string.Format("{0:C}", price);
            lblTravelTime.Text = string.Format("{0:g}", travelTime);
        }

        protected void IntValidator_OnServerValidate(object source, ServerValidateEventArgs args) {
            int i;
            bool valid = int.TryParse(args.Value, out i);
            args.IsValid = valid && i > 0;
        }

        protected void ValidatorDepart_OnServerValidate(object source, ServerValidateEventArgs args) {
            try {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                DateTime.Parse(args.Value);
                args.IsValid = true;
            } catch (Exception) {
                args.IsValid = false;
            }
        }
    }
}