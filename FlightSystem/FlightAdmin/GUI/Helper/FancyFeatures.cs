﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.Helper {
    class FancyFeatures {

        public static void TextChangedDisableParentsInputControls(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            if (txt != null) {
                List<Control> list = GetParentsControls(txt);
                foreach (var c in list) {
                    if (!txt.Equals(c) && txt.Text.Trim().Length > 0) {
                        c.Enabled = false;
                    }
                    else {
                        c.Enabled = true;
                    }
                }
            }
        }

        public static void CheckedChangedDisableParentsInputControls(object sender, EventArgs e) {
            CheckBox chb = sender as CheckBox;
            if (chb != null) {
                List<Control> list = GetParentsControls(chb);
                foreach (var c in list) {
                    if (!chb.Equals(c) && chb.Checked) {
                        c.Enabled = false;
                    } else {
                        c.Enabled = true;
                    }
                }
            }
        }

        private static List<Control> GetParentsControls(Control control) {
            var list = new List<Control>();
            list.AddRange(control.Parent.Controls.OfType<TextBox>());
            list.AddRange(control.Parent.Controls.OfType<CheckBox>());
            return list;
        }
        
        public static bool IsTextBoxValid(TextBox txt, ErrorProvider errProvider, string objText, int minLength, int maxLength) {
            if (txt == null || errProvider == null) {
                throw new NullReferenceException("txt and errProvider can't be null!");
            }

            if (minLength > 0 && maxLength > 0 && maxLength < minLength) {
                throw new ArgumentException("maxLength can't be bigger than minLength!");
            }
            int length = txt.Text.Trim().Length;
            string errText = "";
            if (length == 0 && minLength > 0) {
                errText = string.Format("{0} is requested!", objText);
            } else if (length < minLength && minLength > 0) {
                errText = string.Format("The length of the {0} is to short, minimum is {1} chars!", objText, minLength);
            } else if (length > maxLength && maxLength > 0) {
                errText = string.Format("The length of the {0} is to long, maximum is {1} chars!", objText, maxLength);
            }
            errProvider.SetError(txt, errText);

            return String.IsNullOrEmpty(errText);
        }

        public static bool IsTextBoxDoubleValid(TextBox txt, ErrorProvider errProvider, string objText, int minValue, int maxValue, bool emptyAllowed) {
            string errText = "";
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (!emptyAllowed || (emptyAllowed && txt.TextLength > 0)) {
                try {
                    double value = double.Parse(txt.Text);
                    if (value >= minValue && value <= maxValue) {
                        //Just continue
                    } else if (value < minValue) {
                        errText = string.Format("The {0} is too big, minimum value is {1}", objText, minValue);
                    } else if (value > maxValue) {
                        errText = string.Format("The {0} is too big, maximum value is {1}", objText, maxValue);
                    }
                } catch (Exception ex) {
                    if (ex is FormatException || ex is OverflowException) {
                        errText = string.Format("The format of {0} isn't valid!", objText);
                    }
                }
                errProvider.SetError(txt, errText);
            }
            
            return String.IsNullOrEmpty(errText);
        }

        public static void ShowErrorDialog(IWin32Window parent, string text) {
            MessageBox.Show(parent, text, @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Check if a Control object is empty or not selected.
        /// </summary>
        /// <param name="sender">Only checks TextBox and CheckBox objects</param>
        /// <returns>true if textbox is empty, or CheckBox is not checked</returns>
        public static bool IsSenderEmpty(object sender) {
            TextBox txt = sender as TextBox;
            CheckBox chb = sender as CheckBox;
            bool empty = txt != null && txt.TextLength == 0;
            if (chb != null) {
                empty = !chb.Checked;
            }
            return empty;
        }
    }
}
