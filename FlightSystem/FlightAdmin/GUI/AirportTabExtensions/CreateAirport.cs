using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI.AirportTabExtensions {
    public partial class CreateAirport : Form {
        public CreateAirport() {
            InitializeComponent();
            
        }


        #region Validating methods

        private bool IsShortNameValid() {
            bool valid = FancyFeatures.IsTextBoxValid(txtShortName, errorProvider1, lblShortName.Text, 3, 3);
            //TODO TJEK OM DET ALLEREDE EKSISTERER
            return valid;
        }

        private bool IsNameValid() {
            return FancyFeatures.IsTextBoxValid(txtName, errorProvider1, lblName.Text, 3, 60);
        }

        private bool IsCityValid() {
            return FancyFeatures.IsTextBoxValid(txtCity, errorProvider1, lblCity.Text, 3, 60);
        }

        private bool IsCountryValid() {
            return FancyFeatures.IsTextBoxValid(txtCountry, errorProvider1, lblCountry.Text, 3, 60);
        }

        private bool IsLatitudeValid() {
            //TODO
            return true;
        }

        private bool IsLongtitudeValid() {
            //TODO
            return true;
        }

        private bool IsAltitudeValid() {
            //TODO
            return true;
        }

        private bool IsTimeZoneValid() {
            //TODO
            return true;
        }

        #endregion

        #region Validating Events
        
        private void txtShortName_Validating(object sender, CancelEventArgs e) {
            IsShortNameValid();
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

        private void txtLongtitude_Validating(object sender, CancelEventArgs e) {
            IsLongtitudeValid();
        }

        private void txtAltitude_Validating(object sender, CancelEventArgs e) {
            IsAltitudeValid();
        }

        private void txtTimeZone_Validating(object sender, CancelEventArgs e) {
            IsTimeZoneValid();
        }
        
        #endregion

        #region Other Events

        private void button1_Click(object sender, EventArgs e) {
            //Console.WriteLine("validating: " + FancyFeatures.IsTextBoxValid(txtShortName, errorProvider1, 1, 1));
            //errorProvider1.SetError(txtShortName, "Test");
        }

        private void txtRemoveErrorOn_TextChanged(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            if (txt != null) {
                errorProvider1.SetError(txt, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Dispose();
        }

        #endregion

    }
}
