using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.MainService;
using FlightAdmin.Properties;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public partial class CreateFlights : Form {

        public Route Route { get; set; }
        private readonly bool _isEdit;

        private int _y = 15;
        private const int I = 25;
        private List<Plane> _planes;
        private readonly List<FlightHelper> _flights;

        public CreateFlights() {
            InitializeComponent();
            //y = dateTimePicker1.Location.Y;
            _flights = new List<FlightHelper>();
            _isEdit = false;
        }

        public CreateFlights(Route route) {
            Route = route;
            if (route == null) throw new ArgumentNullException();

            InitializeComponent();
            _flights = new List<FlightHelper>();
            _isEdit = true;
        }

        #region AddFlights

        private void AddFlight(Flight flight) {
            DateTime dep = flight.DepartureTime;
            DateTime arr = flight.ArrivalTime;
            Plane plane = flight.Plane;
            try {
                var dt1 = new DateTimePicker {
                    Location = new Point(1, _y),
                    Name = "dateTimePicker1",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = Resources.CreateFlight_DateTimeFormat,
                    //Value = arr
                    Value = dep
                };
                dt1.Enter += (dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt1);

                var dt2 = new DateTimePicker {
                    Location = new Point(148, _y),
                    Name = "dateTimePicker2",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = Resources.CreateFlight_DateTimeFormat,
                    //Value = dep
                    Value = arr
                };
                dt2.Enter += (dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt2);

                var cmb = new ComboBox {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FormattingEnabled = true,
                    Location = new Point(294, _y),
                    Name = "comboBox1",
                    Size = new Size(106, 21)
                };
                selectionPanel.Controls.Add(cmb);

                cmb.DataSource = _planes.ToList();
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";

                cmb.SelectedItem = plane;

                _y += I;
                _flights.Add(new FlightHelper(dt2, dt1, cmb){Flight = flight});
            } catch (Exception e) {
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
                MessageBox.Show(@"Invalid Interval");
            }
        }


        private void AddFlight(){
            try {
                int interval = int.Parse(txtInterval.Text);

                var dt1 = new DateTimePicker {
                    Location = new Point(1, _y),
                    Name = "dateTimePicker1",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = Resources.CreateFlight_DateTimeFormat
                };
                dt1.Enter += (dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt1);

                var dt2 = new DateTimePicker {
                    Location = new Point(148, _y),
                    Name = "dateTimePicker2",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = Resources.CreateFlight_DateTimeFormat
                };
                dt2.Enter += (dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt2);

                var cmb = new ComboBox {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FormattingEnabled = true,
                    Location = new Point(294, _y),
                    Name = "comboBox1",
                    Size = new Size(106, 21)
                };
                selectionPanel.Controls.Add(cmb);

                cmb.DataSource = _planes.ToList();
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";

                _y += I;


                if (interval != 0) {
                    dt2.Value = _flights[(_flights.Count - 1)].ArrivalTime.Value.AddHours(interval);
                    dt1.Value = _flights[(_flights.Count - 1)].DepartureTime.Value.AddHours(interval);
                }

                _flights.Add(new FlightHelper(dt2, dt1, cmb) {Flight = new Flight()});
            } catch (Exception) {
                MessageBox.Show(@"Invalid Interval");
            }
        }

        #endregion

        #region ErrorProvider Enter Event

        private void dateTimePicker_Enter(object sender, EventArgs e) {
            if (sender != null) {
                epFlights.SetError((DateTimePicker) sender, "");
            }
        }

        #endregion

        #region Load event

        private void CreateFlights_Load(object sender, EventArgs e) {
            Loading(true);
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                //MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
            } else {
                List<Plane> planes = e.Result as List<Plane>;
                _planes = planes;
                if (!_isEdit) {
                    AddFlight();
                } else {
                    if (Route.Flights != null) {
                        foreach (var flight in Route.Flights) {
                            AddFlight(flight);
                        }
                    } else {
                        AddFlight();
                    }
                }

                PopulateCmb(planes);

                Loading(false);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e) {
            PlaneCtr pCtr = new PlaneCtr();
            e.Result = pCtr.GetAllPlanes();
        }

        private void PopulateCmb(List<Plane> planes) {
            foreach (var flightHelper in _flights) {
                List<Plane> pl = planes.ToList();

                flightHelper.Plane.DataSource = pl;
                flightHelper.Plane.DisplayMember = "Name";
                flightHelper.Plane.ValueMember = "ID";

                if (flightHelper.Flight != null && flightHelper.Flight.Plane != null) {
                    flightHelper.Plane.SelectedValue = flightHelper.Flight.Plane.ID;
                }
            }
        }

        #endregion

        #region Button Events

        private void btnAddFlight_Click(object sender, EventArgs e) {
            AddFlight();
        }

        private void btnSaveForCreate_Click(object sender, EventArgs e) {
            RouteCtr rCtr = new RouteCtr();

            List<Flight> flights = _flights.Select(flightHelper => new Flight {
                ArrivalTime = flightHelper.ArrivalTime.Value, 
                DepartureTime = flightHelper.DepartureTime.Value, 
                Plane = (Plane) flightHelper.Plane.SelectedItem,
                ID = flightHelper.Flight.ID
            }).ToList();
            try {
                Route = rCtr.AddOrUpdateFlights(Route, flights);
                DialogResult = DialogResult.OK;
            } catch (DatabaseException ex) {
#if DEBUG
                ex.DebugGetLine();
#endif
                DialogResult = DialogResult.Retry;
                MessageBox.Show(ex.Message);
            
            } catch (DBConcurrencyException exception) {
#if DEBUG
                exception.DebugGetLine();
#endif
                DialogResult = DialogResult.Retry;
                MessageBox.Show(@"One or more of the entities was changed 
 Please search again.");
            }
            
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Dispose();
        }

        #endregion

        private void Loading(bool loading) {
            if (!loading) {
                loadingPicture.Visible = false;
                loadingPanel.Visible = false;
            } else {
                loadingPanel.BringToFront();
                loadingPanel.Visible = true;
                loadingPicture.Visible = true;
            }
        }
    }
}
