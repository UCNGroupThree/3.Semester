namespace FlightAdmin.GUI {
    partial class FlightTab {
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
            this.createFlight1 = new FlightAdmin.GUI.FlightTabExtensions.CreateFlight();
            this.SuspendLayout();
            // 
            // createFlight1
            // 
            this.createFlight1.Location = new System.Drawing.Point(0, 0);
            this.createFlight1.Name = "createFlight1";
            this.createFlight1.Size = new System.Drawing.Size(870, 398);
            this.createFlight1.TabIndex = 0;
            // 
            // FlightTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createFlight1);
            this.Name = "FlightTab";
            this.Size = new System.Drawing.Size(771, 354);
            this.ResumeLayout(false);

        }

        #endregion

        private FlightTabExtensions.CreateFlight createFlight1;
    }
}
