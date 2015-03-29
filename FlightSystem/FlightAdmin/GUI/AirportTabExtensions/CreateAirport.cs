using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.AirportTabExtensions {
    public partial class CreateAirport : Form {
        public CreateAirport() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            errorProvider1.SetError(txtShortName, "Test");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
