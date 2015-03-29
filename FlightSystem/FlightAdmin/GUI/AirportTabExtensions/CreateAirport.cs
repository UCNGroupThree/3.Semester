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
            //btnCreate.Validating += txtShortName_Validating;
        }

        private void button1_Click(object sender, EventArgs e) {
            //Console.WriteLine("validating: " + FancyFeatures.IsTextBoxValid(txtShortName, errorProvider1, 1, 1));
            //errorProvider1.SetError(txtShortName, "Test");
        }

        private void txtShortName_Validating(object sender, CancelEventArgs e) {
            Console.WriteLine("validating: " + txtShortName.CausesValidation);
            TextBox txt = sender as TextBox;
            //Button btn = sender as Button;

            if (txt != null) {
                
                //errorProvider1.SetError(txtShortName, "Length is to short!");
                //} else if (btn != null) {
                //e.Cancel = true;
                //    errorProvider1.SetError(txtCity, "Test3");
            }
            Console.WriteLine("validating: " + FancyFeatures.IsTextBoxValid(txt, errorProvider1, 0, 0));
        }


        private void txtRemoveErrorOn_TextChanged(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            if (txt != null) {
                errorProvider1.SetError(txt, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
