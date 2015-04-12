namespace FlightAdmin.GUI.AdministratorTabExtensions {
    partial class CreateAdministrator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPasswordAgain = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPasswordAgain = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadingImg = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPassword = new System.Windows.Forms.TableLayoutPanel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chbChangePassword = new System.Windows.Forms.CheckBox();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.usernameWorker = new System.ComponentModel.BackgroundWorker();
            this.createWorker = new System.ComponentModel.BackgroundWorker();
            this.editWorker = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImg)).BeginInit();
            this.tableLayoutPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblUsername, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPasswordAgain, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPasswordAgain, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPassword, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 152);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHeader.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHeader, 3);
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(78, 3);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(3);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(118, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Create Admin";
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(18, 32);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // lblPasswordAgain
            // 
            this.lblPasswordAgain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPasswordAgain.AutoSize = true;
            this.lblPasswordAgain.Location = new System.Drawing.Point(18, 84);
            this.lblPasswordAgain.Name = "lblPasswordAgain";
            this.lblPasswordAgain.Size = new System.Drawing.Size(83, 13);
            this.lblPasswordAgain.TabIndex = 1;
            this.lblPasswordAgain.Text = "Password Again";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(108, 29);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(146, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            this.txtUsername.Validating += new System.ComponentModel.CancelEventHandler(this.txtUsername_Validating);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(108, 55);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(146, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // txtPasswordAgain
            // 
            this.txtPasswordAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswordAgain.Location = new System.Drawing.Point(108, 81);
            this.txtPasswordAgain.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtPasswordAgain.Name = "txtPasswordAgain";
            this.txtPasswordAgain.Size = new System.Drawing.Size(146, 20);
            this.txtPasswordAgain.TabIndex = 2;
            this.txtPasswordAgain.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            this.txtPasswordAgain.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasswordAgain_Validating);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.loadingImg);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(108, 107);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 42);
            this.panel1.TabIndex = 4;
            // 
            // loadingImg
            // 
            this.loadingImg.Image = global::FlightAdmin.Properties.Resources.loading1;
            this.loadingImg.Location = new System.Drawing.Point(51, 7);
            this.loadingImg.Name = "loadingImg";
            this.loadingImg.Size = new System.Drawing.Size(20, 20);
            this.loadingImg.TabIndex = 6;
            this.loadingImg.TabStop = false;
            this.loadingImg.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(71, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Create";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveForCreation_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(18, 112);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPassword
            // 
            this.tableLayoutPassword.ColumnCount = 2;
            this.tableLayoutPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.2381F));
            this.tableLayoutPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.tableLayoutPassword.Controls.Add(this.lblPassword, 0, 0);
            this.tableLayoutPassword.Controls.Add(this.chbChangePassword, 1, 0);
            this.tableLayoutPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPassword.Location = new System.Drawing.Point(15, 52);
            this.tableLayoutPassword.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPassword.Name = "tableLayoutPassword";
            this.tableLayoutPassword.RowCount = 1;
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPassword.Size = new System.Drawing.Size(90, 26);
            this.tableLayoutPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 6);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // chbChangePassword
            // 
            this.chbChangePassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chbChangePassword.AutoSize = true;
            this.chbChangePassword.Location = new System.Drawing.Point(66, 6);
            this.chbChangePassword.Name = "chbChangePassword";
            this.chbChangePassword.Size = new System.Drawing.Size(15, 14);
            this.chbChangePassword.TabIndex = 2;
            this.chbChangePassword.UseVisualStyleBackColor = true;
            this.chbChangePassword.Visible = false;
            this.chbChangePassword.CheckedChanged += new System.EventHandler(this.chbChangePassword_CheckedChanged);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // usernameWorker
            // 
            this.usernameWorker.WorkerSupportsCancellation = true;
            this.usernameWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.usernameWorker_DoWork);
            this.usernameWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.usernameWorker_RunWorkerCompleted);
            // 
            // createWorker
            // 
            this.createWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.createWorker_DoWork);
            this.createWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.createWorker_RunWorkerCompleted);
            // 
            // editWorker
            // 
            this.editWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.editWorker_DoWork);
            this.editWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.editWorker_RunWorkerCompleted);
            // 
            // CreateAdministrator
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "CreateAdministrator";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Administrator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingImg)).EndInit();
            this.tableLayoutPassword.ResumeLayout(false);
            this.tableLayoutPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPasswordAgain;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.TextBox txtPasswordAgain;
        private System.Windows.Forms.Button btnClose;
        private System.ComponentModel.BackgroundWorker usernameWorker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox loadingImg;
        private System.ComponentModel.BackgroundWorker createWorker;
        private System.ComponentModel.BackgroundWorker editWorker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chbChangePassword;
        private System.Windows.Forms.ToolTip toolTip;
    }
}