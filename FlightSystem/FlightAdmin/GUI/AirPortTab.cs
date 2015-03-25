using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.Exceptions;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI {
    public partial class AirPortTab : UserControl {
        private AirportCtr ctr = new AirportCtr();

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

        private void btnSearch_Click(object sender, EventArgs e) {
            try {
                List<Airport> list = null;
                if (!String.IsNullOrWhiteSpace(txtID.Text)) {
                    Airport airport = ctr.GetAirport(int.Parse(txtID.Text.Trim())); //TODO Error handling
                    if (airport != null) {
                        list = new List<Airport> {airport};
                    }
                } 
                else if (!String.IsNullOrWhiteSpace(txtShortName.Text)) {
                    list = ctr.GetAirportsByShortName(txtShortName.Text.Trim());
                }
                else if (!String.IsNullOrWhiteSpace(txtName.Text)) {
                    list = ctr.GetAirportsByName(txtName.Text.Trim());
                }
                else if (!String.IsNullOrWhiteSpace(txtCity.Text)) {
                    list = ctr.GetAirportsByCity(txtCity.Text.Trim());
                }
                else if (!String.IsNullOrWhiteSpace(txtCountry.Text)) {
                    list = ctr.GetAirportsByCountry(txtCountry.Text.Trim());
                }
                if (list != null && list.Count > 0) {
                    UpdateDataGrid(list);
                } else {
                    MessageBox.Show(this, @"No airports found", @"Sorry");
                }
            } catch (ConnectionException ex) {
                MessageBox.Show(this, ex.Message, @"ERROR");
            }
            
        }

        private void UpdateDataGrid(List<Airport> list) {
            if (list != null) {
                airportBindingSource.Clear();
                foreach (var a in list) {
                    airportBindingSource.Add(a);
                }
            }
        }
    }
}
