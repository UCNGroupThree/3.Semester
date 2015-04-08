using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI {
    public partial class AdministratorTab : UserControl {
        public AdministratorTab() {
            InitializeComponent();
        }

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

        #endregion
    }
}
