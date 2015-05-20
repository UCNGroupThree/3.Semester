using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.CustomerTabExtension;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;


namespace FlightAdmin.GUI {
    public partial class CustomerTab : UserControl {

        CustomerCtr customerCtr = new CustomerCtr();

        #region Constructors

        public CustomerTab()
        {
            InitializeComponent();
            SetEvents();
        }

        #endregion

        #region Search

        private void btnSearch_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            User user = null;

            try
            {
                if (txtID.Text.Trim() != "") {
                    user = customerCtr.GetUser(Int32.Parse(txtID.Text));
                }
                
            }
            catch (NullException ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
            if (user != null)
            {
                e.Result = new List<User> { user };
            }
  
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<User> userList = e.Result as List<User>;
                if (userList != null && userList.Count > 0)
                {
                    foreach (User user in userList) {
                        txtName.Text = user.Name;
                        txtAddress.Text = user.Address;
                        txtCity.Text = user.Postal.City;
                        txtZip.Text = Convert.ToString(user.Postal.PostCode);
                        txtPhone.Text = user.PhoneNumber;
                        txtEmail.Text = user.Email;
                        txtID.Text = Convert.ToString(user.ID);
                    }
                
                    UpdateDataGrid(userList);
                    errProvider.Clear();
                }
                else
                {
                    MessageBox.Show(this, @"No Users found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private User GetSelectedUser()
        {
            User user = null;
            var current = dataGrid.CurrentRow;
            if (current != null)
            {
                user = current.DataBoundItem as User;
            }
            
            return user;
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
           
            backgroundWorker2.RunWorkerAsync();
        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                if (txtName.Text.Trim() != "") {
                    e.Result = customerCtr.GetUserByName(txtName.Text);
                
                    } else {
                   MessageBox.Show("Sorry can't search on emty fields");
               }
                
                
            }
            catch (NullException ex) {
               
                MessageBox.Show(ex.Message);
                return;
            }
          

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
         
            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<User> userList = e.Result as List<User>;
                if (userList != null && userList.Count > 0)
                {
                    UpdateDataGrid(userList);
                    errProvider.Clear();
                }
                else
                {
                    MessageBox.Show(this, @"No Users found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e) {
                backgroundWorker3.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = customerCtr.GetAllUsers();

            }
            catch (NullException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                List<User> userList = e.Result as List<User>;
                if (userList != null && userList.Count > 0)
                {
                    UpdateDataGrid(userList);
                    errProvider.Clear();
                }
                else
                {
                    MessageBox.Show(this, @"No Users found", @"Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

      

        #endregion

        #region Delete

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            var value = (User)dataGrid.CurrentRow.DataBoundItem;
            int num = value.ID;
            User user = customerCtr.GetUser(num);
            if (user != null)
            {
                DeleteCustomer(user);
            }

        }

        private void DeleteCustomer(User user)
        {
            DialogResult res = MessageBox.Show("Are you shure you want to delete this customer: " + user.Name,
                "Remove", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {

                try
                {
                    customerCtr.DeleteUser(user);
                    userBindingSource.Remove(user);
                    List<User> userDeleted = new List<User>();

                    UpdateDataGrid(userDeleted);
                }
                catch (NullException)
                {
                    MessageBox.Show("The customer has already been deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        #endregion

        #region Create

        public void CreateCustomer()
        {

            Postal postal = new Postal
            {
                PostCode = Int32.Parse(txtZip.Text),
                City = txtCity.Text

            };

            if (postal == null) throw new ArgumentNullException();

            customerCtr.CreateUser(txtName.Text.Trim(), txtAddress.Text.Trim(), postal, txtPhone.Text.Trim(), txtEmail.Text.Trim());
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                CreateCustomer();
                ClearFields();
            }
            else
            {
                MessageBox.Show(this, @"The form isn't valid or completed!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
          
        }


        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCustomer dialog = new CreateCustomer();
            dialog.Show(this);
        }

        #endregion

        #region Edit

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CreateCustomer dialog = new CreateCustomer(GetSelectedUser());
            dialog.Show(this);

        }

        #endregion

        #region Validation

        private bool IsIdValid() {
            return FancyFeatures.IsTextBoxValid(txtID, errProvider, lblID.Text, 1, 10);
        }

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
            if (!IsIdValid()) {
                txtID.Focus();
            }
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




        private void SetEvents()
        {
            foreach (TextBox t in tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                if (t == txtID) {
                    t.TextChanged += FancyFeatures.CheckedChangedDisableParentsInputControls;
                }
            }           
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

            errProvider.Clear();

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
