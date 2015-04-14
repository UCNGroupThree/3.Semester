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
        }
        #endregion

        #region show create plane dialog
        public void CreatePlane() {

            CreatePlane createPlaneWindow = new CreatePlane();
            createPlaneWindow.ShowDialog();
        }
        #endregion

        #region datagrid methods

        private void UpdateDataGrid(List<Plane> list)
        {
            if (list != null)
            {
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
        public void SearchPlane()
        {

            int id = txtID.IntValue;

            if (id > 0) {

                List<Plane> list = new List<Plane>();
                Plane p = ctr.GetPlaneByID(id);

                if (p != null) {
                    list.Add(p);
                    UpdateDataGrid(list);
                } else {
                    
                    MessageBox.Show("No planes with id #:{0} was found", id.ToString());
                }
            }
        }
        #endregion

        #region show all planes
        public void ShowAllPlanes() {

            List<Plane> list = ctr.GetAllPlanes();
            UpdateDataGrid(list);
        }
        #endregion

        #region delete plane
        public void DeleteSelectedPlane() {

            Plane p = getSelected();

            String deletemessage = "Do you want to delete plane" + p.Name + " with id #" + p.ID + ".";
            if (MessageBox.Show(deletemessage, 
                "Deleting Plane", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ctr.DeletePlane(p);
                planeBindingSource.Remove(p);
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

        private void btnShowAllPlanes_Click(object sender, EventArgs e) {
            ShowAllPlanes();
        }

        private void btnClearPlaneSearch_Click_1(object sender, EventArgs e)
        {
            ClearPlaneSearch();
        }
        #endregion

    }

}
