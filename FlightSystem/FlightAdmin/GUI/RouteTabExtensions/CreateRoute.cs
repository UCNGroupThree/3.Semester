using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.RouteTabExtensions {
    public partial class CreateRoute : UserControl {
        public CreateRoute() {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e) {
            try {
                double.Parse(textBox1.Text);
            } catch (Exception) {
                textBox1.Text = "";
            }
        }
    }
}
