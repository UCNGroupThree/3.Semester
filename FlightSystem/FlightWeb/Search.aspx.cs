using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.MainService;

namespace FlightWeb {
    public partial class Search : System.Web.UI.Page {

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
            /*
            for (int i = 1; i <= 10; i++) {
                ddlPersons.Items.Add(new ListItem(i.ToString()));
            }*/
            var today = DateTime.Now;
            txtDepart.Text = new DateTime(today.Year, today.Month, today.Day, 0, 1, 0).ToString("g");
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
            } else {
                airport.SelectedIndex = 0;
                airport.Enabled = false;
            }
        }
        /*
        [WebMethod]
        public static SearchHelper SearchAsync(string from, string to, string depart, string persons) {
            var helper = new SearchHelper {
                ErrorText = "fejlTekst",
                Flight =
                    new List<Flight> {new Flight {DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now, ID = 2}},
                Title = "title?"
            };
            return helper;
        }
        */
        protected void IntValidator_OnServerValidate(object source, ServerValidateEventArgs args) {
            throw new NotImplementedException();
        }

        protected void ValidatorDepart_OnServerValidate(object source, ServerValidateEventArgs args) {
            throw new NotImplementedException();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e) {

            int fromId = int.Parse(ddlFrom.SelectedValue);
            int toId = int.Parse(ddlTo.SelectedValue);
            int seats = int.Parse(ddlPersons.SelectedValue);
            DateTime date = DateTime.Parse(txtDepart.Text);
            
            try {
                //TODO skal erstattes med et andet endpoint
                Session["flights"] = null;
                //ClearSession();
                using (var client = new ReservationServiceClient()) {
                    var list = client.GetFlightsAsd(fromId, toId, seats, date);
                    if (list != null && list.Count > 0) {
                        Session["flights"] = list;
                        var first = list[0];
                        var stops = list.Count - 1;
                        var last = list[stops];

                        var price = list.Sum(x => x.Route.Price);
                        var travelTime = last.DepartureTime - first.ArrivalTime;

                        lblModalFrom.Text = first.Route.From.ToString();
                        lblModalTo.Text = last.Route.To.ToString();
                        if (stops > 0) {
                            lblModalStops.Text = stops.ToString();
                        } else {
                            lblModalStops.Text = "directly";
                        }
                        lblModalPrice.Text = string.Format("{0:C}", price);
                        lblModalPrice.Text = string.Format("{0:g}", travelTime);

                        modalHeaderText.InnerHtml = "Possible departure found";
                        btnBook.Visible = true;
                        lblModalError.Visible = false;
                        modalFlightsPanel.Visible = true;
                    } else {
                        modalHeaderText.InnerHtml = "No flights found";
                        //lblModelError.Text = "We are sorry, but we coundn't find any available flights :(";
                        
                        lblModalError.Text = "We are sorry, but we coundn't find any available flights :(";
                        lblModalError.Visible = true;
                        modalFlightsPanel.Visible = false;
                        btnBook.Visible = false;

                        //modalErrorPanel.Enabled = true;
                        //modalFlightsPanel.Enabled = false;
                    }
                }
            } catch (Exception ex) {
                //TODO bedre håndtering af fejl
                lblModalError.Text = "We are sorry, but we coundn't find any available flights :(";
                lblModalError.Visible = true;
                modalFlightsPanel.Visible = false;
                btnBook.Visible = false;

                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
            }
            
            //System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divModal", "$('#divModal').modal();", true);
            
            UpdatePanelAnswer.Update();
        }

        protected void btnBook_OnClick(object sender, EventArgs e) {
            
        }
    }
}