namespace PresentationLayer
{
    partial class frmMainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainScreen));
            this.btSettings = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pnMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.usCommandsScreen1 = new PresentationLayer.MainScreen.usCommandsScreen();
            this.pnMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSettings
            // 
            this.btSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSettings.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSettings.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btSettings.Image = ((System.Drawing.Image)(resources.GetObject("btSettings.Image")));
            this.btSettings.ImageOffset = new System.Drawing.Point(0, 0);
            this.btSettings.ImageRotate = 0F;
            this.btSettings.Location = new System.Drawing.Point(-3, 582);
            this.btSettings.Name = "btSettings";
            this.btSettings.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btSettings.Size = new System.Drawing.Size(102, 84);
            this.btSettings.TabIndex = 0;
            // 
            // pnMenu
            // 
            this.pnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(138)))), ((int)(((byte)(221)))));
            this.pnMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMenu.Controls.Add(this.btSettings);
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(104, 702);
            this.pnMenu.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.usCommandsScreen1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(104, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 702);
            this.panel1.TabIndex = 2;
            // 
            // usCommandsScreen1
            // 
            this.usCommandsScreen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.usCommandsScreen1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.usCommandsScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usCommandsScreen1.Location = new System.Drawing.Point(0, 0);
            this.usCommandsScreen1.MinimumSize = new System.Drawing.Size(819, 661);
            this.usCommandsScreen1.Name = "usCommandsScreen1";
            this.usCommandsScreen1.Size = new System.Drawing.Size(860, 702);
            this.usCommandsScreen1.TabIndex = 0;
            // 
            // frmMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 702);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnMenu);
            this.MinimumSize = new System.Drawing.Size(982, 749);
            this.Name = "frmMainScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BotPlus";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton btSettings;
        private System.Windows.Forms.Panel pnMenu;
        private System.Windows.Forms.Panel panel1;
        private MainScreen.usCommandsScreen usCommandsScreen1;
    }
}