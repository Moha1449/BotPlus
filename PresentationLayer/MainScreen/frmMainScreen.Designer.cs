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
            this.pnMenu = new System.Windows.Forms.Panel();
            this.pnScreenContainer = new System.Windows.Forms.Panel();
            this.usCommandsScreen1 = new PresentationLayer.MainScreen.usCommandsScreen();
            this.pnScreenContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMenu
            // 
            this.pnMenu.BackColor = System.Drawing.Color.Black;
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(104, 702);
            this.pnMenu.TabIndex = 1;
            // 
            // pnScreenContainer
            // 
            this.pnScreenContainer.Controls.Add(this.usCommandsScreen1);
            this.pnScreenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnScreenContainer.Location = new System.Drawing.Point(104, 0);
            this.pnScreenContainer.Name = "pnScreenContainer";
            this.pnScreenContainer.Size = new System.Drawing.Size(860, 702);
            this.pnScreenContainer.TabIndex = 2;
            // 
            // usCommandsScreen1
            // 
            this.usCommandsScreen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
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
            this.Controls.Add(this.pnScreenContainer);
            this.Controls.Add(this.pnMenu);
            this.MinimumSize = new System.Drawing.Size(982, 749);
            this.Name = "frmMainScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BotPlus";
            this.pnScreenContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnMenu;
        private System.Windows.Forms.Panel pnScreenContainer;
        private MainScreen.usCommandsScreen usCommandsScreen1;
    }
}