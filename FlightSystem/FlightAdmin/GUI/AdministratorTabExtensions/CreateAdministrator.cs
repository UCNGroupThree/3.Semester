using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.AdministratorTabExtensions
{
    public partial class CreateAdministrator : Form
    {
        public Administrator Administrator { get; set; }
        public CreateAdministrator()
        {
            InitializeComponent();
        }

        public CreateAdministrator(Administrator admin) {
            
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
