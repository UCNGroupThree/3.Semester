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
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                using (AirportServiceClient client = new AirportServiceClient())
                {
                    var list = client.GetCountries();
                    var countries = list.Select(ListItem.FromString).ToList();
                    countries.Insert(0, new ListItem("--- Please select ---", "-1"));

                    ddlCountryFrom.Items.AddRange(countries.ToArray());
                    ddlCountryTo.Items.AddRange(countries.ToArray());
                }

                for (int i = 1; i <= 10; i++)
                {
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
            } else {
                airport.SelectedIndex = 0;
                airport.Enabled = false;
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e) {
            Debug.WriteLine("######");
            Debug.WriteLine(e.CurrentStepIndex);
            Debug.WriteLine(e.NextStepIndex);
            
            if (e.CurrentStepIndex == 0) {
                using (DijkstraClient client = new DijkstraClient()) {
                    Airport from = new Airport() {ID = int.Parse(ddlFrom.SelectedValue)};
                    Airport to = new Airport() {ID = int.Parse(ddlTo.SelectedValue)};
                    DateTime date = DateTime.Parse(txtDepart.Text);
                    Debug.WriteLine("from: {0} - to: {1}", from.ID, to.ID);
                    Debug.WriteLine("date: {0}", date);
                    var list = client.DijkstraStuff(from, to, date);
                    Debug.Write("list: " + list);
                    //if (list != null) {
                        Debug.WriteLineIf(list != null, " , Count: " +  list.Count.ToString());
                    //}
                        GridViewFlights.DataSource = list;
                        GridViewFlights.DataBind();
                    var price = list.Sum(x => x.Price);
                    var travelStart = list.Min(x => x.DepartureTime);
                    var travelEnd = list.Max(x => x.ArrivalTime);
                    var travelTime = travelEnd - travelStart;
                    lblTotalPrice.Text = string.Format("{0:C}", price);
                    lblTravelTime.Text = string.Format("{0}", travelTime);
                }
            }
            Debug.WriteLine("######");
            

        }



    }
}