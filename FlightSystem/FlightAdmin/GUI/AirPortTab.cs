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
using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI {
    public partial class AirPortTab : UserControl {
        private AirportCtr ctr = new AirportCtr();

        public AirPortTab() {
            InitializeComponent();
            SetEvents();
        }

        private void SetEvents() {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.TextChanged += FancyFeatures.TextChangedDisableParentsTextboxs;
                t.TextChanged += ChangeButtons;
            }
        }

        private void ChangeButtons(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            bool empty = txt != null && txt.TextLength != 0;
            btnClear.Enabled = empty;
            btnSearch.Enabled = empty;
            //TODO Default button
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
                    int id = -1;
                    try {
                        id = txtID.IntValue;
                    } catch (Exception) { 
                        //Empty 
                    }
                    if (id != -1) {
                        Airport airport = ctr.GetAirport(id);
                        if (airport != null) {
                            list = new List<Airport> { airport };
                        }
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

        private void btnClear_Click(object sender, EventArgs e) {
            foreach (TextBox t in tableLayoutCreate.Controls.OfType<TextBox>()) {
                t.Text = "";
            }
        }

    }
}
