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
using FlightAdmin.Exceptions;
using FlightAdmin.GUI.RouteTabExtensions;
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
                    SearchRoutes(txtFrom.Text);
                } else {
                    SearchRoute(txtFrom.Text, txtTo.Text);
                }
            }
        }




        #region Search

        private void SearchRoute(string from, string to) {
            Route route;

            try {
                Airport fromAirport = new Airport(); //TODO Get airport by name?
                Airport toAirport = new Airport();

               route = rCtr.GetRouteByAirports(fromAirport, toAirport);
            } catch (NullException e) {
                MessageBox.Show(e.Message); //TODO Better error handeling?
                return;
            }

            //TODO Show result in datagrid
        }

        private void SearchRoutes(string from) {
            List<Route> routes;

            try {
                Airport fromAirport = new Airport(); //TODO Get airport by name?

                routes = rCtr.GetRoutesByAirport(fromAirport);
            } catch (NullException e) {
                MessageBox.Show(e.Message); //TODO Better error handeling?
                return;
            } 

            //TODO Show result in datagrid
        }

        #endregion

        private void btnCreate_Click(object sender, EventArgs e) {
            Test t = new Test();
            t.Show();
        }


    }
}
