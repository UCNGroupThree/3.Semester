using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.RouteTabExtensions {
    public partial class Test : Form {

        #region Event Forwarder

        public event CreateRoute.AddRoute AddRouteEvent {
            add { createRoute1.AddRouteEvent += value; }
            remove { createRoute1.AddRouteEvent -= value; }
        }

        #endregion

        public Test() {
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
