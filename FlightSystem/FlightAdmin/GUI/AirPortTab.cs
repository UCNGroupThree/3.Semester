using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class AirPortTab : UserControl {
        public AirPortTab() {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            Airport airport = new Airport {
                Name = "Test",
                ShortName = "test",
                City = "Test",
                Country = "Test",
                Latitude = 32432,
                Longtitude = 324324,
                Altitude = 432423,
                TimeZone = "1",
            };
            Console.WriteLine("ROUTES");
            Console.WriteLine(airport.Routes);
            Console.WriteLine("ROUTES");
            Console.ReadLine();
        }
    }
}
