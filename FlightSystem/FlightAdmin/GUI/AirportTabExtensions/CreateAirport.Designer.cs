namespace FlightAdmin.GUI.AirportTabExtensions {
    partial class CreateAirport {
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
            this.lblShortName = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.lblLongtitude = new System.Windows.Forms.Label();
            this.lblAlittude = new System.Windows.Forms.Label();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.txtLatitude = new FlightAdmin.GUI.Helper.NumericTextBox();
            this.txtLongtitude = new FlightAdmin.GUI.Helper.NumericTextBox();
            this.txtAltitude = new FlightAdmin.GUI.Helper.NumericTextBox();
            this.txtTimeZone = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblShortName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCity, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCountry, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblLatitude, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtShortName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLongtitude, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblAlittude, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblTimeZone, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCreate, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtCity, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCountry, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtLatitude, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtLongtitude, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtAltitude, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtTimeZone, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 282);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHeader.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHeader, 3);
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(76, 3);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(3);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(122, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Create Airport";
            // 
            // lblShortName
            // 
            this.lblShortName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShortName.AutoSize = true;
            this.lblShortName.Location = new System.Drawing.Point(18, 32);
            this.lblShortName.Name = "lblShortName";
            this.lblShortName.Size = new System.Drawing.Size(60, 13);
            this.lblShortName.TabIndex = 1;
            this.lblShortName.Text = "ShortName";
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(18, 58);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblCity
            // 
            this.lblCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(18, 84);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(24, 13);
            this.lblCity.TabIndex = 1;
            this.lblCity.Text = "City";
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(18, 110);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(43, 13);
            this.lblCountry.TabIndex = 1;
            this.lblCountry.Text = "Country";
            // 
            // lblLatitude
            // 
            this.lblLatitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Location = new System.Drawing.Point(18, 136);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(45, 13);
            this.lblLatitude.TabIndex = 1;
            this.lblLatitude.Text = "Latitude";
            // 
            // txtShortName
            // 
            this.txtShortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortName.Location = new System.Drawing.Point(108, 29);
            this.txtShortName.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(146, 20);
            this.txtShortName.TabIndex = 2;
            this.txtShortName.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            this.txtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.txtShortName_Validating);
            // 
            // lblLongtitude
            // 
            this.lblLongtitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLongtitude.AutoSize = true;
            this.lblLongtitude.Location = new System.Drawing.Point(18, 162);
            this.lblLongtitude.Name = "lblLongtitude";
            this.lblLongtitude.Size = new System.Drawing.Size(57, 13);
            this.lblLongtitude.TabIndex = 1;
            this.lblLongtitude.Text = "Longtitude";
            // 
            // lblAlittude
            // 
            this.lblAlittude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAlittude.AutoSize = true;
            this.lblAlittude.Location = new System.Drawing.Point(18, 188);
            this.lblAlittude.Name = "lblAlittude";
            this.lblAlittude.Size = new System.Drawing.Size(42, 13);
            this.lblAlittude.TabIndex = 1;
            this.lblAlittude.Text = "Altitude";
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(18, 214);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(55, 13);
            this.lblTimeZone.TabIndex = 1;
            this.lblTimeZone.Text = "TimeZone";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(108, 55);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(146, 20);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(179, 242);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.Location = new System.Drawing.Point(108, 81);
            this.txtCity.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(146, 20);
            this.txtCity.TabIndex = 2;
            this.txtCity.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // txtCountry
            // 
            this.txtCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCountry.Location = new System.Drawing.Point(108, 107);
            this.txtCountry.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(146, 20);
            this.txtCountry.TabIndex = 2;
            this.txtCountry.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // txtLatitude
            // 
            this.txtLatitude.AllowSpace = false;
            this.txtLatitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatitude.Location = new System.Drawing.Point(108, 133);
            this.txtLatitude.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.OnlyInt = false;
            this.txtLatitude.Size = new System.Drawing.Size(146, 20);
            this.txtLatitude.TabIndex = 4;
            this.txtLatitude.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // txtLongtitude
            // 
            this.txtLongtitude.AllowSpace = false;
            this.txtLongtitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLongtitude.Location = new System.Drawing.Point(108, 159);
            this.txtLongtitude.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtLongtitude.Name = "txtLongtitude";
            this.txtLongtitude.OnlyInt = false;
            this.txtLongtitude.Size = new System.Drawing.Size(146, 20);
            this.txtLongtitude.TabIndex = 4;
            this.txtLongtitude.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // txtAltitude
            // 
            this.txtAltitude.AllowSpace = false;
            this.txtAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAltitude.Location = new System.Drawing.Point(108, 185);
            this.txtAltitude.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtAltitude.Name = "txtAltitude";
            this.txtAltitude.OnlyInt = true;
            this.txtAltitude.Size = new System.Drawing.Size(146, 20);
            this.txtAltitude.TabIndex = 4;
            this.txtAltitude.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeZone.Location = new System.Drawing.Point(108, 211);
            this.txtTimeZone.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.Size = new System.Drawing.Size(146, 20);
            this.txtTimeZone.TabIndex = 2;
            this.txtTimeZone.TextChanged += new System.EventHandler(this.txtRemoveErrorOn_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(18, 242);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateAirport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 292);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 330);
            this.Name = "CreateAirport";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Airport";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblShortName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.TextBox txtShortName;
        private System.Windows.Forms.Label lblLongtitude;
        private System.Windows.Forms.Label lblAlittude;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtCountry;
        private Helper.NumericTextBox txtLatitude;
        private Helper.NumericTextBox txtLongtitude;
        private Helper.NumericTextBox txtAltitude;
        private System.Windows.Forms.TextBox txtTimeZone;
        private System.Windows.Forms.Button btnClose;
    }
}