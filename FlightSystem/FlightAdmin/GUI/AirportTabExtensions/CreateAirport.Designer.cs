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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 6);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 346);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHeader.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHeader, 3);
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(123, 4);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(122, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Create Airport";
            // 
            // lblShortName
            // 
            this.lblShortName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShortName.AutoSize = true;
            this.lblShortName.Location = new System.Drawing.Point(24, 35);
            this.lblShortName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShortName.Name = "lblShortName";
            this.lblShortName.Size = new System.Drawing.Size(76, 16);
            this.lblShortName.TabIndex = 1;
            this.lblShortName.Text = "ShortName";
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 65);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblCity
            // 
            this.lblCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(24, 95);
            this.lblCity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(30, 16);
            this.lblCity.TabIndex = 1;
            this.lblCity.Text = "City";
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(24, 125);
            this.lblCountry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(53, 16);
            this.lblCountry.TabIndex = 1;
            this.lblCountry.Text = "Country";
            // 
            // lblLatitude
            // 
            this.lblLatitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Location = new System.Drawing.Point(24, 155);
            this.lblLatitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(55, 16);
            this.lblLatitude.TabIndex = 1;
            this.lblLatitude.Text = "Latitude";
            // 
            // txtShortName
            // 
            this.txtShortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortName.Location = new System.Drawing.Point(144, 32);
            this.txtShortName.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(197, 22);
            this.txtShortName.TabIndex = 2;
            // 
            // lblLongtitude
            // 
            this.lblLongtitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLongtitude.AutoSize = true;
            this.lblLongtitude.Location = new System.Drawing.Point(24, 185);
            this.lblLongtitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLongtitude.Name = "lblLongtitude";
            this.lblLongtitude.Size = new System.Drawing.Size(70, 16);
            this.lblLongtitude.TabIndex = 1;
            this.lblLongtitude.Text = "Longtitude";
            // 
            // lblAlittude
            // 
            this.lblAlittude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAlittude.AutoSize = true;
            this.lblAlittude.Location = new System.Drawing.Point(24, 215);
            this.lblAlittude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlittude.Name = "lblAlittude";
            this.lblAlittude.Size = new System.Drawing.Size(52, 16);
            this.lblAlittude.TabIndex = 1;
            this.lblAlittude.Text = "Altitude";
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(24, 245);
            this.lblTimeZone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(70, 16);
            this.lblTimeZone.TabIndex = 1;
            this.lblTimeZone.Text = "TimeZone";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(144, 62);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(197, 22);
            this.txtName.TabIndex = 2;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(241, 278);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4, 10, 27, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 28);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.Location = new System.Drawing.Point(144, 92);
            this.txtCity.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(197, 22);
            this.txtCity.TabIndex = 2;
            // 
            // txtCountry
            // 
            this.txtCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCountry.Location = new System.Drawing.Point(144, 122);
            this.txtCountry.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(197, 22);
            this.txtCountry.TabIndex = 2;
            // 
            // txtLatitude
            // 
            this.txtLatitude.AllowSpace = false;
            this.txtLatitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatitude.Location = new System.Drawing.Point(144, 152);
            this.txtLatitude.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.OnlyInt = false;
            this.txtLatitude.Size = new System.Drawing.Size(197, 22);
            this.txtLatitude.TabIndex = 4;
            // 
            // txtLongtitude
            // 
            this.txtLongtitude.AllowSpace = false;
            this.txtLongtitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLongtitude.Location = new System.Drawing.Point(144, 182);
            this.txtLongtitude.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtLongtitude.Name = "txtLongtitude";
            this.txtLongtitude.OnlyInt = false;
            this.txtLongtitude.Size = new System.Drawing.Size(197, 22);
            this.txtLongtitude.TabIndex = 4;
            // 
            // txtAltitude
            // 
            this.txtAltitude.AllowSpace = false;
            this.txtAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAltitude.Location = new System.Drawing.Point(144, 212);
            this.txtAltitude.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtAltitude.Name = "txtAltitude";
            this.txtAltitude.OnlyInt = true;
            this.txtAltitude.Size = new System.Drawing.Size(197, 22);
            this.txtAltitude.TabIndex = 4;
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeZone.Location = new System.Drawing.Point(144, 242);
            this.txtTimeZone.Margin = new System.Windows.Forms.Padding(4, 4, 27, 4);
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.Size = new System.Drawing.Size(197, 22);
            this.txtTimeZone.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(24, 278);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 10, 27, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateAirport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 358);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(661, 397);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 397);
            this.Name = "CreateAirport";
            this.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
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