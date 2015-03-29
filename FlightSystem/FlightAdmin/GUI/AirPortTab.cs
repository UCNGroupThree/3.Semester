using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.Exceptions;
using FlightAdmin.GUI.AirportTabExtensions;
using FlightAdmin.MainService;
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI {
    public partial class AirPortTab : UserControl {
        private readonly AirportCtr ctr = new AirportCtr();

        public AirPortTab() {
            InitializeComponent();
            SetEvents();

        }

        private void SetEvents() {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.TextChanged += FancyFeatures.TextChangedDisableParentsTextboxs;
                t.TextChanged += ChangeButtons;
            }
        }

        private void SetDefaultButton(IButtonControl btnAccept, IButtonControl btnCancel) {
            var f = FindForm();
            if (f != null) {
                f.AcceptButton = btnAccept;
                f.CancelButton = btnCancel;
            }
        }

        private void UpdateDataGrid(List<Airport> list) {
            if (list != null) {
                airportBindingSource.Clear();
                foreach (var a in list) {
                    airportBindingSource.Add(a);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            CreateAirport dialog = new CreateAirport();
            dialog.ShowDialog(this);
        }

        #region Search

        private void DisableSearch(bool enable) {
            foreach (var txt in tableLayoutCreate.Controls.OfType<TextBox>()) {
                txt.Enabled = enable;
                if (enable && txt.TextLength > 0) {
                    txt.Focus();
                }
            }
            btnClear.Enabled = enable;
            btnSearch.Enabled = enable;
        }
        
        private void ChangeButtons(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            bool empty = txt != null && txt.TextLength == 0;
            btnClear.Enabled = !empty;
            btnSearch.Enabled = !empty;
            if (!empty) {
                SetDefaultButton(btnSearch, btnClear);
            } else {
                SetDefaultButton(btnCreate, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            //backgroundWorker1.DoWork += Search();
            //Thread t = new Thread(new ThreadStart(Search));
            //t.Start();
            loadingImg.Visible = true;
            DisableSearch(false);
            backgroundWorker1.RunWorkerAsync();

        }

        private void btnClear_Click(object sender, EventArgs e) {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.Text = "";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            if (!String.IsNullOrWhiteSpace(txtID.Text)) {
                int id = -1;
                try {
                    id = txtID.IntValue;
                } catch (Exception) {
                    //Empty 
                }
                if (id != -1) {
                    Airport airport = ctr.GetAirport(id);
                    if (airport != null) {
                        e.Result = new List<Airport> { airport };
                    }
                }
            } else if (!String.IsNullOrWhiteSpace(txtShortName.Text)) {
                e.Result = ctr.GetAirportsByShortName(txtShortName.Text.Trim());
            } else if (!String.IsNullOrWhiteSpace(txtName.Text)) {
                e.Result = ctr.GetAirportsByName(txtName.Text.Trim());
            } else if (!String.IsNullOrWhiteSpace(txtCity.Text)) {
                e.Result = ctr.GetAirportsByCity(txtCity.Text.Trim());
            } else if (!String.IsNullOrWhiteSpace(txtCountry.Text)) {
                e.Result = ctr.GetAirportsByCountry(txtCountry.Text.Trim());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImg.Visible = false;
            DisableSearch(true);
            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                List<Airport> list = e.Result as List<Airport>;
                if (list != null && list.Count > 0) {
                    UpdateDataGrid(list);
                } else {
                    MessageBox.Show(this, @"No airports found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion
        
    }
}
