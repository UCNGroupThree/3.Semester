﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.Exceptions;
using FlightWeb.Helper;
using FlightWeb.MainService;
using Microsoft.Ajax.Utilities;

namespace FlightWeb {
    public partial class Search : System.Web.UI.Page {

        private ResSession ses;

        protected void Page_Load(object sender, EventArgs e) {
            
            ses = ResSession.Current(Session);
            if (!Page.IsPostBack) {
               FirstLoad();

                var diaHelper = Session["Dialog"] as DialogHelper;
                if (diaHelper != null) {
                    DisplayError(diaHelper.Header, diaHelper.Text, true);
                    Session.Remove("Dialog");
                }
                Demo();
            }
        }

        private void DisplayError(string header, string text, bool loadOnRequest) {

            modalHeaderText.InnerHtml = header;
            lblModalError.Text = text;
            lblModalError.Visible = true;
            modalFlightsPanel.Visible = false;
            btnBook.Visible = false;
            if (loadOnRequest) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divModal", "$('#divModal').modal();", true);
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

        protected void btnSearch_OnClick(object sender, EventArgs e) {
            
            Page.Validate("FindFligtValidator");
            if (!Page.IsValid) {
                modalHeaderText.InnerHtml = "Error";
                lblModalError.Text = "Some of the input fields are not valid! :(";
                lblModalError.Visible = true;
                modalFlightsPanel.Visible = false;
                btnBook.Visible = false;
                return;
            }
            int fromId = int.Parse(ddlFrom.SelectedValue);
            int toId = int.Parse(ddlTo.SelectedValue);
            int seats = int.Parse(ddlPersons.SelectedValue);
            DateTime date = DateTime.Parse(txtDepart.Text);
            
            try {
                ses.Flights = null;
                ses.Ticket = null;
                User user;
                try {
                    using (var userClient = new UserServiceClient()) {
                        var email = HttpContext.Current.User.Identity.Name;
                        if (string.IsNullOrEmpty(email)) {
                            throw new NullException("Please login before searching :)");
                        }
                        user = userClient.GetUsersByEmail(email, true).First();
                    }
                } catch (Exception ex) {
                    if (ex is NullException) {
                        throw;
                    }
                    throw new NullException("An Error happen in getting your user details.", ex);
                }

                var client = ses.GetNewResClient();
                    
                    var list = client.GetFlightsAsd(fromId, toId, seats, date, user);
                    if (list != null && list.Count > 0) {
                        ses.Flights = list;
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
                        lblModalTravelTime.Text = string.Format("{0}", travelTime.ToFineString());

                        modalHeaderText.InnerHtml = "Possible departure found";
                        btnBook.Visible = true;
                        lblModalError.Visible = false;
                        modalFlightsPanel.Visible = true;
                    } else {
                        throw new SubmitException("We are sorry, but we coundn't find any available flights :(");
                    }
                
            } catch (Exception ex) {
                //TODO bedre håndtering af fejl
                if (ex is NullException) {
                    DisplayError("Error", ex.Message, false);
                } else if (ex is SubmitException) {
                    DisplayError("No Flight Found", ex.Message, false);
                } else if (ex is FaultException<NullPointerFault>) {
                    DisplayError("Error", "You have to be sign in, before you can search!", false);
                } else if (ex is FaultException<LockedFault>) {
                    DisplayError("Error", "Please Try again.. The system is busy!", false);
                } else {
                    DisplayError("Error", "We are sorry, but an error happen :(", false);
                }

                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine("ERROR!");
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
            }
            
            
            UpdatePanelAnswer.Update();
        }
        
        protected void btnBook_OnClick(object sender, EventArgs e) {
            
        }
    }
}