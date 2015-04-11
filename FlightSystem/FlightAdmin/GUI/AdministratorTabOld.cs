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
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class AdministratorTabOld : UserControl {

        private AdministratorCtr ctr;

        public AdministratorTabOld() {
            InitializeComponent();
        }

        #region datagridview update 

        public void updateDataGridView(List<Administrator> list) {

            foreach (var admin in list) {

                administratorBindingSource.Add(admin);
            }
        } 

        #endregion

        #region clear

        public void clear() {
            
            // clear field
            txtName.Text = "";
        }

        #endregion

        #region search
        public void search() {
            

        }

        #endregion

        #region create

        public void create() {
            
            // create method
        }

        #endregion
        
        #region show all admins

        public void showAll() {
            
            
        }

        #endregion

        #region button methods
        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            create();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showAll();
        }

        #endregion
    }
}
