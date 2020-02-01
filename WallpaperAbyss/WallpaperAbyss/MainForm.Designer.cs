using System.Threading.Tasks;
using WallpaperAbyssSettings;

namespace WallpaperAbyss
{
    partial class MainForm
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
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblNotConfigured = new System.Windows.Forms.Label();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblImageInfo = new System.Windows.Forms.Label();
            this.tmrCleanup = new System.Windows.Forms.Timer(this.components);
            this.pnlDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblNotConfigured
            // 
            this.lblNotConfigured.AutoSize = true;
            this.lblNotConfigured.BackColor = System.Drawing.Color.Black;
            this.lblNotConfigured.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotConfigured.ForeColor = System.Drawing.Color.White;
            this.lblNotConfigured.Location = new System.Drawing.Point(144, 126);
            this.lblNotConfigured.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNotConfigured.Name = "lblNotConfigured";
            this.lblNotConfigured.Size = new System.Drawing.Size(570, 58);
            this.lblNotConfigured.TabIndex = 0;
            this.lblNotConfigured.Text = "Settings Not Configured!";
            this.lblNotConfigured.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotConfigured.Visible = false;
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.lblAbout);
            this.pnlDescription.Controls.Add(this.lblImageInfo);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDescription.Location = new System.Drawing.Point(0, 539);
            this.pnlDescription.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(800, 52);
            this.pnlDescription.TabIndex = 3;
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAbout.ForeColor = System.Drawing.Color.White;
            this.lblAbout.Location = new System.Drawing.Point(0, 1);
            this.lblAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(203, 51);
            this.lblAbout.TabIndex = 4;
            this.lblAbout.Text = "Written By: https://fromthe.blue\r\nPowered By Wallpaper Abyss\r\nhttps://wall.alphac" +
    "oders.com";
            this.lblAbout.Visible = false;
            // 
            // lblImageInfo
            // 
            this.lblImageInfo.AutoSize = true;
            this.lblImageInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImageInfo.ForeColor = System.Drawing.Color.Transparent;
            this.lblImageInfo.Location = new System.Drawing.Point(800, 0);
            this.lblImageInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageInfo.Name = "lblImageInfo";
            this.lblImageInfo.Size = new System.Drawing.Size(0, 17);
            this.lblImageInfo.TabIndex = 3;
            this.lblImageInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // tmrCleanup
            // 
            this.tmrCleanup.Enabled = true;
            this.tmrCleanup.Interval = 10000;
            this.tmrCleanup.Tick += new System.EventHandler(this.tmrCleanup_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 591);
            this.Controls.Add(this.pnlDescription);
            this.Controls.Add(this.lblNotConfigured);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pnlDescription.ResumeLayout(false);
            this.pnlDescription.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblNotConfigured;
        private System.Windows.Forms.Panel pnlDescription;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblImageInfo;
        private System.Windows.Forms.Timer tmrCleanup;
    }
}

