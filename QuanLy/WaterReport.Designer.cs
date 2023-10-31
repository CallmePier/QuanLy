namespace QuanLy
{
    partial class WaterReport
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
            this.rptWater = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTaoReport = new System.Windows.Forms.Button();
            this.dtpkCheckOut = new System.Windows.Forms.DateTimePicker();
            this.dtpkCheckIn = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rptWater
            // 
            this.rptWater.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.rptWater.LocalReport.ReportEmbeddedResource = "QuanLy.ReportWater.waterReport.rdlc";
            this.rptWater.Location = new System.Drawing.Point(0, 159);
            this.rptWater.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rptWater.Name = "rptWater";
            this.rptWater.ServerReport.BearerToken = null;
            this.rptWater.Size = new System.Drawing.Size(1387, 686);
            this.rptWater.TabIndex = 0;
            this.rptWater.Load += new System.EventHandler(this.rptWater_Load);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.groupBox1.Controls.Add(this.btnTaoReport);
            this.groupBox1.Controls.Add(this.dtpkCheckOut);
            this.groupBox1.Controls.Add(this.dtpkCheckIn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1379, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN REPORT";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnTaoReport
            // 
            this.btnTaoReport.BackColor = System.Drawing.Color.Aqua;
            this.btnTaoReport.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTaoReport.Location = new System.Drawing.Point(537, 42);
            this.btnTaoReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTaoReport.Name = "btnTaoReport";
            this.btnTaoReport.Size = new System.Drawing.Size(144, 43);
            this.btnTaoReport.TabIndex = 2;
            this.btnTaoReport.Text = "Tạo Report";
            this.btnTaoReport.UseVisualStyleBackColor = false;
            this.btnTaoReport.Click += new System.EventHandler(this.btnTaoReport_Click);
            // 
            // dtpkCheckOut
            // 
            this.dtpkCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkCheckOut.Location = new System.Drawing.Point(759, 58);
            this.dtpkCheckOut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpkCheckOut.Name = "dtpkCheckOut";
            this.dtpkCheckOut.Size = new System.Drawing.Size(336, 26);
            this.dtpkCheckOut.TabIndex = 1;
            // 
            // dtpkCheckIn
            // 
            this.dtpkCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkCheckIn.Location = new System.Drawing.Point(144, 58);
            this.dtpkCheckIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpkCheckIn.Name = "dtpkCheckIn";
            this.dtpkCheckIn.Size = new System.Drawing.Size(257, 26);
            this.dtpkCheckIn.TabIndex = 0;
            // 
            // WaterReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 846);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rptWater);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WaterReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WaterReport";
            this.Load += new System.EventHandler(this.WaterReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptWater;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTaoReport;
        private System.Windows.Forms.DateTimePicker dtpkCheckOut;
        private System.Windows.Forms.DateTimePicker dtpkCheckIn;
    }
}