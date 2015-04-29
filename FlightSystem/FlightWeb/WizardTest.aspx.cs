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

        private static List<Flight> flights = new List<Flight>();

        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
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
            }

        }

        [WebMethod]
        public static List<ListItem> GetAirportsFromCountry(string country) {
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
            Debug.WriteLine("######");
            Debug.WriteLine(e.CurrentStepIndex);
            Debug.WriteLine(e.NextStepIndex);

            if (e.CurrentStepIndex == 0) {

                int fromId = int.Parse(ddlFrom.SelectedValue);
                int toId = int.Parse(ddlTo.SelectedValue);
                int seats = int.Parse(ddlPersons.SelectedValue);
                DateTime date = DateTime.Parse(txtDepart.Text);

                Debug.WriteLine("from: {0} - to: {1}", fromId, toId);
                Debug.WriteLine("date: {0}", date);

                try {
                    using (DijkstraClient client = new DijkstraClient()) {

                        var list = client.DijkstraStuff(fromId, toId, seats, date);
                        if (list != null && list.Count > 0) {

                            flights = list;
                            var first = flights[0];
                            var stops = flights.Count-1;
                            var last = flights[stops];

                            var price = flights.Sum(x => x.Price);
                            var travelTime = last.DepartureTime - first.ArrivalTime;

                            lblStep2From.Text = first.From.ToString();
                            lblStep2To.Text = last.To.ToString();
                            if (stops > 0) {
                                lblStep2Stops.Text = stops.ToString();
                            } else {
                                lblStep2Stops.Text = "directly";
                            }
                            lblStep2Price.Text = string.Format("{0:C}", price);
                            lblStep2Time.Text = string.Format("{0:g}", travelTime);

                        }

                    }
                }
                catch (Exception) {
                    Debug.WriteLine("ERROR!");
                    Debug.WriteLine("ERROR!");
                    Debug.WriteLine("ERROR!");
                }

            } else if (e.CurrentStepIndex == 1) {
                        Debug.Write("list: " + flights);
                        //if (list != null) {
                        Debug.WriteLineIf(flights != null, " , Count: " + flights.Count.ToString());
                        //}
                        GridViewFlights.DataSource = flights;
                        GridViewFlights.DataBind();
                        var price = flights.Sum(x => x.Price);
                        var travelStart = flights.Min(x => x.DepartureTime);
                        var travelEnd = flights.Max(x => x.ArrivalTime);
                        var travelTime = travelEnd - travelStart;
                        lblTotalPrice.Text = string.Format("{0:C}", price);
                        lblTravelTime.Text = string.Format("{0:g}", travelTime);
            }
            Debug.WriteLine("######");


        }

    }
}