using FlightAdmin.GUI.Helper;

namespace FlightAdmin.GUI.RouteTabExtensions {
    partial class CreateRoute {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.cmbFromCountry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFromAirport = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbToCountry = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbToAirport = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.loadToAirport = new System.Windows.Forms.PictureBox();
            this.loadToCountry = new System.Windows.Forms.PictureBox();
            this.loadFromAirport = new System.Windows.Forms.PictureBox();
            this.loadFromCountry = new System.Windows.Forms.PictureBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.epRoute = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtPrice = new FlightAdmin.GUI.Helper.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadToAirport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadToCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFromAirport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFromCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFromCountry
            // 
            this.cmbFromCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromCountry.FormattingEnabled = true;
            this.cmbFromCountry.Location = new System.Drawing.Point(53, 21);
            this.cmbFromCountry.Name = "cmbFromCountry";
            this.cmbFromCountry.Size = new System.Drawing.Size(170, 21);
            this.cmbFromCountry.TabIndex = 0;
            this.cmbFromCountry.SelectedIndexChanged += new System.EventHandler(this.cmbFromCountry_SelectedIndexChanged);
            this.cmbFromCountry.Enter += new System.EventHandler(this.On_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Country";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Airport";
            // 
            // cmbFromAirport
            // 
            this.cmbFromAirport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromAirport.Enabled = false;
            this.cmbFromAirport.FormattingEnabled = true;
            this.cmbFromAirport.Location = new System.Drawing.Point(53, 48);
            this.cmbFromAirport.Name = "cmbFromAirport";
            this.cmbFromAirport.Size = new System.Drawing.Size(170, 21);
            this.cmbFromAirport.TabIndex = 2;
            this.cmbFromAirport.Enter += new System.EventHandler(this.On_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Country";
            // 
            // cmbToCountry
            // 
            this.cmbToCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToCountry.FormattingEnabled = true;
            this.cmbToCountry.Location = new System.Drawing.Point(304, 21);
            this.cmbToCountry.Name = "cmbToCountry";
            this.cmbToCountry.Size = new System.Drawing.Size(170, 21);
            this.cmbToCountry.TabIndex = 4;
            this.cmbToCountry.SelectedIndexChanged += new System.EventHandler(this.cmbToCountry_SelectedIndexChanged);
            this.cmbToCountry.Enter += new System.EventHandler(this.On_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Airport";
            // 
            // cmbToAirport
            // 
            this.cmbToAirport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToAirport.Enabled = false;
            this.cmbToAirport.FormattingEnabled = true;
            this.cmbToAirport.Location = new System.Drawing.Point(304, 48);
            this.cmbToAirport.Name = "cmbToAirport";
            this.cmbToAirport.Size = new System.Drawing.Size(170, 21);
            this.cmbToAirport.TabIndex = 6;
            this.cmbToAirport.Enter += new System.EventHandler(this.On_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "To:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Price";
            // 
            // loadToAirport
            // 
            this.loadToAirport.Image = global::FlightAdmin.Properties.Resources.loading1;
            this.loadToAirport.Location = new System.Drawing.Point(477, 48);
            this.loadToAirport.Name = "loadToAirport";
            this.loadToAirport.Size = new System.Drawing.Size(20, 20);
            this.loadToAirport.TabIndex = 15;
            this.loadToAirport.TabStop = false;
            this.loadToAirport.Visible = false;
            // 
            // loadToCountry
            // 
            this.loadToCountry.Image = global::FlightAdmin.Properties.Resources.loading1;
            this.loadToCountry.Location = new System.Drawing.Point(477, 21);
            this.loadToCountry.Name = "loadToCountry";
            this.loadToCountry.Size = new System.Drawing.Size(20, 20);
            this.loadToCountry.TabIndex = 14;
            this.loadToCountry.TabStop = false;
            this.loadToCountry.Visible = false;
            // 
            // loadFromAirport
            // 
            this.loadFromAirport.Image = global::FlightAdmin.Properties.Resources.loading1;
            this.loadFromAirport.Location = new System.Drawing.Point(226, 48);
            this.loadFromAirport.Name = "loadFromAirport";
            this.loadFromAirport.Size = new System.Drawing.Size(20, 20);
            this.loadFromAirport.TabIndex = 13;
            this.loadFromAirport.TabStop = false;
            this.loadFromAirport.Visible = false;
            // 
            // loadFromCountry
            // 
            this.loadFromCountry.Image = global::FlightAdmin.Properties.Resources.loading1;
            this.loadFromCountry.Location = new System.Drawing.Point(226, 21);
            this.loadFromCountry.Name = "loadFromCountry";
            this.loadFromCountry.Size = new System.Drawing.Size(20, 20);
            this.loadFromCountry.TabIndex = 12;
            this.loadFromCountry.TabStop = false;
            this.loadFromCountry.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(171, 105);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 16;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(269, 105);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // epRoute
            // 
            this.epRoute.ContainerControl = this;
            // 
            // txtPrice
            // 
            this.txtPrice.AllowSpace = false;
            this.txtPrice.Location = new System.Drawing.Point(53, 75);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(170, 20);
            this.txtPrice.TabIndex = 11;
            this.txtPrice.Enter += new System.EventHandler(this.On_Enter);
            // 
            // CreateRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.loadToAirport);
            this.Controls.Add(this.loadToCountry);
            this.Controls.Add(this.loadFromAirport);
            this.Controls.Add(this.loadFromCountry);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbToAirport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbToCountry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFromAirport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFromCountry);
            this.Name = "CreateRoute";
            this.Size = new System.Drawing.Size(505, 135);
            this.Load += new System.EventHandler(this.CreateRoute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loadToAirport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadToCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFromAirport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadFromCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epRoute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFromCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFromAirport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbToCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbToAirport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox loadFromCountry;
        private System.Windows.Forms.PictureBox loadFromAirport;
        private System.Windows.Forms.PictureBox loadToCountry;
        private System.Windows.Forms.PictureBox loadToAirport;
        private NumericTextBox txtPrice;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider epRoute;
    }
}
