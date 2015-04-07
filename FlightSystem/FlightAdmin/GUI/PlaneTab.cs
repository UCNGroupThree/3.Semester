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
            txtNameSearch.Text = "";
            spinnerPassengerCount.Text = "0";
        }


        public void SearchPlane() {

            if (txtID.Text != null && txtID.Text == "") {

                try {
                    int id = int.Parse(txtID.Text);

                    Plane foundPlane = ctr.GetPlaneByID(id);

                    planeBindingSource.Clear();
                    planeBindingSource.Add(foundPlane);
                } catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            }
            else if (txtNameSearch != null && txtNameSearch.Text == "") {
                
            }
        }

        public void CreatePlane() {

            CreatePlane createPlaneWindow = new CreatePlane();
            createPlaneWindow.ShowDialog();
        }

        #endregion

        #region right click menu methods

        public void DeleteSelectedPlane() {
            
        }

        public void EditSelectedPlane() {
            
        }

        #endregion

        private void btnClearPlaneSearch_Click(object sender, EventArgs e)
        {
            ClearPlaneSearch();
        }

        #region Show All Planes in table
        private void checkShowAllPlanes_CheckedChanged(object sender, EventArgs e)
        {   

            if (checkShowAllPlanes.CheckState == CheckState.Checked) {
                
                List<Plane> planes;
                planes = ctr.GetAllPlanes();
                planeTable.DataSource = planes;
            }

        }

        #endregion

        private void btnPlaneSearch_Click(object sender, EventArgs e)
        {
            SearchPlane(); 
        }

        private void btnCreatePlane_Click(object sender, EventArgs e)
        {
            CreatePlane();
        }

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

    }

}
