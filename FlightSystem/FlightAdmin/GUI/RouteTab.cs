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
using Common.Exceptions;
using FlightAdmin.Controller;
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

        private void bgWorker_DoWork_SearchFrom(object sender, DoWorkEventArgs e) {
            var aCtr = new AirportCtr();
            var fromAirport = GetAirport((string)e.Argument);

            e.Result = _rCtr.GetRoutesByAirport(fromAirport);
        }

        private void bgWorker_RunWorkerCompleted_SearchFrom(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                var routes = e.Result as List<Route>;
                UpdateDataGrid(routes);
            }

            loadingIcon.Visible = false;
        }

        private void bgWorker_DoWork_SearchFromTo(object sender, DoWorkEventArgs e) {
            var from = ((Tuple<string, string>) e.Argument).Item1;
            var to = ((Tuple<string, string>)e.Argument).Item2;

            var aCtr = new AirportCtr();
            var fromAirport = GetAirport(from);
            var toAirport = GetAirport(to);

            e.Result = _rCtr.GetRouteByAirports(fromAirport, toAirport);
        }

        private void bgWorker_RunWorkerCompleted_SearchFromTo(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                var route = e.Result as Route;
                UpdateDataGrid(new List<Route>() { route });
            }

            loadingIcon.Visible = false;
        }

        private Airport GetAirport(string airport) {
            AirportCtr aCtr = new AirportCtr();
            List<Airport> airports = aCtr.GetAirportsByName(airport);
            if (!(airports.Count > 0)) {
                throw new NullException(string.Format("No airport with the name {0} exists", airport));
            }

            return airports[0];
        }

        #endregion

        #region Button Events

        private void btnSearch_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrWhiteSpace(txtFrom.Text)) {
                MessageBox.Show("You must type in the 'From' Airport.");
            } else {
                if (string.IsNullOrEmpty(txtTo.Text) || string.IsNullOrWhiteSpace(txtTo.Text)) {
                    loadingIcon.Visible = true;
                    BackgroundWorker bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += bgWorker_DoWork_SearchFrom;
                    bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted_SearchFrom;
                    bgWorker.RunWorkerAsync(txtFrom.Text);
                } else {
                    loadingIcon.Visible = true;
                    BackgroundWorker bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += bgWorker_DoWork_SearchFromTo;
                    bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted_SearchFromTo;
                    bgWorker.RunWorkerAsync(new Tuple<string, string>(txtFrom.Text, txtTo.Text));
                }
            }
        }

        

        private void btnCreate_Click(object sender, EventArgs e) {
            CreateRouteMain t = new CreateRouteMain();
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
            if (routes != null) {
                routeBindingSource.Clear();
                foreach (var r in routes) {
                    routeBindingSource.Add(r);
                }
            }
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
                } catch (DeleteException ex) {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var route = (Route)dataRoute.Rows[_mouseLocation.RowIndex].DataBoundItem;
            if (route != null) {
                CreateRouteMain t = new CreateRouteMain(route);
                t.AddRouteEvent += AddRoute;
                t.ShowDialog();
            }
        }

        private void createFlightsToolStripMenuItem_Click(object sender, EventArgs e) {
            var route = (Route)dataRoute.Rows[_mouseLocation.RowIndex].DataBoundItem;
            CreateFlights f = null;
            if (route.Flights == null || route.Flights.Count > 0) {
                f = new CreateFlights(route);
            } else {
                f = new CreateFlights() { Route = route };
            }

            DialogResult rs = f.ShowDialog();

            if (rs == DialogResult.OK) {
                ChangeSelection(route, f.Route);
            } else if (rs == DialogResult.Retry) {
                UpdateDataGrid(null);
            }
        }

        #endregion

        #region Misc

        private void ChangeButton(IButtonControl btnAccept, IButtonControl btnCancel) {
            var f = FindForm();
            if (f != null) {
                f.AcceptButton = btnAccept;
                f.CancelButton = btnCancel;
            }
        }
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
            } else {
                routeBindingSource.Clear();
            }
        }

        #endregion

        private void ChangeButtonFocut_Enter(object sender, EventArgs e) {
            ChangeButton(btnSearch, btnClear);
        }

    }
}
