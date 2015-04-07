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
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.CustomerTabExtension
{
    public partial class CreateCustomer : Form
    {

        private readonly CustomerCtr customerCtr = new CustomerCtr();
        public CreateCustomer()
        {
            InitializeComponent();
        }

        public CreateCustomer(User user) : this() {
            if (user == null) throw new ArgumentNullException();

            base.Text = "Edit Customer";
            lblHeader.Text = "Edit Customer";

            txtName.Text = user.Name;
            txtAddress.Text = user.Address;
            txtCity.Text = user.Postal.City;
            txtZip.Text = user.Postal.PostCode.ToString();
            txtEmail.Text = user.Email;
            txtPhone.Text = user.PhoneNumber;

            btnSave.Text = "Save";
            btnSave.Click -= btnSaveForCreation_Click;
            btnSave.Click += btnSaveForEdit_Click;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region validating
        private bool IsNameValid()
        {
            return FancyFeatures.IsTextBoxValid(txtName, errProvider, lblName.Text, 3, 60);
        }

        private bool IsAddressValid()
        {
            return FancyFeatures.IsTextBoxValid(txtAddress, errProvider, lblName.Text, 3, 60);
        }

        private bool IsCityValid()
        {
            return FancyFeatures.IsTextBoxValid(txtCity, errProvider, lblName.Text, 3, 60);
        }


        private bool IsZipValid()
        {
            return FancyFeatures.IsTextBoxValid(txtZip, errProvider, lblName.Text, 3, 60);
        }

        private bool IsEmailValid()
        {
            return FancyFeatures.IsTextBoxValid(txtEmail, errProvider, lblName.Text, 3, 60);
        }

        private bool IsPhoneValid()
        {
            return FancyFeatures.IsTextBoxValid(txtPhone, errProvider, lblName.Text, 3, 60);
        }

        private bool IsFormValid()
        {
            bool valid = false;
            if (!IsNameValid())
            {
                txtName.Focus();
            }
            else if (!IsAddressValid())
            {
                txtAddress.Focus();
            }
            else if (!IsCityValid())
            {
                txtCity.Focus();
            }        
            else if (!IsZipValid())
            {
                txtZip.Focus();
            }
            else if (!IsEmailValid())
            {
                txtEmail.Focus();
            }
            else if (!IsPhoneValid())
            {
                txtPhone.Focus();
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        #endregion

        #region Validating events
        private void txtName_Validating(object sender, CancelEventArgs e) {
            IsNameValid();
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e) {
            IsAddressValid();
        }

        private void txtCity_Validating(object sender, CancelEventArgs e) {
            IsCityValid();
        }

        private void txtZip_Validating(object sender, CancelEventArgs e) {
            IsZipValid();
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e) {
            IsEmailValid();
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e) {
            IsPhoneValid();
        }

        #endregion

      

        private void btnSaveForEdit_Click(object sender, EventArgs e) {
            MessageBox.Show("hello");
        }

        private void btnSaveForCreation_Click(object sender, EventArgs e) {
            MessageBox.Show("Create");
        }

       

       






    }
}
