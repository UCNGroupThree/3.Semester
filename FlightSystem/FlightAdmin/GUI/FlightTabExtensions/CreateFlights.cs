using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public partial class CreateFlights : Form {

        public Route Route { get; set; }
        private bool isEdit;

        private int y = 15;
        private int i = 25;
        private List<Plane> _planes;
        private List<FlightHelper> _flights;
        private bool loading = true;

        public CreateFlights() {
            InitializeComponent();
            //y = dateTimePicker1.Location.Y;
            _flights = new List<FlightHelper>();
            isEdit = false;
        }

        public CreateFlights(Route route) {
            Route = route;
            if (route == null) throw new ArgumentNullException();

            InitializeComponent();
            _flights = new List<FlightHelper>();
            isEdit = true;
            
            btnSave.Click -= btnSaveForCreate_Click;
            btnSave.Click += btnSaveForEdit_Click;
        }

        #region AddFlights

        private void AddFlight(Flight flight) {
            DateTime dep = flight.DepartureTime;
            DateTime arr = flight.ArrivalTime;
            Plane plane = flight.Plane;
            try {
                var dt1 = new System.Windows.Forms.DateTimePicker {
                    Location = new Point(1, y),
                    Name = "dateTimePicker1",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "yyyy.MM.dd HH:mm",
                    //Value = arr
                    Value = dep
                };
                dt1.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt1);

                var dt2 = new System.Windows.Forms.DateTimePicker {
                    Location = new Point(148, y),
                    Name = "dateTimePicker2",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "yyyy.MM.dd HH:mm",
                    //Value = dep
                    Value = arr
                };
                dt2.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt2);

                var cmb = new System.Windows.Forms.ComboBox {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FormattingEnabled = true,
                    Location = new Point(294, y),
                    Name = "comboBox1",
                    Size = new Size(106, 21)
                };
                selectionPanel.Controls.Add(cmb);

                cmb.DataSource = _planes.ToList();
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";

                cmb.SelectedItem = plane;

                y += i;
                _flights.Add(new FlightHelper(dt2, dt1, cmb){Flight = flight});
            } catch (Exception e) {
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
                MessageBox.Show("Invalid Interval");
            }
        }


        private void AddFlight(){
            try {
                int interval = int.Parse(txtInterval.Text);

                var dt1 = new DateTimePicker {
                    Location = new Point(1, y),
                    Name = "dateTimePicker1",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "yyyy.MM.dd HH:mm"
                };
                dt1.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt1);

                var dt2 = new DateTimePicker {
                    Location = new Point(148, y),
                    Name = "dateTimePicker2",
                    Size = new Size(126, 20),
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "yyyy.MM.dd HH:mm"
                };
                dt2.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt2);

                var cmb = new ComboBox {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FormattingEnabled = true,
                    Location = new Point(294, y),
                    Name = "comboBox1",
                    Size = new Size(106, 21)
                };
                selectionPanel.Controls.Add(cmb);

                cmb.DataSource = _planes.ToList();
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";

                y += i;


                if (interval != 0) {
                    dt2.Value = _flights[(_flights.Count - 1)].ArrivalTime.Value.AddHours(interval);
                    dt1.Value = _flights[(_flights.Count - 1)].DepartureTime.Value.AddHours(interval);
                }

                _flights.Add(new FlightHelper(dt2, dt1, cmb) {Flight = new Flight()});
            } catch (Exception) {
                MessageBox.Show("Invalid Interval");
            }
        }

        #endregion

        #region ErrorProvider Enter Event

        private void dateTimePicker_Enter(object sender, EventArgs e) {
            if (sender != null) {
                epFlights.SetError(sender as DateTimePicker, "");
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
                this.Dispose();
            } else {
                List<Plane> planes = e.Result as List<Plane>;
                _planes = planes;
                if (!isEdit) {
                    AddFlight();
                } else {
                    if (Route.Flights != null) {
                        foreach (var flight in Route.Flights) {
                            System.Diagnostics.Debug.WriteLine(flight.ID + "added"); //TODO Remove after test
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
                ArrivalTime = flightHelper.ArrivalTime.Value, DepartureTime = flightHelper.DepartureTime.Value, Plane = (Plane) flightHelper.Plane.SelectedItem
            }).ToList();

            Route = rCtr.UpdateRoute(Route, Route.From, Route.To, flights);

            TestCrap(Route.Flights); //TODO Remove after test

            DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnSaveForEdit_Click(object sender, EventArgs e) {
            RouteCtr rCtr = new RouteCtr();

            List<Flight> flights = _flights.Select(flightHelper => new Flight {
                ArrivalTime = flightHelper.ArrivalTime.Value,
                DepartureTime = flightHelper.DepartureTime.Value,
                Plane = (Plane)flightHelper.Plane.SelectedItem,
                ID = flightHelper.Flight.ID
            }).ToList();



            Route = rCtr.UpdateRoute(Route, Route.From, Route.To, flights);

            TestCrap(Route.Flights); //TODO Remove after test

            DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        #endregion




        private void TestCrap(List<Flight> fl) { //TODO Remove after test
            foreach (var flight in fl) {
                Debug.WriteLine(flight.ID + " Returned");
            }
        }

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
