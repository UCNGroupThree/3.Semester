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
    public partial class CustomerTab : UserControl {

        CustomerCtr customerCtr = new CustomerCtr();

        public CustomerTab() {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            ClearFields();
        }

        public void ClearFields()
        {

            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtID.Text = "";

        }

        private void SearchUser() {

            User user;

            try {
                user = customerCtr.GetUser(Int32.Parse(txtID.Text));
            } catch (NullException e) {

                MessageBox.Show(e.Message);
                return;
            }

            txtName.Text = user.Name;
            txtAddress.Text = user.Address;
            txtCity.Text = "";
            txtZip.Text = "";
            txtPhone.Text = user.PhoneNumber;
            txtEmail.Text = user.Email;
            txtID.Text = Convert.ToString(user.ID);

        }


        private void UpdateDataGrid(List<User> users)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (users != null)
                {
                    userBindingSource.Clear();
                    foreach (var r in users)
                    {
                        userBindingSource.Add(r);
                    }

                }
            });
        }
    }
}
