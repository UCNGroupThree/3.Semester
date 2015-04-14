namespace FlightAdmin.GUI.PlaneTabExtensions
{
    partial class CreatePlane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.spinnerSeats = new System.Windows.Forms.NumericUpDown();
            this.btnCancelNewPlane = new System.Windows.Forms.Button();
            this.btnCreateNewPlane = new System.Windows.Forms.Button();
            this.epPlane = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epPlane)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.05578F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.94422F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.spinnerSeats, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelNewPlane, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCreateNewPlane, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 112);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 33);
            this.label2.TabIndex = 1;
            this.label2.Text = "Seats";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(135, 5);
            this.txtName.Margin = new System.Windows.Forms.Padding(5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(222, 20);
            this.txtName.TabIndex = 2;
            // 
            // spinnerSeats
            // 
            this.spinnerSeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinnerSeats.Location = new System.Drawing.Point(135, 33);
            this.spinnerSeats.Margin = new System.Windows.Forms.Padding(5);
            this.spinnerSeats.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spinnerSeats.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinnerSeats.Name = "spinnerSeats";
            this.spinnerSeats.Size = new System.Drawing.Size(222, 20);
            this.spinnerSeats.TabIndex = 3;
            this.spinnerSeats.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancelNewPlane
            // 
            this.btnCancelNewPlane.Location = new System.Drawing.Point(3, 64);
            this.btnCancelNewPlane.Name = "btnCancelNewPlane";
            this.btnCancelNewPlane.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewPlane.TabIndex = 4;
            this.btnCancelNewPlane.Text = "Cancel";
            this.btnCancelNewPlane.UseVisualStyleBackColor = true;
            this.btnCancelNewPlane.Click += new System.EventHandler(this.btnCancelNewPlane_Click);
            // 
            // btnCreateNewPlane
            // 
            this.btnCreateNewPlane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNewPlane.Location = new System.Drawing.Point(284, 64);
            this.btnCreateNewPlane.Name = "btnCreateNewPlane";
            this.btnCreateNewPlane.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNewPlane.TabIndex = 5;
            this.btnCreateNewPlane.Text = "Create";
            this.btnCreateNewPlane.UseVisualStyleBackColor = true;
            this.btnCreateNewPlane.Click += new System.EventHandler(this.btnCreateNewPlane_Click);
            // 
            // epPlane
            // 
            this.epPlane.ContainerControl = this;
            // 
            // CreatePlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 112);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CreatePlane";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Create New Plane";
            this.Load += new System.EventHandler(this.CreatePlane_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epPlane)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown spinnerSeats;
        private System.Windows.Forms.Button btnCancelNewPlane;
        private System.Windows.Forms.Button btnCreateNewPlane;
        private System.Windows.Forms.ErrorProvider epPlane;
    }
}