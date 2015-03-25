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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbFromCountry
            // 
            this.cmbFromCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromCountry.FormattingEnabled = true;
            this.cmbFromCountry.Location = new System.Drawing.Point(90, 61);
            this.cmbFromCountry.Name = "cmbFromCountry";
            this.cmbFromCountry.Size = new System.Drawing.Size(170, 21);
            this.cmbFromCountry.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Country";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 91);
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
            this.cmbFromAirport.Location = new System.Drawing.Point(90, 88);
            this.cmbFromAirport.Name = "cmbFromAirport";
            this.cmbFromAirport.Size = new System.Drawing.Size(170, 21);
            this.cmbFromAirport.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Country";
            // 
            // cmbToCountry
            // 
            this.cmbToCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToCountry.FormattingEnabled = true;
            this.cmbToCountry.Location = new System.Drawing.Point(341, 61);
            this.cmbToCountry.Name = "cmbToCountry";
            this.cmbToCountry.Size = new System.Drawing.Size(170, 21);
            this.cmbToCountry.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 91);
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
            this.cmbToAirport.Location = new System.Drawing.Point(341, 88);
            this.cmbToAirport.Name = "cmbToAirport";
            this.cmbToAirport.Size = new System.Drawing.Size(170, 21);
            this.cmbToAirport.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(405, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "To:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Price";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // CreateRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
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
            this.Size = new System.Drawing.Size(626, 364);
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
        private System.Windows.Forms.TextBox textBox1;
    }
}
