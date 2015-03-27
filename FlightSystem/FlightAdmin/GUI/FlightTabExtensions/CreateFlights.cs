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
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public partial class CreateFlights : UserControl {

        public Route Route { get; set; }

        private int y;
        private int i = 25;
        private List<Plane> _planes;
        private List<FlightHelper> _flights; 

        public CreateFlights() {
            InitializeComponent();
            y = dateTimePicker1.Location.Y;
            _flights = new List<FlightHelper>();
            _flights.Add(new FlightHelper(dateTimePicker1, dateTimePicker2, comboBox1));
        }


        private void More() {


            try {
                int interval = int.Parse(txtInterval.Text);

                var dt1 = new System.Windows.Forms.DateTimePicker {
                    Location = new System.Drawing.Point(dateTimePicker1.Location.X, y + i),
                    Name = "dateTimePicker1",
                    Size = dateTimePicker1.Size,
                    Format = dateTimePicker1.Format,
                    CustomFormat = dateTimePicker1.CustomFormat
                };
                dt1.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt1);

                var dt2 = new System.Windows.Forms.DateTimePicker {
                    Location = new System.Drawing.Point(dateTimePicker2.Location.X, y + i),
                    Name = "dateTimePicker2",
                    Size = dateTimePicker2.Size,
                    Format = dateTimePicker2.Format,
                    CustomFormat = dateTimePicker2.CustomFormat
                };
                dt2.Enter += new EventHandler(dateTimePicker_Enter);
                selectionPanel.Controls.Add(dt2);

                var cmb = new System.Windows.Forms.ComboBox {
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    FormattingEnabled = true,
                    Location = new System.Drawing.Point(comboBox1.Location.X, y + i),
                    Name = "comboBox1",
                    Size = comboBox1.Size
                };
                selectionPanel.Controls.Add(cmb);

                cmb.DataSource = _planes.ToList();
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";

                y += i;


                if (interval != 0) {
                    dt1.Value = _flights[(_flights.Count - 1)].ArrivalTime.Value.AddHours(interval);
                    dt2.Value = _flights[(_flights.Count - 1)].DepartureTime.Value.AddHours(interval);
                }

                _flights.Add(new FlightHelper(dt1, dt2, cmb));
            } catch (Exception) {
                MessageBox.Show("Invalid Interval");
            }
        }

        private void dateTimePicker_Enter(object sender, EventArgs e) {
            if (sender != null) {
                epFlights.SetError(sender as DateTimePicker, "");
            }
        }


        private void LoadPlanes() {
            BeginInvoke((MethodInvoker)delegate {
                loadingPanel.BringToFront();
                loadingPanel.Visible = true;
                loadingPicture.Visible = true;
            });

            PlaneCtr pCtr = new PlaneCtr();
            List<Plane> pl = pCtr.GetAllPlanes();

            BeginInvoke((MethodInvoker)delegate {
                _planes = pl;
                loadingPicture.Visible = false;
                loadingPanel.Visible = false;
            });

            PopulateCmb();
        }

        private void PopulateCmb() {
            BeginInvoke((MethodInvoker)delegate {
                foreach (var flightHelper in _flights) {
                    List<Plane> pl = _planes.ToList();

                    flightHelper.Plane.DataSource = pl;
                    flightHelper.Plane.DisplayMember = "Name";
                    flightHelper.Plane.ValueMember = "ID";
                }
            });
        }

        private void CreateFlights_Load(object sender, EventArgs e) {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((obj, ex) => LoadPlanes());
            bgWorker.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e) {
            More();
        }


    }
}
