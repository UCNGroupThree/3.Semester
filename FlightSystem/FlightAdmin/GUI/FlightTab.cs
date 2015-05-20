using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.FlightTabExtensions;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class FlightTab : UserControl {

        private FlightCtr _fCtr = new FlightCtr();
        private DataGridViewCellEventArgs _mouseLocation;
        public FlightTab() { //Todo Nick D PEdersen
            InitializeComponent();
            txtID.OnlyInt = true;
            SetEvents();
        }

        #region TextBox Events

        private void SetEvents() {
            txtFrom.TextChanged += TextChanged;
            txtTo.TextChanged += TextChanged;

            txtID.TextChanged += delegate(object sender, EventArgs args) {
                if ((sender as TextBox).Text.Length > 0) {
                    txtTo.Enabled = false;
                    txtFrom.Enabled = false;
                } else {
                    txtTo.Enabled = true;
                    txtFrom.Enabled = true;
                }
            };
        }

        private void TextChanged(object sender, EventArgs args) {
            if ((sender as TextBox).Text.Length > 0) {
                txtID.Enabled = false;
            } else {
                txtID.Enabled = true;
            }
        }

        #endregion

        #region Search

        private Airport GetAirport(string name) {
            AirportCtr aCtr = new AirportCtr();
            List<Airport> air = aCtr.GetAirportsByName(name);
            Airport airport = null;

            if (air != null && air.Count > 0) {
                airport = air[0];
            }

            return airport;
        }

        #endregion

        #region DataGrid

        private void UpdateDataGrid(List<Flight> flights) {
            if (flights != null) {
                flightBindingSource.Clear();
                foreach (var f in flights) {
                    flightBindingSource.Add(f);
                }
                loadingPanel.Visible = false;
            }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e) {
            Search();
        }

        private void Search() {
            if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrWhiteSpace(txtFrom.Text)) {
                if (!string.IsNullOrEmpty(txtID.Text) || !string.IsNullOrWhiteSpace(txtID.Text)) {
                    SearchByID(txtID.IntValue);
                }
            } else {
                if (string.IsNullOrEmpty(txtTo.Text) || string.IsNullOrWhiteSpace(txtTo.Text)) {
                    MessageBox.Show(@"The 'To' field can't be empty!");
                } else {
                    SearchByFrom(txtFrom.Text, txtTo.Text);
                }
            }
        }

        private void SearchByFrom(string from, string to) {
            loadingIcon.Visible = true;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += (sender, args) => bgWorker_SearchByFrom_DoWork(args, from, to);
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
        }

        private void SearchByID(int id) {
            loadingIcon.Visible = true;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += (sender, args) => bgWorker_SearchByID_DoWork(args, id);
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
        }


        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                List<Flight> flights = e.Result as List<Flight>;
                UpdateDataGrid(flights);
            }

            loadingIcon.Visible = false;
        }

        private void bgWorker_SearchByFrom_DoWork(DoWorkEventArgs e, string from, string to) {
            AirportCtr aCtr = new AirportCtr();

            Airport fAirport = GetAirport(from);

            Airport tAirport = GetAirport(to);

            if (fAirport == null) {
                throw new Exception("The Airport: " + from + " does not exist");
            }else if (tAirport == null) {
                throw new Exception("The Airport: " + to + " does not exist");
            } else if (fAirport.ID == tAirport.ID) {
                throw new Exception("To and From, can't be the same airport");
            }

            e.Result = _fCtr.GetFlights(fAirport, tAirport);
        }

        private void bgWorker_SearchByID_DoWork(DoWorkEventArgs e, int id) {
            e.Result = new List<Flight>() {_fCtr.GetFlight(id)};
        }

        private void btnClear_Click(object sender, EventArgs e) {
            Clear();
        }

        private void Clear() {
            txtFrom.Text = "";
            txtTo.Text = "";
            txtID.Text = "";
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e) {
            var flight = (Flight)dataFlight.Rows[_mouseLocation.RowIndex].DataBoundItem;
            if (flight != null) {
                DeleteFlight(flight);
            }
        }

        private void DeleteFlight(Flight flight) {
            try {
                _fCtr.DeleteFlight(flight);

                MessageBox.Show(String.Format("Flight: {0} \n has been deleted!", flight.ID));
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private void dataRoute_CellMouseEnter(object sender, DataGridViewCellEventArgs location) {
            _mouseLocation = location;
        }

        private void ChangeButtonFocut_Enter(object sender, EventArgs e) {
            ChangeButton(btnSearch, btnClear);
        }

        private void ChangeButton(IButtonControl btnAccept, IButtonControl btnCancel) {
            var f = FindForm();
            if (f != null) {
                f.AcceptButton = btnAccept;
                f.CancelButton = btnCancel;
            }
        }

    }
}
