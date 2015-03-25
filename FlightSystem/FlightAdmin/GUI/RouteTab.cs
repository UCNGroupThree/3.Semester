using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.Exceptions;
using FlightAdmin.GUI.RouteTabExtensions;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class RouteTab : UserControl {

        private readonly RouteCtr _rCtr = new RouteCtr();

        public RouteTab() {
            InitializeComponent();
        }

        #region Search

        private void SearchRoute(string from, string to) {
            Route route;

            try {
                AirportCtr aCtr = new AirportCtr();
                Airport fromAirport = aCtr.GetAirportsByName(from)[0]; //TODO Change to a "select" list?
                Airport toAirport = aCtr.GetAirportsByName(to)[0];

               route = _rCtr.GetRouteByAirports(fromAirport, toAirport);
            } catch (NullException e) {
                MessageBox.Show(e.Message); //TODO Better error handeling?
                return;
            }

            UpdateDataGrid(new List<Route>(){route});
        }

        private void SearchRoutes(string from) {
            List<Route> routes;

            try {
                AirportCtr aCtr = new AirportCtr();
                Airport fromAirport = aCtr.GetAirportsByName(from)[0]; //TODO Change to a "select" list?

                routes = _rCtr.GetRoutesByAirport(fromAirport);
            } catch (NullException e) {
                MessageBox.Show(e.Message); //TODO Better error handeling?
                return;
            }

            UpdateDataGrid(routes);
        }

        #endregion


        #region DataGrid

        private void UpdateDataGrid(List<Route> routes) {
            BeginInvoke((MethodInvoker) delegate {
                if (routes != null) {
                    routeBindingSource.Clear();
                    foreach (var r in routes) {
                        routeBindingSource.Add(r);
                    }
                    loadingPanel.Visible = false;
                }
            });
        }

        #endregion


        #region Buttons

        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrWhiteSpace(txtFrom.Text)) {
                MessageBox.Show("You must type in the 'From' Airport.");
            } else {
                
                if (string.IsNullOrEmpty(txtTo.Text) || string.IsNullOrWhiteSpace(txtTo.Text)) {
                    loadingPanel.Visible = true;
                    Thread worker = new Thread(new ThreadStart(() => SearchRoutes(txtFrom.Text))); 
                    worker.Start();
                } else {
                    loadingPanel.Visible = true;
                    Thread worker = new Thread(new ThreadStart(() => SearchRoute(txtFrom.Text, txtTo.Text)));
                    worker.Start();
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            Test t = new Test();
            t.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtFrom.Text = "";
            txtTo.Text = "";
            routeBindingSource.Clear();
        }

        #endregion

    }
}
