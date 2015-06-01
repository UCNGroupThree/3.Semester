using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.MainService;

namespace FlightWeb.Test {
    public partial class Visual : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            var t = DateTime.Today;
            var dt = new DateTime(t.Year, t.Month, t.Day, 0, 1, 0);
            var all = Request.QueryString["all"];
            if (all != null) {
                showAll();
                return;
            }
            var fromStr = Request.QueryString["from"];
            var toStr = Request.QueryString["to"];
            if (fromStr == null && toStr == null) {
                Response.Write("use from and to queryString!");
                Response.End();
            }
            var from = int.Parse(fromStr);
            var to = int.Parse(toStr);

            using (var client = new DijkstraClient()) {
                var list = client.GetShortestPath(from, to, 1, dt);
                if (list == null || list.Count == 0) {
                    Response.Write("No flights founded!");
                    Response.End();
                }
                var first = list.First(f => f.Route.From.ID == from).Route.From;
                var last = list.First(f => f.Route.To.ID == to).Route.To;
                lblHeader.Text = first + " --> " + last;



                HiddenData.Value = first.Latitude + "," + first.Longitude;

                foreach (var airport in list.Select(f => f.Route.To)) {
                    HiddenData.Value += ";" + airport.Latitude + "," + airport.Longitude;
                }

            }





        }

        private void showAll() {
            List<Airport> airports = new List<Airport>();
            using (var client = new AirportServiceClient()) {
                airports = client.GetAirportsByCountry("Denmark");
            }
            using (var client = new RouteServiceClient()) {
                var routes = new HashSet<Route>();
                foreach (var airport in airports) {
                    try {
                        var rList = client.GetRoutesByAirport(airport);
                        foreach (var r in rList) {
                            routes.Add(r);
                        }
                    } catch (Exception) {}
                    
                }

                foreach (var r in routes) {
                    HiddenData.Value += r.From.Latitude + "," + r.From.Longitude +";";
                    HiddenData.Value += r.To.Latitude + "," + r.To.Longitude + ":";
                }
                HiddenData.Value += HiddenData.Value.Substring(0, HiddenData.Value.Length - 1);
                //

            } 
        }
    }
}