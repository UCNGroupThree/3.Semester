using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public partial class CreateFlight : UserControl {

        private int y;
        private int i = 25;

        private List<FlightHelper> flights; 
        public CreateFlight() {
            InitializeComponent();
            y = dateTimePicker1.Location.Y;
            flights = new List<FlightHelper>();
            flights.Add(new FlightHelper(dateTimePicker1, dateTimePicker2, comboBox1));
        }

        //YourDatePicker.Format = DateTimePickerFormat.Custom;
         //YourDatePicker.CustomFormat = "yyyy.MM.dd HH:mm";
        private void More() {
            var dt1 = new System.Windows.Forms.DateTimePicker();
            dt1.Location = new System.Drawing.Point(3, y + i);
            dt1.Name = "dateTimePicker1";
            dt1.Size = new System.Drawing.Size(200, 20);
            dt1.Format = DateTimePickerFormat.Custom;
            dt1.CustomFormat = "yyyy.MM.dd HH:mm";
            this.Controls.Add(dt1);
            
            var dt2 = new System.Windows.Forms.DateTimePicker();
            dt2.Location = new System.Drawing.Point(227, y + i);
            dt2.Name = "dateTimePicker2";
            dt2.Size = new System.Drawing.Size(200, 20);
            dt2.Format = DateTimePickerFormat.Custom;
            dt2.CustomFormat = "yyyy.MM.dd HH:mm";
            this.Controls.Add(dt2);

            var cmb = new System.Windows.Forms.ComboBox();
            cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb.FormattingEnabled = true;
            cmb.Location = new System.Drawing.Point(444, y + i);
            cmb.Name = "comboBox1";
            cmb.Size = new System.Drawing.Size(168, 20);
            this.Controls.Add(cmb);

            flights.Add(new FlightHelper(dt1, dt2, cmb));

            y += i;
        }

        private void button1_Click(object sender, EventArgs e) {
            More();
        }

        private void button2_Click(object sender, EventArgs e) {
            foreach (var flightHelper in flights) {
                var str = String.Format(flightHelper.ArrivalTime.Text + " - " + flightHelper.DepartureTime.Text + "\n");
                txtTest.AppendText(str);
            }
        }
    }
}
