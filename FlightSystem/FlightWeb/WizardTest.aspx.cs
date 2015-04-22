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
                    countries.Insert(0, new ListItem("--- Select ---", null));

                    ddlCountryFrom.Items.AddRange(countries.ToArray());
                    ddlCountryTo.Items.AddRange(countries.ToArray());
                }

                for (int i = 1; i <= 10; i++)
                {
                    ddlPersons.Items.Add(new ListItem(i.ToString()));
                }
            }
            
        }

        protected void ddlCountryFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("hej!");
            ddlFrom.Items.Add("HEJ!");
        }

     
    }
}