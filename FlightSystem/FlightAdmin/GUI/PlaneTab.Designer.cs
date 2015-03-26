namespace FlightAdmin.GUI {
    partial class PlaneTab {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grpPlaneSearch = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIDSearch = new System.Windows.Forms.Label();
            this.lblNameSearch = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtNameSearch = new System.Windows.Forms.TextBox();
            this.lblPlanePassengers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spinnerPassengerCount = new System.Windows.Forms.NumericUpDown();
            this.comboPassengerCountChoice = new System.Windows.Forms.ComboBox();
            this.btnPlaneSearch = new System.Windows.Forms.Button();
            this.btnClearPlaneSearch = new System.Windows.Forms.Button();
            this.grpCreatePlane = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreatePlane = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpPlaneSearch.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerPassengerCount)).BeginInit();
            this.grpCreatePlane.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 353);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.grpPlaneSearch, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grpCreatePlane, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.77419F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.22581F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(219, 347);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // grpPlaneSearch
            // 
            this.grpPlaneSearch.Controls.Add(this.tableLayoutPanel3);
            this.grpPlaneSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPlaneSearch.Location = new System.Drawing.Point(3, 3);
            this.grpPlaneSearch.Name = "grpPlaneSearch";
            this.grpPlaneSearch.Size = new System.Drawing.Size(213, 214);
            this.grpPlaneSearch.TabIndex = 0;
            this.grpPlaneSearch.TabStop = false;
            this.grpPlaneSearch.Text = "Search Plane";
            this.grpPlaneSearch.Enter += new System.EventHandler(this.grpPlaneSearch_Enter);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblIDSearch, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNameSearch, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtNameSearch, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblPlanePassengers, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.spinnerPassengerCount, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.comboPassengerCountChoice, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnPlaneSearch, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.btnClearPlaneSearch, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(207, 195);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblIDSearch
            // 
            this.lblIDSearch.AutoSize = true;
            this.lblIDSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIDSearch.Location = new System.Drawing.Point(3, 0);
            this.lblIDSearch.Name = "lblIDSearch";
            this.lblIDSearch.Size = new System.Drawing.Size(75, 26);
            this.lblIDSearch.TabIndex = 0;
            this.lblIDSearch.Text = "ID";
            this.lblIDSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameSearch
            // 
            this.lblNameSearch.AutoSize = true;
            this.lblNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNameSearch.Location = new System.Drawing.Point(3, 26);
            this.lblNameSearch.Name = "lblNameSearch";
            this.lblNameSearch.Size = new System.Drawing.Size(75, 26);
            this.lblNameSearch.TabIndex = 1;
            this.lblNameSearch.Text = "Name";
            this.lblNameSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(84, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(120, 20);
            this.txtID.TabIndex = 2;
            // 
            // txtNameSearch
            // 
            this.txtNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNameSearch.Location = new System.Drawing.Point(84, 29);
            this.txtNameSearch.Name = "txtNameSearch";
            this.txtNameSearch.Size = new System.Drawing.Size(120, 20);
            this.txtNameSearch.TabIndex = 3;
            // 
            // lblPlanePassengers
            // 
            this.lblPlanePassengers.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblPlanePassengers, 2);
            this.lblPlanePassengers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanePassengers.Location = new System.Drawing.Point(3, 52);
            this.lblPlanePassengers.Name = "lblPlanePassengers";
            this.lblPlanePassengers.Size = new System.Drawing.Size(201, 13);
            this.lblPlanePassengers.TabIndex = 4;
            this.lblPlanePassengers.Text = "Passengers";
            this.lblPlanePassengers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Equal to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "as";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spinnerPassengerCount
            // 
            this.spinnerPassengerCount.AutoSize = true;
            this.spinnerPassengerCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinnerPassengerCount.Location = new System.Drawing.Point(84, 95);
            this.spinnerPassengerCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinnerPassengerCount.Name = "spinnerPassengerCount";
            this.spinnerPassengerCount.Size = new System.Drawing.Size(120, 20);
            this.spinnerPassengerCount.TabIndex = 7;
            this.spinnerPassengerCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboPassengerCountChoice
            // 
            this.comboPassengerCountChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboPassengerCountChoice.FormattingEnabled = true;
            this.comboPassengerCountChoice.Items.AddRange(new object[] {
            "less and same",
            "same ",
            "more and same"});
            this.comboPassengerCountChoice.Location = new System.Drawing.Point(84, 68);
            this.comboPassengerCountChoice.Name = "comboPassengerCountChoice";
            this.comboPassengerCountChoice.Size = new System.Drawing.Size(120, 21);
            this.comboPassengerCountChoice.TabIndex = 1;
            // 
            // btnPlaneSearch
            // 
            this.btnPlaneSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaneSearch.Location = new System.Drawing.Point(84, 126);
            this.btnPlaneSearch.Name = "btnPlaneSearch";
            this.btnPlaneSearch.Size = new System.Drawing.Size(120, 26);
            this.btnPlaneSearch.TabIndex = 9;
            this.btnPlaneSearch.Text = "Search";
            this.btnPlaneSearch.UseVisualStyleBackColor = true;
            // 
            // btnClearPlaneSearch
            // 
            this.btnClearPlaneSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPlaneSearch.Location = new System.Drawing.Point(3, 126);
            this.btnClearPlaneSearch.Name = "btnClearPlaneSearch";
            this.btnClearPlaneSearch.Size = new System.Drawing.Size(75, 26);
            this.btnClearPlaneSearch.TabIndex = 10;
            this.btnClearPlaneSearch.Text = "Clear";
            this.btnClearPlaneSearch.UseVisualStyleBackColor = true;
            this.btnClearPlaneSearch.Click += new System.EventHandler(this.btnClearPlaneSearch_Click);
            // 
            // grpCreatePlane
            // 
            this.grpCreatePlane.Controls.Add(this.tableLayoutPanel4);
            this.grpCreatePlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCreatePlane.Location = new System.Drawing.Point(3, 223);
            this.grpCreatePlane.Name = "grpCreatePlane";
            this.grpCreatePlane.Size = new System.Drawing.Size(213, 103);
            this.grpCreatePlane.TabIndex = 1;
            this.grpCreatePlane.TabStop = false;
            this.grpCreatePlane.Text = "Create Plane";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btnCreatePlane, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(207, 84);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnCreatePlane
            // 
            this.btnCreatePlane.Location = new System.Drawing.Point(23, 3);
            this.btnCreatePlane.Name = "btnCreatePlane";
            this.btnCreatePlane.Size = new System.Drawing.Size(75, 23);
            this.btnCreatePlane.TabIndex = 0;
            this.btnCreatePlane.Text = "Create";
            this.btnCreatePlane.UseVisualStyleBackColor = true;
            this.btnCreatePlane.Click += new System.EventHandler(this.btnCreatePlane_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.planeBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(228, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(608, 347);
            this.dataGridView1.TabIndex = 1;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // planeBindingSource
            // 
            this.planeBindingSource.DataSource = typeof(FlightAdmin.MainService.Plane);
            // 
            // PlaneTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PlaneTab";
            this.Size = new System.Drawing.Size(839, 353);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.grpPlaneSearch.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerPassengerCount)).EndInit();
            this.grpCreatePlane.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox grpPlaneSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grpCreatePlane;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblIDSearch;
        private System.Windows.Forms.Label lblNameSearch;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtNameSearch;
        private System.Windows.Forms.Label lblPlanePassengers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown spinnerPassengerCount;
        private System.Windows.Forms.ComboBox comboPassengerCountChoice;
        private System.Windows.Forms.Button btnPlaneSearch;
        private System.Windows.Forms.Button btnClearPlaneSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource planeBindingSource;
        private System.Windows.Forms.Button btnCreatePlane;
    }
}
