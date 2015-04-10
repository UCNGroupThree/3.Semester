using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.AirportTabExtensions;
using FlightAdmin.MainService;
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI {
    public partial class AirPortTab : UserControl {
        private readonly AirportCtr ctr = new AirportCtr();

        public AirPortTab() {
            InitializeComponent();
            SetEvents();
            timeZoneDataGridViewTextBoxColumn.DataPropertyName = "TimeZone";
            //UpdateDataGrid(ctr.GetAirportsByCountry("denmark"));
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
            var airport = dialog.Airport;
            if (airport != null) {
                airportBindingSource.Add(airport);
            }
            dialog.Dispose();
        }

        #region Search

        private void DisableSearch(bool enable) {
            foreach (var txt in tableLayoutCreate.Controls.OfType<TextBox>().Where(t => t.TextLength > 0)) {
                txt.Enabled = enable;
                txt.Focus();
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
            //bgWorker.DoWork += Search();
            //Thread t = new Thread(new ThreadStart(Search));
            //t.Start();
            loadingImg.Visible = true;
            DisableSearch(false);
            bgWorker.RunWorkerAsync();

        }

        private void btnClear_Click(object sender, EventArgs e) {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.Text = "";
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e) {
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
                e.Result = ctr.GetAirportsByShortName(txtShortName.Text.Trim(), false);
            } else if (!String.IsNullOrWhiteSpace(txtName.Text)) {
                e.Result = ctr.GetAirportsByName(txtName.Text.Trim());
            } else if (!String.IsNullOrWhiteSpace(txtCity.Text)) {
                e.Result = ctr.GetAirportsByCity(txtCity.Text.Trim());
            } else if (!String.IsNullOrWhiteSpace(txtCountry.Text)) {
                e.Result = ctr.GetAirportsByCountry(txtCountry.Text.Trim());
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
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

        private Airport GetSelected() {
            Airport air = null;
            var current = dataGrid.CurrentRow;
            if (current != null) {
                air = current.DataBoundItem as Airport;
            }
            return air;
        }

        private void DeleteSelected() {
            Airport selectedAirport = GetSelected();
            if (selectedAirport != null) {
                var text = string.Format("Are you sure you will delete {0} #{1}?", selectedAirport, selectedAirport.ID);
                var confirm = MessageBox.Show(this, text, @"Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes) {
                    try {
                        ctr.DeleteAirport(selectedAirport);
                        var delText = string.Format("{0} #{1} is deleted!", selectedAirport, selectedAirport.ID);
                        airportBindingSource.Remove(selectedAirport);
                        MessageBox.Show(this, delText, @"Deleted!");
                    } catch (NullException) {
                        FancyFeatures.ShowErrorDialog(this, "Unknown Exception - NullExcetion");
                    } catch (DatabaseException ex) {
                        FancyFeatures.ShowErrorDialog(this, ex.Message);
                    } catch (Exception ex) {
                        FancyFeatures.ShowErrorDialog(this, "Unknown Exception\r\n" + ex);
                    }
                    
                }
            }
        }
        
        private void EditSelected() {
            Airport selectedAirport = GetSelected();
            if (selectedAirport != null) {
                
                CreateAirport dialog = new CreateAirport(selectedAirport);
                dialog.ShowDialog(this);
                dataGrid.Refresh();

                dialog.Dispose();
            }
            //MessageBox.Show("Edit Selected: " + selectedAirport);
        }

        #region Datagrid Events

        private void deleteMenuItem_Click(object sender, EventArgs e) {
            DeleteSelected();
        }

        private void editMenuItem_Click(object sender, EventArgs e) {
            EditSelected();
        }

        private void dataGridMenu_Opening(object sender, CancelEventArgs e) {
            //Only show menu, if datagrid has selected rows.
            if (dataGrid.SelectedRows.Count != 1) {
                e.Cancel = true;
            }
        }
        
        private void dataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                var hti = dataGrid.HitTest(e.X, e.Y);
                dataGrid.ClearSelection();
                var rowIndex = e.RowIndex;
                if (rowIndex != -1) {
                    dataGrid.Rows[rowIndex].Selected = true;
                    //Set arrow to selected column
                    dataGrid.CurrentCell = dataGrid.Rows[rowIndex].Cells[0]; 
                }
            }
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {
                DeleteSelected();
            } else if (e.KeyCode == Keys.Space) {
                EditSelected();
            }
        }

        #endregion
    }
}
