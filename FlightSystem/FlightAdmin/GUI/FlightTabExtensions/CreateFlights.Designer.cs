namespace FlightAdmin.GUI.FlightTabExtensions {
    partial class CreateFlights {
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
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.loadingPicture = new System.Windows.Forms.PictureBox();
            this.flightPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.selectionPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddFlight = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtInterval = new FlightAdmin.GUI.Helper.NumericTextBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.epFlights = new System.Windows.Forms.ErrorProvider(this.components);
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).BeginInit();
            this.flightPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.selectionPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epFlights)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.loadingPicture);
            this.loadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingPanel.Location = new System.Drawing.Point(0, 0);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(521, 165);
            this.loadingPanel.TabIndex = 0;
            // 
            // loadingPicture
            // 
            this.loadingPicture.Image = global::FlightAdmin.Properties.Resources.image_836585;
            this.loadingPicture.Location = new System.Drawing.Point(247, 63);
            this.loadingPicture.Name = "loadingPicture";
            this.loadingPicture.Size = new System.Drawing.Size(131, 126);
            this.loadingPicture.TabIndex = 0;
            this.loadingPicture.TabStop = false;
            // 
            // flightPanel
            // 
            this.flightPanel.Controls.Add(this.tableLayoutPanel1);
            this.flightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flightPanel.Location = new System.Drawing.Point(0, 0);
            this.flightPanel.Name = "flightPanel";
            this.flightPanel.Size = new System.Drawing.Size(521, 165);
            this.flightPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 465F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.selectionPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 165);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // selectionPanel
            // 
            this.selectionPanel.AutoScroll = true;
            this.selectionPanel.Controls.Add(this.label3);
            this.selectionPanel.Controls.Add(this.label2);
            this.selectionPanel.Controls.Add(this.label1);
            this.selectionPanel.Controls.Add(this.btnAddFlight);
            this.selectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectionPanel.Location = new System.Drawing.Point(0, 0);
            this.selectionPanel.Margin = new System.Windows.Forms.Padding(0);
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Size = new System.Drawing.Size(465, 142);
            this.selectionPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Plane";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ArrivalTime";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "DepartureTime";
            // 
            // btnAddFlight
            // 
            this.btnAddFlight.Location = new System.Drawing.Point(421, 14);
            this.btnAddFlight.Name = "btnAddFlight";
            this.btnAddFlight.Size = new System.Drawing.Size(21, 21);
            this.btnAddFlight.TabIndex = 7;
            this.btnAddFlight.Text = "+";
            this.btnAddFlight.UseVisualStyleBackColor = true;
            this.btnAddFlight.Click += new System.EventHandler(this.btnAddFlight_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtInterval);
            this.panel2.Controls.Add(this.lblInterval);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(465, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(56, 142);
            this.panel2.TabIndex = 1;
            // 
            // txtInterval
            // 
            this.txtInterval.AllowSpace = false;
            this.txtInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterval.Location = new System.Drawing.Point(0, 14);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.OnlyInt = false;
            this.txtInterval.Size = new System.Drawing.Size(56, 20);
            this.txtInterval.TabIndex = 1;
            this.txtInterval.Text = "0";
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(0, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(45, 13);
            this.lblInterval.TabIndex = 0;
            this.lblInterval.Text = "Interval:";
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 142);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(521, 23);
            this.panel3.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(263, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveForCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Location = new System.Drawing.Point(183, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // epFlights
            // 
            this.epFlights.ContainerControl = this;
            // 
            // CreateFlights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 165);
            this.ControlBox = false;
            this.Controls.Add(this.flightPanel);
            this.Controls.Add(this.loadingPanel);
            this.Name = "CreateFlights";
            this.Text = "Create Flights";
            this.Load += new System.EventHandler(this.CreateFlights_Load);
            this.loadingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).EndInit();
            this.flightPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.selectionPanel.ResumeLayout(false);
            this.selectionPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epFlights)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.Panel flightPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel selectionPanel;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddFlight;
        private System.Windows.Forms.Panel panel2;
        private Helper.NumericTextBox txtInterval;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox loadingPicture;
        public System.Windows.Forms.ErrorProvider epFlights;
    }
}
