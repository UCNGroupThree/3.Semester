using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.RouteTabExtensions {
    public partial class CreateRoute : UserControl {

        #region Events

        public delegate void Close();
        public event Close CloseEvent;

        public delegate void AddRoute(Route route);
        public event AddRoute AddRouteEvent;

        #endregion

        #region Properties

        public bool Working { get; private set; }

        private bool _isEdit;
        private Route edRoute;

        #endregion

        public CreateRoute() {
            InitializeComponent();
            _isEdit = false;
            Debug.WriteLine("CreateRoute: Create");
        }

        public CreateRoute(Route route) {
            InitializeComponent();
            _isEdit = true;
            edRoute = route;
            txtPrice.Text = edRoute.Price.ToString();
            btnCreate.Text = "Save";
            btnCreate.Click -= btnCreate_Click;
            btnCreate.Click += btnCreateUpdate_Click;
            Debug.WriteLine("CreateRoute: Edit");
        }

        #region BackgroundWorker

        private void bgWorker_RunWorkerCompleted_Countries(object sender, RunWorkerCompletedEventArgs e) {
            var countriesFrom = e.Result as List<string>;
            var countriesTo = countriesFrom.ToList();

            cmbFromCountry.DataSource = countriesFrom;
            cmbToCountry.DataSource = countriesTo;

            if (edRoute != null) {
                cmbToCountry.SelectedItem = edRoute.To.Country;
                cmbFromCountry.SelectedItem = edRoute.From.Country;
            }

            btnClose.Enabled = true;
            loadFromCountry.Visible = false;
            loadToCountry.Visible = false;
        }

        private void LoadCountries(object sender, DoWorkEventArgs e) {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var countriesFrom = client.GetCountries();
                countriesFrom.Insert(0, "-");

                e.Result = countriesFrom;
            }
        }

        private void bgWorker_RunWorkerCompleted_Airports(object sender, RunWorkerCompletedEventArgs e) {
            var cmb = ((Tuple<ComboBox, Airport[]>)e.Result).Item1;
            var airports = ((Tuple<ComboBox, Airport[]>)e.Result).Item2.ToList();

            cmb.DataSource = airports;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";

            if (edRoute != null) {
                cmbFromAirport.SelectedValue = edRoute.From.ID;
                cmbToAirport.SelectedValue = edRoute.To.ID;
            }

            cmb.Enabled = true;
            btnClose.Enabled = true;
            loadFromAirport.Visible = false;
            loadToAirport.Visible = false;
        }

        private void LoadAirports(object sender, DoWorkEventArgs e) {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var args = ((Tuple<string, ComboBox>) e.Argument);
                var airports = client.GetAirportsByCountry(args.Item1);

                e.Result = new Tuple<ComboBox, Airport[]>(args.Item2, airports.ToArray());
            }
        }

        #endregion

        #region Loader

        private void CreateRoute_Load(object sender, EventArgs e) {
            try {
                BackgroundWorker bgWorker = new BackgroundWorker();
                btnClose.Enabled = false;
                loadFromCountry.Visible = true;
                loadToCountry.Visible = true;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted_Countries;
                bgWorker.DoWork += LoadCountries;
                bgWorker.RunWorkerAsync();
            } catch (Exception ex) {
#if (DEBUG)
                ex.DebugGetLine();
#endif
                if (CloseEvent != null) {
                    CloseEvent();
                }

                Dispose();
            }
        }

        #endregion

        #region cmbCountry

        private void cmbFromCountry_SelectedIndexChanged(object sender, EventArgs e) {
            string country = cmbFromCountry.Text;
            if (!country.Equals("-")) {
                loadFromAirport.Visible = true;
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted_Airports;
                bgWorker.DoWork += LoadAirports;
                bgWorker.RunWorkerAsync(new Tuple<string, ComboBox>(country, cmbFromAirport));
            } else {
                cmbFromAirport.DataSource = null;
                cmbFromAirport.Enabled = false;
            }
        }

        private void cmbToCountry_SelectedIndexChanged(object sender, EventArgs e) {
            string country = cmbToCountry.Text;
            if (!country.Equals("-")) {
                loadToAirport.Visible = true;
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted_Airports;
                bgWorker.DoWork += LoadAirports;
                bgWorker.RunWorkerAsync(new Tuple<string, ComboBox>(country, cmbToAirport));
            } else {
                cmbToAirport.DataSource = null;
                cmbToAirport.Enabled = false;
            }
        }

        #endregion

        #region Create / Updated

        private void btnCreate_Click(object sender, EventArgs e) {
            if (ValidateRoute()) {
                try {
                    RouteCtr rCtr = new RouteCtr();
                    Route route = rCtr.CreateRoute((Airport)cmbFromAirport.SelectedItem, (Airport)cmbToAirport.SelectedItem, null,
                        decimal.Parse(txtPrice.Text));

                    MessageBox.Show(String.Format("The Route:\n {0} -> {1} \n has been created!", route.From.Name, route
                        .To.Name), "Route Created", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    if (AddRouteEvent != null)
                        AddRouteEvent(route);
                    if (CloseEvent != null)
                        CloseEvent();

                } catch (ValidationException ex) {
#if (DEBUG)
                    ex.DebugGetLine();
#endif
                    //TODO Something here
                } catch (Exception ex) {
#if (DEBUG)
                    ex.DebugGetLine();
#endif


                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCreateUpdate_Click(object sender, EventArgs e) {
            if (ValidateRoute()) {
                try {
                    RouteCtr rCtr = new RouteCtr();
                    Route route = rCtr.UpdateRoute(edRoute, (Airport)cmbFromAirport.SelectedItem,
                        (Airport)cmbToAirport.SelectedItem, edRoute.Flights, decimal.Parse(txtPrice.Text));

                    MessageBox.Show(String.Format("The Route:\n {0} -> {1} \n has been Updated!", route.From.Name, route
                        .To.Name), "Route Created", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    if (AddRouteEvent != null)
                        AddRouteEvent(route);
                    if (CloseEvent != null)
                        CloseEvent();

                } catch (ValidationException ex) {

#if (DEBUG)
                    ex.DebugGetLine();
#endif
                    //TODO Something here
                } catch (DatabaseException ex) {
#if (DEBUG)
                    ex.DebugGetLine();
#endif
                    MessageBox.Show("This Route is not the same as the Route in the database,\n you must search for the Route again..");
                    if (CloseEvent != null)
                        CloseEvent();
                } catch (Exception ex) {
#if (DEBUG)
                    ex.DebugGetLine();
#endif
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool ValidateRoute() {
            try {
                decimal.Parse(txtPrice.Text);
                var from = (Airport)cmbFromAirport.SelectedItem;
                var to = (Airport)cmbToAirport.SelectedItem;

                if (from.ID == to.ID) {
                    epRoute.SetError(cmbFromAirport, "The 'From' and 'To' Airports can't be the same");
                    epRoute.SetError(cmbToAirport, "The 'From' and 'To' Airports can't be the same");
                    return false;
                }
            } catch (NullReferenceException ex) {
#if (DEBUG)
                ex.DebugGetLine();
#endif
                epRoute.SetError(cmbFromAirport, "Airports must be selected");
                epRoute.SetError(cmbToAirport, "Airports must be selected");

                return false;
            } catch (Exception ex) {
#if (DEBUG)
                ex.DebugGetLine();
#endif
                epRoute.SetError(txtPrice, "Invalid Price");
                return false;
            }

            return true;
        }

        #endregion

        #region Close

        private void btnClose_Click(object sender, EventArgs e) {
            if (CloseEvent != null)
                CloseEvent();
        }

        #endregion

        #region Error Provider Clear

        private void On_Enter(object sender, EventArgs e) {
            var control = sender as Control;
            if (control != null) {
                epRoute.SetError(control, "");
            }
        }

        #endregion

    }
}
