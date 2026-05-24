namespace ReactRunner
{
    partial class frmRR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRR));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlArea = new System.Windows.Forms.Panel();
            this.pnlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(13, 23);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(94, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(13, 11);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(142, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Drage the poject folder here.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlArea
            // 
            this.pnlArea.AllowDrop = true;
            this.pnlArea.Controls.Add(this.lblInfo);
            this.pnlArea.Location = new System.Drawing.Point(188, 14);
            this.pnlArea.Name = "pnlArea";
            this.pnlArea.Size = new System.Drawing.Size(204, 36);
            this.pnlArea.TabIndex = 3;
            this.pnlArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlArea_DragDrop);
            this.pnlArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlArea_DragEnter);
            this.pnlArea.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlArea_Paint);
            // 
            // frmRR
            // 
            this.ClientSize = new System.Drawing.Size(409, 66);
            this.Controls.Add(this.pnlArea);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmRR";
            this.Text = "React Runner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRR_FormClosing);
            this.Load += new System.EventHandler(this.frmRR_Load);
            this.pnlArea.ResumeLayout(false);
            this.pnlArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlArea;
    }
}

