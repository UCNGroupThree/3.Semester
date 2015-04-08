﻿using System;
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
using FlightAdmin.GUI.FlightTabExtensions;
using FlightAdmin.GUI.RouteTabExtensions;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class RouteTab : UserControl {

        private readonly RouteCtr _rCtr = new RouteCtr();
        private DataGridViewCellEventArgs _mouseLocation;

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

        #region Button Events

        private void btnSearch_Click(object sender, EventArgs e) {
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
            t.AddRouteEvent += AddRoute;
            t.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtFrom.Text = "";
            txtTo.Text = "";
            routeBindingSource.Clear();
        }

        #endregion

        #region DataGrid

        private void dataRoute_CellMouseEnter(object sender, DataGridViewCellEventArgs location) {
            _mouseLocation = location;
        }

        private void UpdateDataGrid(List<Route> routes) {
            BeginInvoke((MethodInvoker)delegate {
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

        #region Delete / Edit

        private void DeleteRoute(Route route) {
            DialogResult res =
                    MessageBox.Show("Are you sure you wish to delete this route: \n" + route.From + " -> " + route.To,
                        "Remove", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes) {
                try {
                    _rCtr.DeleteRoute(route);
                    routeBindingSource.Remove(route);
                } catch (NullException) {
                    MessageBox.Show("The Route has already been deleted");
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        #endregion

        #region Delete / Edit Event / Create-Edit Flights - DataGrid Menu

        private void removeToolStripMenuItem_Click(object sender, EventArgs e) {
            var route = (Route)dataRoute.Rows[_mouseLocation.RowIndex].DataBoundItem;
            if (route != null) {
                DeleteRoute(route);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void createFlightsToolStripMenuItem_Click(object sender, EventArgs e) {
            var route = (Route)dataRoute.Rows[_mouseLocation.RowIndex].DataBoundItem;
            CreateFlights f = null;
            if (route.Flights == null || route.Flights.Count > 0) {
                f = new CreateFlights(route);
            } else {
                f = new CreateFlights() { Route = route };
            }

            if (f.ShowDialog() == DialogResult.OK) {
                ChangeSelection(route, f.Route);
            }
        }

        #endregion

        #region Misc

        private void ChangeSelection(Route from, Route to) {
            int i = routeBindingSource.IndexOf(from);
            routeBindingSource.Remove(from);
            routeBindingSource.Insert(i, to);
            dataRoute.CurrentCell = dataRoute.Rows[i].Cells[0];
        }

        private void AddRoute(Route route) {
            if (route != null) {
                routeBindingSource.Clear();
                routeBindingSource.Add(route);
            }
        }

        #endregion

    }
}
