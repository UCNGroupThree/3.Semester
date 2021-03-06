﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.CustomerTabExtension
{
    public partial class CreateCustomer : Form
    {

        private readonly CustomerCtr customerCtr = new CustomerCtr();
        public User User { get; set; }

        #region Constructors
        public CreateCustomer()
        {
            InitializeComponent();
        }

        public CreateCustomer(User user) : this() {
            if (user == null) throw new ArgumentNullException();
            if (user.Postal == null) throw new ArgumentNullException();
            User = user;
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

        #endregion

        #region Editing

        private void btnSaveForEdit_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                editWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show(this, @"Somthing went wrong!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void editWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Postal postal = User.Postal;
            postal.PostCode = Int32.Parse(txtZip.Text);
            postal.City = txtCity.Text;

            e.Result = customerCtr.UpdateUser(User, txtName.Text.Trim(), txtAddress.Text.Trim(), postal, txtPhone.Text.Trim(), txtEmail.Text.Trim());

        }

        private void editWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Exception ex = e.Error;
                if (ex is AlreadyExistException)
                {
                    errProvider.SetError(txtName, txtName.Text.Trim() + "already exists!");
                }
                else
                {
                    MessageBox.Show(this, ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                User user = e.Result as User;
                if (user != null)
                {
                    MessageBox.Show(this, @"The customer has been Updated", @"Succese", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, @"Unknown Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        #endregion

        #region Creating

        private void btnSaveForCreation_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                createWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show(this, @"Somthing went wrong!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void createWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Postal postal = new Postal
            {
                PostCode = Int32.Parse(txtZip.Text),
                City = txtCity.Text

            };

            if (postal == null) throw new ArgumentNullException();

            e.Result = customerCtr.CreateUser(txtName.Text.Trim(), txtAddress.Text.Trim(), postal, txtPhone.Text.Trim(), txtEmail.Text.Trim());
        }

        private void createWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Exception ex = e.Error;
                if (ex is AlreadyExistException)
                {
                    errProvider.SetError(txtName, txtName.Text.Trim() + "already exists!");
                }
                else
                {
                    MessageBox.Show(this, ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                User user = e.Result as User;
                if (user != null)
                {
                    MessageBox.Show(this, @"The customer has been created", @"Succese", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, @"Unknown Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region validating
        private bool IsNameValid()
        {
            return FancyFeatures.IsTextBoxValid(txtName, errProvider, lblName.Text, 3, 60);
        }

        private bool IsAddressValid()
        {
            return FancyFeatures.IsTextBoxValid(txtAddress, errProvider, lblAddress.Text, 3, 60);
        }

        private bool IsCityValid()
        {
            return FancyFeatures.IsTextBoxValid(txtCity, errProvider, lblCity.Text, 3, 60);
        }


        private bool IsZipValid()
        {
            return FancyFeatures.IsTextBoxValid(txtZip, errProvider, lblZip.Text, 3, 60);
        }

        private bool IsEmailValid()
        {
            return EmailValidation.IsEmailValid(txtEmail, errProvider, lblEmail.Text);
        }

        private bool IsPhoneValid()
        {
            return FancyFeatures.IsTextBoxValid(txtPhone, errProvider, lblPhone.Text, 3, 60);
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

        #region Other Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
