﻿using System;
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
        public Test() {
            InitializeComponent();
            createRoute1.CloseReady += Close;
        }

        private void Close() {
            this.Dispose();
        }
    }
}