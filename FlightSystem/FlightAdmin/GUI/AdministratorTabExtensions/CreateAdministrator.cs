using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Common.Exceptions;
using FlightAdmin.Controller;
using FlightAdmin.GUI.Helper;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.AdministratorTabExtensions {
    public partial class CreateAdministrator : Form {
        private readonly AdministratorCtr ctr = new AdministratorCtr();
        private string _nextUsername = null; //used by usernameWorker
        public Administrator Administrator { get; private set; }
        private bool IsEditing { get; set; }

        #region Constructors

        /// <summary>
        /// Create new Administrator
        /// </summary>
        public CreateAdministrator() {
            InitializeComponent();
            txtUsername.TextChanged += (sender, args) => _nextUsername = null;
        }

        /// <summary>
        /// Edit an Administrator
        /// </summary>
        /// <param name="administrator" >The administrator to edit</param>
        /// <exception cref="ArgumentNullException" />
        public CreateAdministrator(Administrator administrator)
            : this() {
            if (administrator == null) throw new ArgumentNullException();

            Administrator = administrator;
            IsEditing = true;
            string editText = string.Format("Edit Admin - #{0}", administrator.ID);
            base.Text = editText;
            lblHeader.Text = editText;

            txtUsername.Text = administrator.Username;

            btnSave.Click -= btnSaveForCreation_Click;
            btnSave.Text = @"Save";
            btnSave.Click += btnSaveForEditing_Click;

            chbChangePassword.Visible = true;
            toolTip.SetToolTip(chbChangePassword, "Check to Change password");
            txtPassword.Enabled = false;
            txtPasswordAgain.Enabled = false;
        }

        #endregion

        #region Editing

        private void chbChangePassword_CheckedChanged(object sender, EventArgs e) {
            bool enabled = chbChangePassword.Checked;
            txtPassword.Enabled = enabled;
            txtPasswordAgain.Enabled = enabled;
            /*if (enabled) {
                txtPassword.Validating += txtPassword_Validating;
                txtPasswordAgain.Validating += txtPasswordAgain_Validating;
            } else {
                txtPassword.Validating -= txtPassword_Validating;
                txtPasswordAgain.Validating -= txtPasswordAgain_Validating;
             */
            txtPassword.Text = "";
            txtPasswordAgain.Text = "";
            //}
            errProvider.SetError(txtPassword, "");
            errProvider.SetError(txtPasswordAgain, "");
        }

        private void btnSaveForEditing_Click(object sender, EventArgs eventArgs) {
            if (!editWorker.IsBusy) {
                if (usernameWorker.IsBusy) {
                    usernameWorker.CancelAsync();
                }
                if (IsEditFormValid()) {
                    loadingImg.Visible = true;
                    editWorker.RunWorkerAsync();
                }
            } else {
                MessageBox.Show(this, @"A saving is pending, please wait for it to complete!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void editWorker_DoWork(object sender, DoWorkEventArgs e) {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (!chbChangePassword.Checked) {
                password = null;
            }
            e.Result = ctr.UpdateAdministrator(Administrator, username, password);
        }

        private void editWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImg.Visible = false;
            if (e.Error != null) {
                Exception ex = e.Error;
                if (ex is AlreadyExistException) {
                    errProvider.SetError(txtUsername, txtUsername.Text.Trim() + " already exists!");
                    txtUsername.Focus();
                } else if (ex is PasswordFormatException) {
                    errProvider.SetError(txtPassword, "Wrong Format");
                    txtPassword.Focus();
                } else {
                    MessageBox.Show(this, ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Edit Exception!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex);
                }
            } else {
                Administrator administrator = e.Result as Administrator;
                if (administrator != null) {
                    Administrator = administrator;
                    Visible = false;
                    MessageBox.Show(this, @"The administrator has been updated", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                } else {
                    MessageBox.Show(this, @"Unknown Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Validating methods

        private bool IsUsernameValid() {
            return FancyFeatures.IsTextBoxValid(txtUsername, errProvider, lblUsername.Text, 3, 40);
        }

        private bool IsPasswordValid() {
            bool valid = FancyFeatures.IsTextBoxValid(txtPassword, errProvider, lblPassword.Text, 4, 60);
            string password = txtPassword.Text;
            if (valid) {
                if (!password.Any(char.IsUpper)) {
                    errProvider.SetError(txtPassword, "Password must contains at least one uppercase");
                    valid = false;
                } else if (!password.Any(char.IsLower)) {
                    errProvider.SetError(txtPassword, "Password must contains at least one lowercase");
                    valid = false;
                } else if (!password.Any(char.IsDigit)) {
                    errProvider.SetError(txtPassword, "Password must contains at least one digit");
                    valid = false;
                }
            }
            return valid;
        }

        private bool IsPasswordAgainValid() {
            //FancyFeatures.IsTextBoxValid(txtPasswordAgain, errProvider, lblPasswordAgain.Text, 4, 60);
            bool valid = (txtPassword.Text.Equals(txtPasswordAgain.Text));
            if (!valid) {
                errProvider.SetError(txtPasswordAgain, "Password is not the same!");
            }
            return valid;
        }

        private bool IsCreateFormValid() {
            bool valid = false;
            if (!IsUsernameValid()) {
                txtUsername.Focus();
            } else if (!IsPasswordValid()) {
                txtPassword.Focus();
            } else if (!IsPasswordAgainValid()) {
                txtPasswordAgain.Focus();
            } else {
                valid = true;
            }
            return valid;
        }

        private bool IsEditFormValid() {
            bool valid = IsUsernameValid();
            if (!valid && !chbChangePassword.Checked) {
                txtUsername.Focus();
            } else if (chbChangePassword.Checked) {
                valid = IsCreateFormValid();  //True, If changing password
            }
            Console.WriteLine(valid);
            return valid;
        }

        #endregion

        #region Validating Events

        private void txtUsername_Validating(object sender, CancelEventArgs e) {
            if (IsUsernameValid() && !string.IsNullOrWhiteSpace(txtUsername.Text)) {
                _nextUsername = txtUsername.Text.Trim();
                if (!usernameWorker.IsBusy) {
                    usernameWorker.RunWorkerAsync();
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e) {
            IsPasswordValid();
        }

        private void txtPasswordAgain_Validating(object sender, CancelEventArgs e) {
            IsPasswordAgainValid();
        }

        #endregion

        #region Other Events

        private void txtRemoveErrorOn_TextChanged(object sender, EventArgs e) {
            TextBox txt = sender as TextBox;
            if (txt != null) {
                errProvider.SetError(txt, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) { //TODO Kig her?
            Dispose();
        }

        #endregion

        #region Creating

        private void btnSaveForCreation_Click(object sender, EventArgs e) {
            if (!createWorker.IsBusy) {
                if (usernameWorker.IsBusy) {
                    usernameWorker.CancelAsync();
                }
                if (IsCreateFormValid()) {
                    loadingImg.Visible = true;
                    createWorker.RunWorkerAsync();
                }
            } else {
                MessageBox.Show(this, @"A creation is pending, please wait for it to complete!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void createWorker_DoWork(object sender, DoWorkEventArgs e) {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            e.Result = ctr.CreateAdministrator(username, password);
        }

        private void createWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingImg.Visible = false;
            if (e.Error != null) {
                Exception ex = e.Error;
                if (ex is AlreadyExistException) {
                    errProvider.SetError(txtUsername, txtUsername.Text.Trim() + " already exists!");
                    txtUsername.Focus();
                } else if (ex is PasswordFormatException) {
                    errProvider.SetError(txtPassword, "Wrong password format!");
                    txtPassword.Focus();
                } else {
                    MessageBox.Show(this, ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Create Exception!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex);
                }
            } else {
                Administrator administrator = e.Result as Administrator;
                if (administrator != null) {
                    Administrator = administrator;
                    Visible = false;
                    MessageBox.Show(this, @"The administrator has been created", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                } else {
                    MessageBox.Show(this, @"Unknown Error", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region BackgroundWorker for txtUsername

        private void usernameWorker_DoWork(object sender, DoWorkEventArgs e) {
            string exists = null;
            while (!string.IsNullOrWhiteSpace(_nextUsername) && !usernameWorker.CancellationPending) {
                string search = _nextUsername;
                List<Administrator> list = ctr.GetAdministratorsByUsername(search, true);

                //remove Administrator, if editing
                if (Administrator != null) {
                    list.RemoveAll(a => a.ID == Administrator.ID);
                }

                if (list != null && list.Any()) {
                    exists = search;
                } else {
                    exists = null;
                }
                //Console.WriteLine("Exits: " + search + ": " + exists);
                //Console.WriteLine("next: " + _nextUsername);
                if (search.Equals(_nextUsername)) {
                    _nextUsername = null;
                }
                //Thread.Sleep(3000);
            }
            if (usernameWorker.CancellationPending) {
                e.Cancel = true;
            }
            e.Result = exists;
        }

        private void usernameWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled && e.Result != null && !txtUsername.ContainsFocus) {
                errProvider.SetError(txtUsername, e.Result + " already exists!");
            }
        }

        #endregion

    }
}
