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
        public PlaneTab() {
            InitializeComponent();
        }

        private void grpPlaneSearch_Enter(object sender, EventArgs e)
        {

        }

        
        private void btnCreatePlane_Click(object sender, EventArgs e)
        {
            // open dialog for create new plane'
            CreatePlane createPlaneWindow = new CreatePlane();
            createPlaneWindow.ShowDialog();
        }

        private void btnClearPlaneSearch_Click(object sender, EventArgs e, object clearPlaneSearch) {

            //clearPlaneSearch();
        }


        private void btnPlaneSearch_Click(object sender, EventArgs e) {

            string searchPlaneID = txtID.Text;
            string searchPlaneName = txtNameSearch.Text;
            if (searchPlaneName == null) throw new ArgumentNullException("searchPlaneName");

            //planeSearch(searchPlaneID, searchPlaneName);
        }


        private void clearPlaneSearch() {

            txtID.Text = "";
            txtNameSearch.Text = "";
            comboPassengerCountChoice.Text = "";
            spinnerPassengerCount.Text = "0";
        }

        private void updateDataGrid(List<Plane> list)
        {

            if (list != null)
            {
                planeBindingSource.Clear();

                foreach (var a in list)
                {
                    planeBindingSource.Add(a);
                }
            }
        }

        private void searchPlane(int id, string name) {
             
            List<Plane> foundPlanes = new List<Plane>();

            PlaneCtr ctr = new PlaneCtr();

            foundPlanes = ctr.GetAllPlanes();

            //if (foundPlanes != null) UpdateDataGrid(foundPlanes);
        }

       
    }
}
