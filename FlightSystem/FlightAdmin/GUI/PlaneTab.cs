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
        #region search methods

        public void ClearPlaneSearch() {

            txtID.Text = "";
            txtName.Text = "";
            spinnerPassengerCount.Text = "0";
        }


        


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

        public void ShowAllPlanes() {

            List<Plane> list = ctr.GetAllPlanes();
            UpdateDataGrid(list);
        }

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

        public void EditSelectedPlane() {

            Plane p = getSelected();

            if (p != null) {

                MessageBox.Show("Plane:" + p.Name.ToString() + ".");
            }

        }


        public Plane getSelected() {

            Plane p = null;
            var index = planeTable.CurrentRow;

            if (index != null) {
                p = index.DataBoundItem as Plane;
            }

            return p;
        }

        #endregion

        #region button methods
        private void btnClearPlaneSearch_Click(object sender, EventArgs e)
        {
            ClearPlaneSearch();
        }



        private void btnPlaneSearch_Click(object sender, EventArgs e)
        {
            SearchPlane(); 
        }

        private void btnCreatePlane_Click(object sender, EventArgs e)
        {
            CreatePlane();
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
    }

}
