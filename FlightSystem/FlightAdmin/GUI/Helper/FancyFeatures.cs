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

        public static bool IsTextBoxValid(TextBox txt, ErrorProvider errProvider, int minLength, int maxLength) {
            if (txt == null || errProvider == null) {
                throw new NullReferenceException("txt and errProvider can't be null!");
            }
            if (minLength > 0 && maxLength > 0 && maxLength < minLength) {
                throw new ArgumentException("maxLength can't be bigger than minLength!");
            }
            int length = txt.Text.Trim().Length;
            string errText = "";
            if (length == 0 && minLength > 0) {
                errText = String.Format("{0} is requested!", txt.Name);
            } else if (length < minLength && minLength > 0) {
                errText = String.Format("The length of the {0} is to short, minimum is {1} chars!", txt.Name, minLength);
            } else if (length > maxLength && maxLength > 0) {
                errText = String.Format("The length of the {0} is to long, maximum is {1} chars!", txt.Name, maxLength);
            }
            errProvider.SetError(txt, errText);

            return String.IsNullOrEmpty(errText);
        }
    }
}
