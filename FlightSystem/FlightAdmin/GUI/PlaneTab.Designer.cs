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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grpPlaneSearch = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIDSearch = new System.Windows.Forms.Label();
            this.lblNameSearch = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPlanePassengers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spinnerPassengerCount = new System.Windows.Forms.NumericUpDown();
            this.txtID = new FlightAdmin.GUI.Helper.NumericTextBox();
            this.chkShowAllPlanes = new System.Windows.Forms.CheckBox();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.btnPlaneSearch = new System.Windows.Forms.Button();
            this.btnClearPlaneSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.grpCreatePlane = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreatePlane = new System.Windows.Forms.Button();
            this.planeTable = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planeTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planeBackgroundworker = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpPlaneSearch.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerPassengerCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.grpCreatePlane.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planeTable)).BeginInit();
            this.planeTableMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.planeTable, 1, 0);
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
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.7234F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.2766F));
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
            this.grpPlaneSearch.Size = new System.Drawing.Size(213, 253);
            this.grpPlaneSearch.TabIndex = 0;
            this.grpPlaneSearch.TabStop = false;
            this.grpPlaneSearch.Text = "Search Plane";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblIDSearch, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNameSearch, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblPlanePassengers, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.spinnerPassengerCount, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkShowAllPlanes, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.loadingImage, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.btnPlaneSearch, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.btnClearPlaneSearch, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 8;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(207, 234);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblIDSearch
            // 
            this.lblIDSearch.AutoSize = true;
            this.lblIDSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIDSearch.Location = new System.Drawing.Point(3, 0);
            this.lblIDSearch.Name = "lblIDSearch";
            this.lblIDSearch.Size = new System.Drawing.Size(117, 26);
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
            this.lblNameSearch.Size = new System.Drawing.Size(117, 26);
            this.lblNameSearch.TabIndex = 1;
            this.lblNameSearch.Text = "Name";
            this.lblNameSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(126, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(78, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
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
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Equal to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "as";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spinnerPassengerCount
            // 
            this.spinnerPassengerCount.AutoSize = true;
            this.spinnerPassengerCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinnerPassengerCount.Location = new System.Drawing.Point(126, 81);
            this.spinnerPassengerCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinnerPassengerCount.Name = "spinnerPassengerCount";
            this.spinnerPassengerCount.Size = new System.Drawing.Size(78, 20);
            this.spinnerPassengerCount.TabIndex = 7;
            this.spinnerPassengerCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinnerPassengerCount.ValueChanged += new System.EventHandler(this.spinnerPassengerCount_ValueChanged);
            // 
            // txtID
            // 
            this.txtID.AllowSpace = false;
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(126, 3);
            this.txtID.Name = "txtID";
            this.txtID.OnlyInt = false;
            this.txtID.Size = new System.Drawing.Size(78, 20);
            this.txtID.TabIndex = 12;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // chkShowAllPlanes
            // 
            this.chkShowAllPlanes.AutoSize = true;
            this.chkShowAllPlanes.Location = new System.Drawing.Point(3, 112);
            this.chkShowAllPlanes.Name = "chkShowAllPlanes";
            this.chkShowAllPlanes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowAllPlanes.Size = new System.Drawing.Size(102, 17);
            this.chkShowAllPlanes.TabIndex = 18;
            this.chkShowAllPlanes.Text = "Show All Planes";
            this.chkShowAllPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowAllPlanes.UseVisualStyleBackColor = true;
            this.chkShowAllPlanes.CheckedChanged += new System.EventHandler(this.chkShowAllPlanes_CheckedChanged);
            // 
            // loadingImage
            // 
            this.loadingImage.Image = global::FlightAdmin.Properties.Resources.loading;
            this.loadingImage.Location = new System.Drawing.Point(126, 112);
            this.loadingImage.Name = "loadingImage";
            this.loadingImage.Size = new System.Drawing.Size(78, 25);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingImage.TabIndex = 19;
            this.loadingImage.TabStop = false;
            this.loadingImage.Visible = false;
            // 
            // btnPlaneSearch
            // 
            this.btnPlaneSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaneSearch.Location = new System.Drawing.Point(126, 143);
            this.btnPlaneSearch.Name = "btnPlaneSearch";
            this.btnPlaneSearch.Size = new System.Drawing.Size(78, 25);
            this.btnPlaneSearch.TabIndex = 9;
            this.btnPlaneSearch.Text = "Search";
            this.btnPlaneSearch.UseVisualStyleBackColor = true;
            this.btnPlaneSearch.Click += new System.EventHandler(this.btnPlaneSearch_Click_1);
            // 
            // btnClearPlaneSearch
            // 
            this.btnClearPlaneSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPlaneSearch.Enabled = false;
            this.btnClearPlaneSearch.Location = new System.Drawing.Point(45, 143);
            this.btnClearPlaneSearch.Name = "btnClearPlaneSearch";
            this.btnClearPlaneSearch.Size = new System.Drawing.Size(75, 25);
            this.btnClearPlaneSearch.TabIndex = 10;
            this.btnClearPlaneSearch.Text = "Clear";
            this.btnClearPlaneSearch.UseVisualStyleBackColor = true;
            this.btnClearPlaneSearch.Click += new System.EventHandler(this.btnClearPlaneSearch_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Same or less";
            // 
            // grpCreatePlane
            // 
            this.grpCreatePlane.Controls.Add(this.tableLayoutPanel4);
            this.grpCreatePlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCreatePlane.Location = new System.Drawing.Point(3, 262);
            this.grpCreatePlane.Name = "grpCreatePlane";
            this.grpCreatePlane.Size = new System.Drawing.Size(213, 64);
            this.grpCreatePlane.TabIndex = 1;
            this.grpCreatePlane.TabStop = false;
            this.grpCreatePlane.Text = "Create Plane";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel4.Controls.Add(this.btnCreatePlane, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(207, 45);
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
            this.btnCreatePlane.Click += new System.EventHandler(this.btnCreatePlane_Click_1);
            // 
            // planeTable
            // 
            this.planeTable.AllowUserToAddRows = false;
            this.planeTable.AllowUserToResizeRows = false;
            this.planeTable.AutoGenerateColumns = false;
            this.planeTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.planeTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.planeTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.planeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.planeTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn});
            this.planeTable.ContextMenuStrip = this.planeTableMenu;
            this.planeTable.DataSource = this.planeBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.planeTable.DefaultCellStyle = dataGridViewCellStyle5;
            this.planeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planeTable.Location = new System.Drawing.Point(228, 3);
            this.planeTable.Name = "planeTable";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.planeTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.planeTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.planeTable.Size = new System.Drawing.Size(608, 347);
            this.planeTable.TabIndex = 1;
            this.planeTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.planeTable_KeyDown);
            this.planeTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.planeTable_MouseDown);
            // 
            // iDDataGridViewTextBoxColumn1
            // 
            this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn1.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // planeTableMenu
            // 
            this.planeTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.planeTableMenu.Name = "planeTableMenu";
            this.planeTableMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // planeBindingSource
            // 
            this.planeBindingSource.DataSource = typeof(FlightAdmin.MainService.Plane);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // Seats
            // 
            this.Seats.HeaderText = "Seats";
            this.Seats.Name = "Seats";
            this.Seats.ReadOnly = true;
            // 
            // planeBackgroundworker
            // 
            this.planeBackgroundworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.planeBackgroundworker_DoWork);
            this.planeBackgroundworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.planeBackgroundworker_RunWorkerCompleted);
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
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.grpCreatePlane.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.planeTable)).EndInit();
            this.planeTableMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.planeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox grpPlaneSearch;
        private System.Windows.Forms.DataGridView planeTable;
        private System.Windows.Forms.GroupBox grpCreatePlane;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnClearPlaneSearch;
        private System.Windows.Forms.BindingSource planeBindingSource;
        private System.Windows.Forms.Button btnCreatePlane;
        private System.ComponentModel.BackgroundWorker planeBackgroundworker;
        private System.Windows.Forms.ContextMenuStrip planeTableMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblIDSearch;
        private System.Windows.Forms.Label lblNameSearch;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPlanePassengers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown spinnerPassengerCount;
        private Helper.NumericTextBox txtID;
        private System.Windows.Forms.Button btnPlaneSearch;
        private System.Windows.Forms.CheckBox chkShowAllPlanes;
        private System.Windows.Forms.PictureBox loadingImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seats;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label3;
    }
}
