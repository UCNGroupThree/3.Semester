﻿using System;
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
            if (ddlCountryFrom.SelectedIndex != 0) {
                var sel = ddlFrom.Items[0];
                ddlFrom.Items.Clear();
                ddlFrom.Items.Insert(0, sel);
                ddlFrom.Items.AddRange(GetAirportsFromCountry(ddlCountryFrom.SelectedValue).ToArray());
                ddlFrom.Enabled = true;
            } else {
                ddlFrom.SelectedIndex = 0;
                ddlFrom.Enabled = false;
            }

            UpdatePanel101.Update();
        }

        protected void ddlCountryTo_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlCountryTo.SelectedIndex != 0) {
                var sel = ddlFrom.Items[0];
                ddlTo.Items.Clear();
                ddlTo.Items.Insert(0, sel);
                ddlTo.Items.AddRange(GetAirportsFromCountry(ddlCountryTo.SelectedValue).ToArray());
                ddlTo.Enabled = true;
            } else {
                ddlTo.SelectedIndex = 0;
                ddlTo.Enabled = false;
            }

            UpdatePanel101.Update();
        }



    }
}