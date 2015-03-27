using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public partial class CreateFlight : UserControl {

        private int y;
        private int i = 25;
        private List<Plane> planes; 

        private List<FlightHelper> flights; 
        public CreateFlight() {
            InitializeComponent();
            y = dateTimePicker1.Location.Y;
            flights = new List<FlightHelper>();
            flights.Add(new FlightHelper(dateTimePicker1, dateTimePicker2, comboBox1));
        }

        private void More() {
            var dt1 = new System.Windows.Forms.DateTimePicker();
            dt1.Location = new System.Drawing.Point(3, y + i);
            dt1.Name = "dateTimePicker1";
            dt1.Size = new System.Drawing.Size(200, 20);
            dt1.Format = DateTimePickerFormat.Custom;
            dt1.CustomFormat = "yyyy.MM.dd HH:mm";
            dt1.Enter += new EventHandler(dateTimePicker_Enter);
            this.Controls.Add(dt1);
            
            var dt2 = new System.Windows.Forms.DateTimePicker();
            dt2.Location = new System.Drawing.Point(227, y + i);
            dt2.Name = "dateTimePicker2";
            dt2.Size = new System.Drawing.Size(200, 20);
            dt2.Format = DateTimePickerFormat.Custom;
            dt2.CustomFormat = "yyyy.MM.dd HH:mm";
            dt2.Enter += new EventHandler(dateTimePicker_Enter);
            this.Controls.Add(dt2);

            var cmb = new System.Windows.Forms.ComboBox();
            cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb.FormattingEnabled = true;
            cmb.Location = new System.Drawing.Point(444, y + i);
            cmb.Name = "comboBox1";
            cmb.Size = new System.Drawing.Size(168, 20);
            this.Controls.Add(cmb);

            flights.Add(new FlightHelper(dt1, dt2, cmb));

            cmb.DataSource = planes.ToList();
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";

            y += i;
        }

        private void button1_Click(object sender, EventArgs e) {
            More();
        }


        private void LoadPlanes() {
            BeginInvoke((MethodInvoker) delegate {
                loadingPanel.Visible = true;
            });
                
                PlaneCtr pCtr = new PlaneCtr();
                List<Plane> pl = pCtr.GetAllPlanes();

            BeginInvoke((MethodInvoker) delegate {
                planes = pl;
                loadingPanel.Visible = false;
            });

            PopulateCmb();
        }

        private void PopulateCmb() {
            BeginInvoke((MethodInvoker) delegate {
                foreach (var flightHelper in flights) {
                    List<Plane> pl = planes.ToList();

                    flightHelper.Plane.DataSource = pl;
                    flightHelper.Plane.DisplayMember = "Name";
                    flightHelper.Plane.ValueMember = "ID";
                }
            });
        }

        private void CreateFlight_Load(object sender, EventArgs e) {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler((obj, ex) => LoadPlanes());
            bgWorker.RunWorkerAsync();
        }

        private void dateTimePicker_Enter(object sender, EventArgs e) {
            if (sender != null) {
                epDate.SetError(sender as DateTimePicker, "");
            }
        }
    }
}
