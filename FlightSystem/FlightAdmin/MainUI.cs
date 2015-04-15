using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FlightAdmin.GUI;

namespace FlightAdmin {
    public partial class MainUI : Form {
        public MainUI() {
            InitializeComponent();
        }

        #region menu events - program menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            exitProgram();
        }

        private void exitProgram() {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }
        #endregion

        #region menu events - help menu
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e) {
            help();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            about();
        }



        private void help() {
            MessageBox.Show("Help file?", @"Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void about() {
            AboutBox dialog = new AboutBox();
            dialog.Show();
        }

        #endregion

        
    }
}
