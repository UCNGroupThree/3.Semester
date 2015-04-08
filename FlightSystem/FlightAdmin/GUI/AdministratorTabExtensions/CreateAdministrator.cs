using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.AdministratorTabExtensions
{
    public partial class CreateAdministrator : Form
    {
        public CreateAdministrator()
        {
            InitializeComponent();
        }

        #region create new admin

        public void create() {
            
        }

        #endregion

        #region button methods
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            create();
        }

        #endregion
    }
}
