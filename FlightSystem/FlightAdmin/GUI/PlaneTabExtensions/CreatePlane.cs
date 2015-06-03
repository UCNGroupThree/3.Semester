using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightAdmin.Controller;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.PlaneTabExtensions
{
    public partial class CreatePlane : Form
    {
        public Plane Plane { get; private set; }


        // Empty constructor
        public CreatePlane() 
        {
            InitializeComponent();
        }

        public CreatePlane(Plane plane) {
            InitializeComponent();
            Plane = plane;
            txtName.Text = plane.Name;
            spinnerSeats.Text = plane.Seats.Count.ToString();

        }

        private bool ValidatePlane() {

            try {
                if (txtName.Text.Trim() == "") {

                    epPlane.SetError(txtName, "Name field cant be empty");
                    return false;
                }

                decimal.Parse(spinnerSeats.Text);

            } catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            return true;
        }

        private void CreatePlane_Load(object sender, EventArgs e)
        {
           
        }

        private void btnCreateNewPlane_Click(object sender, EventArgs e)
        {
            if (ValidatePlane()) {

                try {
                    PlaneCtr planeCtr = new PlaneCtr();
                    Plane plane = planeCtr.CreatePlane(txtName.Text.Trim(), Convert.ToInt32(spinnerSeats.Text));

                    MessageBox.Show(System.String.Format("The plane: \n {0} \n has been created med {1} seats", plane.Name, plane.Seats.Count),
                        "Plane created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                //catch (ValidationException exception) {
                //    //TODO Something here
                //}
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancelNewPlane_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        
    }

}
