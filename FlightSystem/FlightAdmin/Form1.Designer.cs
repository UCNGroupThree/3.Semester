using System.Drawing;
using System.Windows.Forms;

namespace FlightAdmin {
    partial class MainUI {
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cookiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lasseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPlane = new System.Windows.Forms.TabPage();
            this.planeTab1 = new FlightAdmin.GUI.PlaneTab();
            this.tabCustomer = new System.Windows.Forms.TabPage();
            this.customerTab1 = new FlightAdmin.GUI.CustomerTab();
            this.tabRoute = new System.Windows.Forms.TabPage();
            this.routeTab1 = new FlightAdmin.GUI.RouteTab();
            this.tabAirPort = new System.Windows.Forms.TabPage();
            this.airPortTab1 = new FlightAdmin.GUI.AirPortTab();
            this.tabFlight = new System.Windows.Forms.TabPage();
            this.flightTab1 = new FlightAdmin.GUI.FlightTab();
            this.tabReservation = new System.Windows.Forms.TabPage();
            this.reservationTab1 = new FlightAdmin.GUI.ReservationTab();
            this.tabAdministrator = new System.Windows.Forms.TabPage();
            this.administratorTab1 = new FlightAdmin.GUI.AdministratorTab();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPlane.SuspendLayout();
            this.tabCustomer.SuspendLayout();
            this.tabRoute.SuspendLayout();
            this.tabAirPort.SuspendLayout();
            this.tabFlight.SuspendLayout();
            this.tabReservation.SuspendLayout();
            this.tabAdministrator.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.unitTypeToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(841, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.cakeToolStripMenuItem,
            this.cookiesToolStripMenuItem,
            this.lasseToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // cakeToolStripMenuItem
            // 
            this.cakeToolStripMenuItem.Name = "cakeToolStripMenuItem";
            this.cakeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.cakeToolStripMenuItem.Text = "Cake";
            // 
            // cookiesToolStripMenuItem
            // 
            this.cookiesToolStripMenuItem.Name = "cookiesToolStripMenuItem";
            this.cookiesToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.cookiesToolStripMenuItem.Text = "Cookies";
            // 
            // lasseToolStripMenuItem
            // 
            this.lasseToolStripMenuItem.Name = "lasseToolStripMenuItem";
            this.lasseToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.lasseToolStripMenuItem.Text = "Lasse";
            // 
            // unitTypeToolStripMenuItem
            // 
            this.unitTypeToolStripMenuItem.Name = "unitTypeToolStripMenuItem";
            this.unitTypeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.unitTypeToolStripMenuItem.Text = "UnitType";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPlane);
            this.tabControl1.Controls.Add(this.tabCustomer);
            this.tabControl1.Controls.Add(this.tabRoute);
            this.tabControl1.Controls.Add(this.tabAirPort);
            this.tabControl1.Controls.Add(this.tabFlight);
            this.tabControl1.Controls.Add(this.tabReservation);
            this.tabControl1.Controls.Add(this.tabAdministrator);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(841, 443);
            this.tabControl1.TabIndex = 2;
            
