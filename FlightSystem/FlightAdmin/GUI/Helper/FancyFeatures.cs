using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.Helper {
    class FancyFeatures {

        public static void TextChangedDisableParentsTextboxs(object sender, EventArgs e) {
            TextBox txtChanged = sender as TextBox;
            if (txtChanged != null) {
                foreach (var c in ((TextBox)sender).Parent.Controls.OfType<TextBox>()) {
                    if (!txtChanged.Equals(c) && txtChanged.Text.Trim().Length > 0) {
                        c.Enabled = false;
                    }
                    else {
                        c.Enabled = true;
                    }
                }
            }
        }
    }
}
