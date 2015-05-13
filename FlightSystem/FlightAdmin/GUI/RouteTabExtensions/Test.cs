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

namespace FlightAdmin.GUI.RouteTabExtensions {
    public partial class Test : Form {

        #region Event Forwarder

        public event CreateRoute.AddRoute AddRouteEvent {
            add { createRoute1.AddRouteEvent += value; }
            remove { createRoute1.AddRouteEvent -= value; }
        }

        #endregion

        public Test() {
            createRoute1 = new CreateRoute();
            InitializeComponent();
            createRoute1.CloseEvent += Close;
        }

        public Test(Route route) {
            createRoute1 = new CreateRoute(route);
            InitializeComponent();
            createRoute1.CloseEvent += Close;
        }

        #region Closer

        private void Close() {
            this.Dispose();
        }

        #endregion
    
    }
}