            // 
            // tabPlane
            // 
            this.tabPlane.AutoScroll = true;
            this.tabPlane.Controls.Add(this.planeTab1);
            this.tabPlane.Location = new System.Drawing.Point(4, 22);
            this.tabPlane.Name = "tabPlane";
            this.tabPlane.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlane.Size = new System.Drawing.Size(833, 417);
            this.tabPlane.TabIndex = 0;
            this.tabPlane.Text = "Plane";
            this.tabPlane.UseVisualStyleBackColor = true;
            // 
            // planeTab1
            // 
            this.planeTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planeTab1.Location = new System.Drawing.Point(3, 3);
            this.planeTab1.Name = "planeTab1";
            this.planeTab1.Size = new System.Drawing.Size(827, 411);
            this.planeTab1.TabIndex = 0;
            // 
            // tabCustomer
            // 
            this.tabCustomer.Controls.Add(this.customerTab1);
            this.tabCustomer.Location = new System.Drawing.Point(4, 22);
            this.tabCustomer.Name = "tabCustomer";
            this.tabCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomer.Size = new System.Drawing.Size(833, 417);
            this.tabCustomer.TabIndex = 1;
            this.tabCustomer.Text = "Customer";
            this.tabCustomer.UseVisualStyleBackColor = true;
            // 
            // customerTab1
            // 
            this.customerTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerTab1.Location = new System.Drawing.Point(3, 3);
            this.customerTab1.Name = "customerTab1";
            this.customerTab1.Size = new System.Drawing.Size(827, 411);
            this.customerTab1.TabIndex = 0;
            // 
            // tabRoute
            // 
            this.tabRoute.Controls.Add(this.routeTab1);
            this.tabRoute.Location = new System.Drawing.Point(4, 22);
            this.tabRoute.Name = "tabRoute";
            this.tabRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoute.Size = new System.Drawing.Size(833, 417);
            this.tabRoute.TabIndex = 2;
            this.tabRoute.Text = "Route";
            this.tabRoute.UseVisualStyleBackColor = true;
            // 
            // routeTab1
            // 
            this.routeTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routeTab1.Location = new System.Drawing.Point(3, 3);
            this.routeTab1.Name = "routeTab1";
            this.routeTab1.Size = new System.Drawing.Size(827, 411);
            this.routeTab1.TabIndex = 0;
            // 
            // tabAirPort
            // 
            this.tabAirPort.Controls.Add(this.airPortTab1);
            this.tabAirPort.Location = new System.Drawing.Point(4, 22);
            this.tabAirPort.Name = "tabAirPort";
            this.tabAirPort.Padding = new System.Windows.Forms.Padding(3);
            this.tabAirPort.Size = new System.Drawing.Size(833, 417);
            this.tabAirPort.TabIndex = 3;
            this.tabAirPort.Text = "AirPort";
            this.tabAirPort.UseVisualStyleBackColor = true;
            // 
            // airPortTab1
            // 
            this.airPortTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.airPortTab1.Location = new System.Drawing.Point(3, 3);
            this.airPortTab1.Name = "airPortTab1";
            this.airPortTab1.Size = new System.Drawing.Size(827, 411);
            this.airPortTab1.TabIndex = 0;
            // 
            // tabFlight
            // 
            this.tabFlight.Controls.Add(this.flightTab1);
            this.tabFlight.Location = new System.Drawing.Point(4, 22);
            this.tabFlight.Name = "tabFlight";
            this.tabFlight.Padding = new System.Windows.Forms.Padding(3);
            this.tabFlight.Size = new System.Drawing.Size(833, 417);
            this.tabFlight.TabIndex = 4;
            this.tabFlight.Text = "Flight";
            this.tabFlight.UseVisualStyleBackColor = true;
            // 
            // flightTab1
            // 
            this.flightTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flightTab1.Location = new System.Drawing.Point(3, 3);
            this.flightTab1.Name = "flightTab1";
            this.flightTab1.Size = new System.Drawing.Size(827, 411);
            this.flightTab1.TabIndex = 0;
            // 
            // tabReservation
            // 
            this.tabReservation.Controls.Add(this.reservationTab1);
            this.tabReservation.Location = new System.Drawing.Point(4, 22);
            this.tabReservation.Name = "tabReservation";
            this.tabReservation.Padding = new System.Windows.Forms.Padding(3);
            this.tabReservation.Size = new System.Drawing.Size(833, 417);
            this.tabReservation.TabIndex = 5;
            this.tabReservation.Text = "Reservation";
            this.tabReservation.UseVisualStyleBackColor = true;
            // 
            // reservationTab1
            // 
            this.reservationTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reservationTab1.Location = new System.Drawing.Point(3, 3);
            this.reservationTab1.Name = "reservationTab1";
            this.reservationTab1.Size = new System.Drawing.Size(827, 411);
            this.reservationTab1.TabIndex = 0;
            // 
            // tabAdministrator
            // 
            this.tabAdministrator.Controls.Add(this.administratorTab1);
            this.tabAdministrator.Location = new System.Drawing.Point(0, 0);
            this.tabAdministrator.Name = "tabAdministrator";
            this.tabAdministrator.Padding = new System.Windows.Forms.Padding(0);
            this.tabAdministrator.Size = new System.Drawing.Size(833, 417);
            this.tabAdministrator.TabIndex = 6;
            this.tabAdministrator.Text = "Administrator";
            this.tabAdministrator.UseVisualStyleBackColor = true;
            // 
            // administratorTab1
            // 
            this.administratorTab1.BackColor = System.Drawing.SystemColors.Control;
            this.administratorTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.administratorTab1.Location = new System.Drawing.Point(3, 3);
            this.administratorTab1.Margin = new System.Windows.Forms.Padding(0);
            this.administratorTab1.Name = "administratorTab1";
            this.administratorTab1.Size = new System.Drawing.Size(827, 411);
            this.administratorTab1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 467);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(841, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Status";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 489);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Name = "MainUI";
            this.Text = "Flight Administration";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPlane.ResumeLayout(false);
            this.tabCustomer.ResumeLayout(false);
            this.tabRoute.ResumeLayout(false);
            this.tabAirPort.ResumeLayout(false);
            this.tabFlight.ResumeLayout(false);
            this.tabReservation.ResumeLayout(false);
            this.tabAdministrator.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cakeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cookiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lasseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitTypeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPlane;
        private System.Windows.Forms.TabPage tabCustomer;
        private System.Windows.Forms.TabPage tabRoute;
        private System.Windows.Forms.TabPage tabAirPort;
        private System.Windows.Forms.TabPage tabFlight;
        private System.Windows.Forms.TabPage tabReservation;
        private System.Windows.Forms.TabPage tabAdministrator;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private GUI.PlaneTab planeTab1;
        private GUI.CustomerTab customerTab1;
        private GUI.RouteTab routeTab1;
        private GUI.AirPortTab airPortTab1;
        private GUI.FlightTab flightTab1;
        private GUI.ReservationTab reservationTab1;
        private GUI.AdministratorTab administratorTab1;
    }
}

