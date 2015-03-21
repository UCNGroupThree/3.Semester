using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class RouteTab : UserControl {

        private RouteCtr rCtr = new RouteCtr();

        public RouteTab() { //TODO Nick D PEdersen
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrWhiteSpace(txtFrom.Text)) {
                MessageBox.Show("You must type in the 'From' Airport.");
            } else {
                if (string.IsNullOrEmpty(txtTo.Text) || string.IsNullOrWhiteSpace(txtTo.Text)) {
                    
                }
            }
        }




        #region Search

        private void SearchRoute(string from, string to) {


            try {
                Airport fromAirport = new Airport(); //TODO Get airport by name?
                Airport toAirport = new Airport();

                rCtr.GetRoutes(fromAirport, toAirport);
            } catch (Exception e) {
                MessageBox.Show("The Airport does not exist, please try again"); //TODO Better error handeling?
            }

        }

        private void SearchRoute(string from) {
            
        }

        #endregion


    }
}
