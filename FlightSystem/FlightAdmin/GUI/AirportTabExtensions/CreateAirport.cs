using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FlightAdmin.Exceptions;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.AirportTabExtensions {
    public sealed partial class CreateAirport : Form {
        private readonly AirportCtr ctr = new AirportCtr();
        private string _nextShortName = null; //used by backgroundWorker
        public Airport Airport { get; private set; }
        private bool IsEditing { get; set; }

        /// <summary>
        /// Create new Airport
        /// </summary>
        public CreateAirport() {
            InitializeComponent();
            SetShortNameEvent();
            InitializeTimeZones();
            cmbTimeZone.SelectedItem = TimeZoneInfo.Local;
        }

        /// <summary>
        /// Edit an Airport
        /// </summary>
        /// <param name="airport" >The airport to edit</param>
        /// <exception cref="ArgumentNullException" />
        public CreateAirport(Airport airport) : this() {
            if (airport == null) throw new ArgumentNullException();
            
            Airport = airport;
            IsEditing = true;
            string editText = string.Format("Edit Airport - #{0}", airport.ID);
            Text = editText;
            lblHeader.Text = editText;

            txtShortName.Text = airport.ShortName;
            txtName.Text = airport.Name;
            txtCity.Text = airport.City;
            txtCountry.Text = airport.Country;
            txtLatitude.Text = airport.Latitude.ToString(CultureInfo.CurrentCulture);
            txtLongitude.Text = airport.Longitude.ToString(CultureInfo.CurrentCulture);
            txtAltitude.Text = airport.Altitude.ToString(CultureInfo.CurrentCulture);
            cmbTimeZone.SelectedItem = airport.TimeZone;

            btnSave.Click -= btnSaveForCreation_Click;
            btnSave.Text = @"Save";
            btnSave.Click += btnSaveForEditing_Click;

        }

        private void btnSaveForEditing_Click(object sender, EventArgs eventArgs) {
            MessageBox.Show("Editing!");
        }

        private void InitializeTimeZones() {
            cmbTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
        }
        
        private void SetShortNameEvent() {
            txtShortName.TextChanged += (sender, args) => _nextShortName = null;
        }


        #region Validating methods

        private bool IsShortNameValid() {
            return FancyFeatures.IsTextBoxValid(txtShortName, errProvider, lblShortName.Text, 0, 3);
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
            return FancyFeatures.IsTextBoxDoubleValid(txtLongitude, errProvider, lblLongitude.Text, -180, 180, false);
        }

        private bool IsAltitudeValid() {
            return FancyFeatures.IsTextBoxDoubleValid(txtAltitude, errProvider, txtAltitude.Text, -90, 90, false);
        }

        private bool IsTimeZoneValid() {
            return (cmbTimeZone.SelectedItem is TimeZoneInfo);
        } 

        private bool IsFormValid() {
            bool valid = false;
            if (!IsNameValid()) {
                txtName.Focus();
            } else if (!IsShortNameValid()) {
                txtShortName.Focus();
            } else if (!IsCityValid()) {
                txtCity.Focus();
            } else if (!IsCountryValid()) {
                txtCountry.Focus();
            } else if (!IsLatitudeValid()) {
                txtLatitude.Focus();
            } else if (!IsLongitudeValid()) {
                txtLongitude.Focus();
            } else if (!IsAltitudeValid()) {
                txtAltitude.Focus();
            } else if (!IsTimeZoneValid()) {
                cmbTimeZone.Focus();
            } else {
                valid = true;
            }
            return valid;
        }
        
        #endregion

        #region Validating Events

        private void txtShortName_Validating(object sender, CancelEventArgs e) {
            if (IsShortNameValid() && !string.IsNullOrWhiteSpace(txtShortName.Text)) {
                _nextShortName = txtShortName.Text.Trim();
                if (!ShortNameWorker.IsBusy) {
                    ShortNameWorker.RunWorkerAsync();
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

        #endregion

        #region Other Events
        /*
        private void txtFirstLetterUpper_TextChanged(object sender, EventArgs eventArgs) { //TODO Fancy Feature!
            TextBox txt = sender as TextBox;
            string str = txt.Text;

            if (string.IsNullOrEmpty(str)) {
                return;
            }

            txt.Text = str.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + str.Substring(1);
        }
        */
        
        

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

        #region Creating

        private void btnSaveForCreation_Click(object sender, EventArgs e) {
            if (!CreateWorker.IsBusy) {
                if (ShortNameWorker.IsBusy) {
                    ShortNameWorker.CancelAsync();
                }
                if (IsFormValid()) {
                    loadingImg.Visible = true;
                    CreateWorker.RunWorkerAsync(cmbTimeZone.SelectedItem);
                }
            } else {
                MessageBox.Show(this, @"A creation is pending, please wait for it to complete!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CreateWorker_DoWork(object sender, DoWorkEventArgs e) {
            string name = txtName.Text.Trim();
            string shortName = txtShortName.Text.Trim();
            string city = txtCity.Text.Trim();
            string country = txtCountry.Text.Trim();
            double latitude = txtLatitude.DoubleValue();
            double longitude = txtLongitude.DoubleValue();
            double altitude = txtAltitude.DoubleValue();
            TimeZoneInfo timeZone = e.Argument as TimeZoneInfo;

            //TimeZoneInfo timeZone = cmbTimeZone.SelectedItem as TimeZoneInfo;

            e.Result = ctr.CreateAirport(name, shortName, city, country, latitude, longitude, altitude, timeZone);
        }

        private void CreateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImg.Visible = false;
            if (e.Error != null) {
                Exception ex = e.Error;
                if (ex is AlreadyExistException) {
                    errProvider.SetError(txtShortName, txtShortName.Text.Trim() + " already exists!");
                    txtShortName.Focus();
                } else if (ex is TimeZoneException) {
                    errProvider.SetError(cmbTimeZone, ex.Message);
                    txtShortName.Focus();
                } else {
                    MessageBox.Show(this, ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Create Exception!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex);
                }
            } else if (e.Cancelled) {
                //Form is not valid
            }
            else {
                Airport airport = e.Result as Airport;
                if (airport != null) {
                    Airport = airport;
                    Visible = false;
                    MessageBox.Show(this, @"The airport has been created", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                } else {
                    MessageBox.Show(this, @"Unknown Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region BackgroundWorker for txtShortName

        private void ShortNameWorker_DoWork(object sender, DoWorkEventArgs e) {
            string exists = null;
            while (_nextShortName != null && !ShortNameWorker.CancellationPending) {
                string search = _nextShortName;
                List<Airport> list = ctr.GetAirportsByShortName(search, true);

                if (list != null && list.Any()) {
                    exists = search;
                } else {
                    exists = null;
                }
                //Console.WriteLine("Exits: " + search + ": " + exists);
                //Console.WriteLine("next: " + _nextShortName);
                if (search.Equals(_nextShortName)) {
                    _nextShortName = null;
                }
                //Thread.Sleep(3000);
            }
            if (ShortNameWorker.CancellationPending) {
                e.Cancel = true;
            }
            e.Result = exists;
        }

        private void ShortNameWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled && e.Result != null && !txtShortName.ContainsFocus) {
                errProvider.SetError(txtShortName, e.Result + " already exists!");
            }
        }

        #endregion


    }
}
