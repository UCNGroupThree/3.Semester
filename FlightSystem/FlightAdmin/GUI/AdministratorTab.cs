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
using FlightAdmin.GUI.AdministratorTabExtensions;
using FlightAdmin.GUI.AirportTabExtensions;
using FlightAdmin.MainService;
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI {
    public partial class AdministratorTab : UserControl {
        private readonly AdministratorCtr ctr = new AdministratorCtr();

        public AdministratorTab() {
            InitializeComponent();
            SetEvents();
        }

        private void SetEvents() {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.TextChanged += FancyFeatures.TextChangedDisableParentsInputControls;
                t.TextChanged += ChangeButtons;
            }
            chbShowAll.CheckedChanged += FancyFeatures.CheckedChangedDisableParentsInputControls;
            chbShowAll.CheckedChanged += ChangeButtons;
        }

        
        private void SetDefaultButton(IButtonControl btnAccept, IButtonControl btnCancel) {
            var f = FindForm();
            if (f != null) {
                f.AcceptButton = btnAccept;
                f.CancelButton = btnCancel;
            }
        }

        private void UpdateDataGrid(List<Administrator> list) {
            if (list != null) {
                adminBindingSource.Clear();
                foreach (var a in list) {
                    adminBindingSource.Add(a);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            CreateAdministrator dialog = new CreateAdministrator();
            dialog.ShowDialog(this);
            Administrator administrator = dialog.Administrator;
            if (administrator != null) {
                adminBindingSource.Add(administrator);
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
            bool empty = FancyFeatures.IsSenderEmpty(sender);
            btnClear.Enabled = !empty;
            btnSearch.Enabled = !empty;
            if (!empty) {
                SetDefaultButton(btnSearch, btnClear);
            } else {
                SetDefaultButton(btnCreate, null);
            }
        }
        
        private void btnSearch_Click(object sender, EventArgs e) {
            loadingImg.Visible = true;
            DisableSearch(false);
            bgWorker.RunWorkerAsync();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.Text = "";
            }
            chbShowAll.Checked = false;
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
                    Administrator admin = ctr.GetAdministrator(id);
                    if (admin != null) {
                        e.Result = new List<Administrator> { admin };
                    }
                }
            } else if (!String.IsNullOrWhiteSpace(txtUsername.Text)) {
                e.Result = ctr.GetAdministratorsByUsername(txtUsername.Text.Trim(), false);
            } else if (chbShowAll.Checked) {
                e.Result = ctr.GetAllAdministrators();
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImg.Visible = false;
            DisableSearch(true);
            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                List<Administrator> list = e.Result as List<Administrator>;
                if (list != null && list.Count > 0) {
                    UpdateDataGrid(list);
                } else {
                    MessageBox.Show(this, @"No administrators found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region Selection in Datagrid (Delete / Edit / Change password)

        private Administrator GetSelected() {
            Administrator admin = null;
            var current = dataGrid.CurrentRow;
            if (current != null) {
                admin = current.DataBoundItem as Administrator;
            }
            return admin;
        }

        private void DeleteSelected() {
            Administrator selectedAdmin = GetSelected();
            if (selectedAdmin != null) {
                var text = string.Format("Are you sure you will delete {0}?", selectedAdmin);
                var confirm = MessageBox.Show(this, text, @"Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes) {
                    try {
                        ctr.DeleteAdministrator(selectedAdmin);
                        var delText = string.Format("{0} is deleted!", selectedAdmin);
                        adminBindingSource.Remove(selectedAdmin);
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
            Administrator selectedAdmin = GetSelected();
            if (selectedAdmin != null) {

                CreateAdministrator dialog = new CreateAdministrator(selectedAdmin);
                dialog.ShowDialog(this);
                dataGrid.Refresh();

                dialog.Dispose();
            }
            //MessageBox.Show("Edit Selected: " + selectedAirport);
        }

        #endregion

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
