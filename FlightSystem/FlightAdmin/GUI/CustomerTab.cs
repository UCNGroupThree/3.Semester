﻿using System;
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
using FlightAdmin.GUI.CustomerTabExtension;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;


namespace FlightAdmin.GUI {
    public partial class CustomerTab : UserControl {

        CustomerCtr customerCtr = new CustomerCtr();
        private DataGridViewCellEventArgs mouseLocation;
        public CustomerTab() {
            InitializeComponent();
            SetEvents();
        }


        private void SetEvents()
        {
            foreach (TextBox t in tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                if (t == txtID) {
                    t.TextChanged += FancyFeatures.TextChangedDisableParentsTextboxs;
                }
            }           
        }

        private void btnClear_Click(object sender, EventArgs e) {
            ClearFields();
        }

        public void CreateCustomer() {

           Postal postal = new Postal {
               PostCode = Int32.Parse(txtZip.Text),
               City = txtCity.Text
               
           };

            if (postal == null) throw new ArgumentNullException();              
            
            customerCtr.CreateUser(txtName.Text.Trim(), txtAddress.Text.Trim(), postal, txtPhone.Text.Trim(), txtEmail.Text.Trim());        
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


        private void SearchUserByName() {

            List<User> userList = customerCtr.GetUserByName(txtName.Text);
            UpdateDataGrid(userList);
           
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateCustomer();
            ClearFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //SearchUser();
            backgroundWorker1.RunWorkerAsync();
          //  SearchUserByName();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            User user;

            try
            {
                user = customerCtr.GetUser(Int32.Parse(txtID.Text));
            }
            catch (NullException ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
            if (user != null) {
                txtName.Text = user.Name;
                txtAddress.Text = user.Address;
                txtCity.Text = user.Postal.City;
                txtZip.Text = Convert.ToString(user.Postal.PostCode);
                txtPhone.Text = user.PhoneNumber;
                txtEmail.Text = user.Email;
                txtID.Text = Convert.ToString(user.ID);
                e.Result = new List<User> { user };

            }
           
            //UpdateDataGrid(userList);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null) {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                List<User> userList = e.Result as List<User>;
                if (userList != null  && userList.Count > 0) {
                    UpdateDataGrid(userList);
                } else {
                    MessageBox.Show(this, @"No Users found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCustomer dialog = new CreateCustomer();
            dialog.Show(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {

         
            var som = (User) dataGrid.Rows[mouseLocation.RowIndex].DataBoundItem;
            if (som != null) {
                customerCtr.DeleteUser(som);
            }

        }

   

     
    }
}
