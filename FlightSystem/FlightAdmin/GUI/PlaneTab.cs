using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI {
    public partial class PlaneTab : UserControl {
        public PlaneTab() {
            InitializeComponent();
        }

        private void grpPlaneSearch_Enter(object sender, EventArgs e)
        {

        }

        private void btnClearPlaneSearch_Click(object sender, EventArgs e) {

            txtID.Text = "";
            txtNameSearch.Text = "";
            comboPassengerCountChoice.Text = "";
            spinnerPassengerCount.Text = "0";

        }
    }
}
