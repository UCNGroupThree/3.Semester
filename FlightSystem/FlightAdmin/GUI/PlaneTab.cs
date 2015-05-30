using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.GUI.PlaneTabExtensions;
using FlightAdmin.MainService;
// Exceptions
using Common.Exceptions;
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI {
    public partial class PlaneTab : UserControl {
        
        private readonly PlaneCtr ctr = new PlaneCtr();

        public PlaneTab() {
            InitializeComponent();
        }

        #region clear plane search
        public void ClearPlaneSearch() {

            txtID.Text = "";
            txtName.Text = "";

            comboPassengerCountChoice.SelectedIndex = -1;
            spinnerPassengerCount.Text = "0";

            chkShowAllPlanes.Checked = false;
            spinnerPassengerCount.Enabled = false;

            // enable compontents
            txtID.Enabled = true;
            txtName.Enabled = true;
            comboPassengerCountChoice.Enabled = true;
            spinnerPassengerCount.Enabled = false;

            // disable buttons
            btnClearPlaneSearch.Enabled = false;
            btnPlaneSearch.Enabled = false;

        }
        #endregion

        #region show create plane dialog
        public void CreatePlane() {

            CreatePlane createPlaneWindow = new CreatePlane();
            createPlaneWindow.ShowDialog();
            Plane plane = createPlaneWindow.plane;
            if (plane != null) {
                planeBindingSource.Add(plane);
            }

            createPlaneWindow.Dispose();
        }
        #endregion

        #region datagrid methods

        private void UpdateDataGrid(List<Plane> list)
        {
            if (list != null) {
                planeBindingSource.Clear();
                foreach (var p in list) {
                    planeBindingSource.Add(p);
                }
            }
        }

        public Plane getSelected() {

            Plane p = null;
            var index = planeTable.CurrentRow;

            if (index != null)
            {
                p = index.DataBoundItem as Plane;
            }

            return p;

        }
        #endregion

        #region search plane
        public void SearchPlane() {
            loadingImage.Visible = true;
            // DisableSearch(false);
            planeBackgroundworker.RunWorkerAsync();

            // disable buttons
            btnClearPlaneSearch.Enabled = false;
            btnPlaneSearch.Enabled = false;
        }
        #endregion

        #region show all planes

        /*
        public void ShowAllPlanes() {

            List<Plane> list = ctr.GetAllPlanes();
            UpdateDataGrid(list);
        } 
        */
        #endregion

        #region delete plane
        public void DeleteSelectedPlane() {

            Plane selectedPlane = getSelected();
            if (selectedPlane != null)
            {
                var text = string.Format("Are you sure you will delete {0}?", selectedPlane);
                var confirm = MessageBox.Show(this, text, @"Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        ctr.DeletePlane(selectedPlane);
                        var delText = string.Format("{0} is deleted!", selectedPlane);
                        planeBindingSource.Remove(selectedPlane);
                        MessageBox.Show(this, delText, @"Deleted!");
                    }
                    catch (NullException)
                    {
                        FancyFeatures.ShowErrorDialog(this, "Unknown Exception - NullExcetion");
                    }
                    catch (DatabaseException ex)
                    {
                        FancyFeatures.ShowErrorDialog(this, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        FancyFeatures.ShowErrorDialog(this, "Unknown Exception\r\n" + ex);
                    }

                }
            }
        }
        #endregion

        #region edit plane

        private void EditSelectedPlane() {
            Plane selectedPlane = getSelected();
            if (selectedPlane != null) {

                CreatePlane dialog = new CreatePlane(selectedPlane);
                dialog.ShowDialog(this);
                planeTable.Refresh();

                dialog.Dispose();
            }
        }

        #endregion

        #region right click menu methods
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePlane();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedPlane();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedPlane();
        }

        #endregion

        #region button methods
        private void btnPlaneSearch_Click_1(object sender, EventArgs e)
        {   
            SearchPlane();
        }

        private void btnCreatePlane_Click_1(object sender, EventArgs e) {
            CreatePlane();
        }
   

        private void btnClearPlaneSearch_Click_1(object sender, EventArgs e)
        {
            ClearPlaneSearch();
        }
        #endregion

        private void planeTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {   
                DeleteSelectedPlane();
            }
            else if (e.KeyCode == Keys.Space) {
                EditSelectedPlane();
            }
        }

        private void planeTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (planeTable.SelectedRows.Count != 1) {
                //e.cancel = true;
            }
                
        }

        #region backgroundworker Events
        private void planeBackgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtID.Text)) {
                int id = -1;

                try {

                    id = txtID.IntValue;
                } catch (Exception) {}

                if (id != -1) {

                    Plane p = ctr.GetPlaneByID(id);

                    if (p != null) {

                        e.Result = new List<Plane> {p};
                    }
                }
            } else if (!String.IsNullOrWhiteSpace(txtName.Text)) {

                e.Result = ctr.GetPlaneByName(txtName.Text.Trim());
            }
            // passenger count 
            /*
            else if (!comboPassengerCountChoice.SelectedItem.Equals(-1)) {
                int seats = Convert.ToInt32(spinnerPassengerCount.Text.Trim());

                // Same 
                if (comboPassengerCountChoice.SelectedIndex.Equals(0)) {
                    e.Result = ctr.GetPlanesWithSeatNumber(seats);
                }
                // less and same
                else if (comboPassengerCountChoice.SelectedIndex.Equals(1)) {
                    e.Result = ctr.GetPlanesWithLessOrEqualSeatNumber(seats);
                }
                // more and same
                else if (comboPassengerCountChoice.SelectedIndex.Equals(2)) {
                    e.Result = ctr.GetPlanesWithMoreOrEqualSeatNumber(seats);
                } else {
                    MessageBox.Show("An error occured when searching with combobox", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            } */
            else if (chkShowAllPlanes.Checked) {
                e.Result = ctr.GetAllPlanes();
            }
        }
       

        private void planeBackgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImage.Visible = false;
            //DisableSearch(true);

            if (e.Error != null) {

                MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {

                List<Plane> list = e.Result as List<Plane>;

                if (list != null && list.Count > 0) {

                    UpdateDataGrid(list);
                } else {

                    MessageBox.Show(this, @"No planes was found!", @"Sorry", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region textchanged / misc events 
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtID.Text.Trim())) {
                txtName.Enabled = true;
                comboPassengerCountChoice.SelectedIndex = -1;
                comboPassengerCountChoice.Enabled = true;

                btnClearPlaneSearch.Enabled = false;
                btnPlaneSearch.Enabled = false;

            } else {
                txtName.Enabled = false;
                comboPassengerCountChoice.Enabled = false;

                btnClearPlaneSearch.Enabled = true;
                btnPlaneSearch.Enabled = true;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text.Trim()))
            {
                txtID.Enabled = true;
                comboPassengerCountChoice.SelectedIndex = -1;
                comboPassengerCountChoice.Enabled = true;

                btnClearPlaneSearch.Enabled = false;
                btnPlaneSearch.Enabled = false;


            }
            else
            {
                txtID.Enabled = false;
                comboPassengerCountChoice.Enabled = false;

                btnClearPlaneSearch.Enabled = true;
                btnPlaneSearch.Enabled = true;
            }
        }

        private void comboPassengerCountChoice_SelectedIndexChanged(object sender, EventArgs e) {
            txtID.Enabled = false;
            txtName.Enabled = false;

            btnClearPlaneSearch.Enabled = true;
            btnPlaneSearch.Enabled = true;

            spinnerPassengerCount.Enabled = true;
        }

        private void chkShowAllPlanes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowAllPlanes.Checked) {

                txtID.Enabled = false;
                txtID.Text = "";
                txtName.Enabled = false;
                txtName.Text = "";

                comboPassengerCountChoice.Enabled = false;
                comboPassengerCountChoice.SelectedIndex = -1;

                spinnerPassengerCount.Enabled = false;
                spinnerPassengerCount.Text = "0";

                // enable buttons
                btnClearPlaneSearch.Enabled = true;
                btnPlaneSearch.Enabled = true;

            } else {

                txtID.Enabled = true;
                txtName.Enabled = true;

                comboPassengerCountChoice.Enabled = true;
                spinnerPassengerCount.Enabled = true;

                // disable buttons
                btnClearPlaneSearch.Enabled = false;
                btnPlaneSearch.Enabled = false;
            }

        }
        #endregion

    }


}
