using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FlightAdmin.Controller;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.AirportTabExtensions {
    public partial class CreateAirport : Form {
        private readonly AirportCtr ctr = new AirportCtr();
        private string nextShortName = null;

        public CreateAirport() {
            InitializeComponent();
            SetEvents();
        }

        private void SetEvents() {
            txtShortName.TextChanged += (sender, args) => nextShortName = null;
        }


        #region Validating methods

        private bool IsShortNameValid() {
            bool valid = FancyFeatures.IsTextBoxValid(txtShortName, errProvider, lblShortName.Text, 0, 3);
            return valid;
        }

        private bool IsNameValid() {
            return FancyFeatures.IsTextBoxValid(txtName, errProvider, lblName.Text, 3, 60);
        }

        private bool IsCityValid() {
            return FancyFeatures.IsTextBoxValid(txtCity, errProvider, lblCity.Text, 3, 60);
        }

        private bool IsCountryValid() {
            return FancyFeatures.IsTextBoxValid(txtCountry, errProvider, lblCountry.Text, 3, 60);
        }

        private bool IsLatitudeValid() {
            return FancyFeatures.IsTextBoxDoubleValid(txtLatitude, errProvider, lblLatitude.Text, -90, 90, false);
        }

        private bool IsLongitudeValid() {
            return FancyFeatures.IsTextBoxDoubleValid(txtLongitude, errProvider, lblLongitude.Text, -90, 90, false);
        }

        private bool IsAltitudeValid() {
            return FancyFeatures.IsTextBoxDoubleValid(txtAltitude, errProvider, txtAltitude.Text, -90, 90, false);
        }

        private bool IsTimeZoneValid() {
            //TODO
            return true;
        }

        #endregion

        #region Validating Events

        private void txtShortName_Validating(object sender, CancelEventArgs e) {
            if (IsShortNameValid() && !string.IsNullOrWhiteSpace(txtShortName.Text)) {
                nextShortName = txtShortName.Text.Trim();
                if (!bgWorker.IsBusy) {
                    bgWorker.RunWorkerAsync();
                }
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e) {
            IsNameValid();
        }

        private void txtCity_Validating(object sender, CancelEventArgs e) {
            IsCityValid();
        }

        private void txtCountry_Validating(object sender, CancelEventArgs e) {
            IsCountryValid();
        }

        private void txtLatitude_Validating(object sender, CancelEventArgs e) {
            IsLatitudeValid();
        }

        private void txtLongitude_Validating(object sender, CancelEventArgs e) {
            IsLongitudeValid();
        }

        private void txtAltitude_Validating(object sender, CancelEventArgs e) {
            IsAltitudeValid();
        }

        private void txtTimeZone_Validating(object sender, CancelEventArgs e) {
            IsTimeZoneValid();
        }

        #endregion

        #region Other Events

        private void txtShortName_TextChanged(object sender, EventArgs e) {

        }

        private void txtFirstLetterUpper_TextChanged(object sender, EventArgs eventArgs) {
            TextBox txt = sender as TextBox;
            string str = txt.Text;

            if (string.IsNullOrEmpty(str)) {
                return;
            }

            txt.Text = str.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + str.Substring(1);
        }

        private void button1_Click(object sender, EventArgs e) {
            //Console.WriteLine("validating: " + FancyFeatures.IsTextBoxValid(txtShortName, errorProvider1, 1, 1));
            //errorProvider1.SetError(txtShortName, "Test");
            if (bgWorker.IsBusy) {
                bgWorker.CancelAsync();
            }
        }

        private void txtRemoveErrorOn_TextChanged(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            if (txt != null) {
                errProvider.SetError(txt, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Dispose();
        }

        #endregion

        #region BackgroundWorker for txtShortName

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e) {
            string exists = null;
            while (nextShortName != null && !bgWorker.CancellationPending) {
                string search = nextShortName;
                List<Airport> list = ctr.GetAirportsByShortName(search, true);

                if (list != null && list.Any()) {
                    exists = search;
                } else {
                    exists = null;
                }
                //Console.WriteLine("Exits: " + search + ": " + exists);
                //Console.WriteLine("next: " + nextShortName);
                if (search.Equals(nextShortName)) {
                    nextShortName = null;
                }
                //Thread.Sleep(3000);
            }
            if (bgWorker.CancellationPending) {
                e.Cancel = true;
            }
            e.Result = exists;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled && e.Result != null && !txtShortName.ContainsFocus) {
                txtCity.Text = e.Result.ToString();
                errProvider.SetError(txtShortName, e.Result + " already exists!");
            }
        }

        #endregion

    }
}
