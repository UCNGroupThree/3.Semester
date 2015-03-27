using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.Exceptions;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.RouteTabExtensions {
    public partial class CreateRoute : UserControl {

        public delegate void Close();
        public event Close CloseReady;
        public bool Working { get; private set; }

        public CreateRoute() {
            InitializeComponent();
        }

        #region BackgroundWorker

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            btnClose.Enabled = true;
        }

        private void LoadCountries(object sender, DoWorkEventArgs e) {
            BeginInvoke((MethodInvoker) delegate {
                loadFromCountry.Visible = true;
                loadToCountry.Visible = true;
            });

            List<string> countriesFrom;
            List<string> countriesTo = new List<string>();
            using (AirportServiceClient client = new AirportServiceClient()) {
                countriesFrom = client.GetCountries();
            }

            countriesFrom.Insert(0, "-");
            countriesTo = countriesFrom.ToList();

            BeginInvoke((MethodInvoker) delegate {
                cmbFromCountry.DataSource = countriesFrom;
                cmbToCountry.DataSource = countriesTo;
                loadFromCountry.Visible = false;
                loadToCountry.Visible = false;
            });
        }

        private void LoadAirports(BGHelper helper, string country) {
            ComboBox cmb = helper.Item as ComboBox;
            BeginInvoke((MethodInvoker)delegate {
                helper.Loader.Visible = true;
            });

            List<Airport> airports;

            using (AirportServiceClient client = new AirportServiceClient()) {
                airports = client.GetAirportsByCountry(country);
            }

            BeginInvoke((MethodInvoker)delegate {
                cmb.DataSource = airports;
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";
                helper.Loader.Visible = false;
                cmb.Enabled = true;
            });
        }

        #endregion

        #region Loader

        private void CreateRoute_Load(object sender, EventArgs e) {
            try {
                BackgroundWorker bgWorker = new BackgroundWorker();
                btnClose.Enabled = false;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;    
                bgWorker.DoWork += new DoWorkEventHandler(LoadCountries);
                bgWorker.RunWorkerAsync();
            } catch (Exception) {
                if (CloseReady != null) {
                    CloseReady();
                }

                this.Dispose();
            }
        }

        #endregion

        #region cmbCountry

        private void cmbFromCountry_SelectedIndexChanged(object sender, EventArgs e) {
            string country = cmbFromCountry.Text;
            if (!country.Equals("-")) {
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                BGHelper helper = new BGHelper() { Item = cmbFromAirport , Loader = loadFromAirport};
                bgWorker.DoWork += (obj, ex) => LoadAirports(helper, country);
                bgWorker.RunWorkerAsync();
            } else {
                cmbFromAirport.DataSource = null;
                cmbFromAirport.Enabled = false;
            }
        }

        private void cmbToCountry_SelectedIndexChanged(object sender, EventArgs e) {
            string country = cmbToCountry.Text;
            if (!country.Equals("-")) {
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                BGHelper helper = new BGHelper() { Item = cmbToAirport, Loader = loadToAirport };
                bgWorker.DoWork += (obj, ex) => LoadAirports(helper, country);
                bgWorker.RunWorkerAsync();
            } else {
                cmbToAirport.DataSource = null;
                cmbToAirport.Enabled = false;
            }
        }

        #endregion

        #region Create

        private void btnCreate_Click(object sender, EventArgs e) {
            if (ValidateRoute()) {
                try {
                    RouteCtr rCtr = new RouteCtr();
                    rCtr.CreateRoute((Airport) cmbFromAirport.SelectedItem, (Airport) cmbToAirport.SelectedItem, null,
                        decimal.Parse(txtPrice.Text));
                    if (CloseReady != null)
                        CloseReady();
                } catch (ValidationException exception) {
                    //TODO Something here
                } catch (Exception ex) {
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
            } catch (NullReferenceException) {
                epRoute.SetError(cmbFromAirport, "Airports must be selected");
                epRoute.SetError(cmbToAirport, "Airports must be selected");
                return false;
            } catch (Exception) {
                epRoute.SetError(txtPrice, "Invalid Price");
                return false;
            }

            return true;
        }

        #endregion

        #region Close

        private void btnClose_Click(object sender, EventArgs e) {
            if (CloseReady != null)
                CloseReady();
        }

        #endregion

        private void On_Enter(object sender, EventArgs e) {
            var control = sender as Control;
            if (control != null) {
                epRoute.SetError(control, "");
            }
        }
    }
}
