namespace PresentationLayer.MainScreen
{
    partial class usCommandsScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbCommandInputer = new Guna.UI2.WinForms.Guna2TextBox();
            this.btRunCommand = new Guna.UI2.WinForms.Guna2Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pnTitles = new System.Windows.Forms.Panel();
            this.lbBotState = new System.Windows.Forms.Label();
            this.pnCommandControllers = new System.Windows.Forms.Panel();
            this.pnCommandsScreenContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.tbCommandsStateShower = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnTitles.SuspendLayout();
            this.pnCommandControllers.SuspendLayout();
            this.pnCommandsScreenContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCommandInputer
            // 
            this.tbCommandInputer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommandInputer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.tbCommandInputer.BorderRadius = 10;
            this.tbCommandInputer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCommandInputer.DefaultText = "";
            this.tbCommandInputer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbCommandInputer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbCommandInputer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandInputer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandInputer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(25)))));
            this.tbCommandInputer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandInputer.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            this.tbCommandInputer.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbCommandInputer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandInputer.Location = new System.Drawing.Point(19, 47);
            this.tbCommandInputer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbCommandInputer.Name = "tbCommandInputer";
            this.tbCommandInputer.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(133)))));
            this.tbCommandInputer.PlaceholderText = "Type Command\r\n";
            this.tbCommandInputer.SelectedText = "";
            this.tbCommandInputer.Size = new System.Drawing.Size(608, 47);
            this.tbCommandInputer.TabIndex = 1;
            this.tbCommandInputer.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // btRunCommand
            // 
            this.btRunCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRunCommand.BackColor = System.Drawing.Color.Transparent;
            this.btRunCommand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.btRunCommand.BorderRadius = 12;
            this.btRunCommand.BorderThickness = 1;
            this.btRunCommand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btRunCommand.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btRunCommand.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btRunCommand.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btRunCommand.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btRunCommand.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(25)))));
            this.btRunCommand.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btRunCommand.ForeColor = System.Drawing.Color.White;
            this.btRunCommand.Location = new System.Drawing.Point(633, 47);
            this.btRunCommand.Name = "btRunCommand";
            this.btRunCommand.Size = new System.Drawing.Size(122, 47);
            this.btRunCommand.TabIndex = 2;
            this.btRunCommand.Text = "Run";
            this.btRunCommand.Click += new System.EventHandler(this.guna2Button1_Click_1);
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.lbTitle.Location = new System.Drawing.Point(13, 25);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(241, 32);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "Command Screen";
            // 
            // pnTitles
            // 
            this.pnTitles.Controls.Add(this.lbBotState);
            this.pnTitles.Controls.Add(this.lbTitle);
            this.pnTitles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitles.Location = new System.Drawing.Point(0, 0);
            this.pnTitles.Name = "pnTitles";
            this.pnTitles.Size = new System.Drawing.Size(769, 78);
            this.pnTitles.TabIndex = 4;
            // 
            // lbBotState
            // 
            this.lbBotState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBotState.AutoSize = true;
            this.lbBotState.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBotState.ForeColor = System.Drawing.Color.Red;
            this.lbBotState.Location = new System.Drawing.Point(584, 25);
            this.lbBotState.Name = "lbBotState";
            this.lbBotState.Size = new System.Drawing.Size(171, 32);
            this.lbBotState.TabIndex = 4;
            this.lbBotState.Text = "Bot Stopped";
            // 
            // pnCommandControllers
            // 
            this.pnCommandControllers.Controls.Add(this.tbCommandInputer);
            this.pnCommandControllers.Controls.Add(this.btRunCommand);
            this.pnCommandControllers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnCommandControllers.Location = new System.Drawing.Point(0, 479);
            this.pnCommandControllers.Name = "pnCommandControllers";
            this.pnCommandControllers.Size = new System.Drawing.Size(769, 141);
            this.pnCommandControllers.TabIndex = 5;
            // 
            // pnCommandsScreenContainer
            // 
            this.pnCommandsScreenContainer.Controls.Add(this.tbCommandsStateShower);
            this.pnCommandsScreenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCommandsScreenContainer.Location = new System.Drawing.Point(0, 78);
            this.pnCommandsScreenContainer.Name = "pnCommandsScreenContainer";
            this.pnCommandsScreenContainer.Size = new System.Drawing.Size(769, 401);
            this.pnCommandsScreenContainer.TabIndex = 6;
            // 
            // tbCommandsStateShower
            // 
            this.tbCommandsStateShower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommandsStateShower.BackColor = System.Drawing.Color.Transparent;
            this.tbCommandsStateShower.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.tbCommandsStateShower.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCommandsStateShower.DefaultText = "";
            this.tbCommandsStateShower.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbCommandsStateShower.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbCommandsStateShower.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandsStateShower.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandsStateShower.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(25)))));
            this.tbCommandsStateShower.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandsStateShower.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbCommandsStateShower.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandsStateShower.Location = new System.Drawing.Point(19, 7);
            this.tbCommandsStateShower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbCommandsStateShower.Multiline = true;
            this.tbCommandsStateShower.Name = "tbCommandsStateShower";
            this.tbCommandsStateShower.PlaceholderText = "";
            this.tbCommandsStateShower.ReadOnly = true;
            this.tbCommandsStateShower.SelectedText = "";
            this.tbCommandsStateShower.Size = new System.Drawing.Size(736, 394);
            this.tbCommandsStateShower.TabIndex = 0;
            this.tbCommandsStateShower.TextChanged += new System.EventHandler(this.tbCommands_TextChanged);
            // 
            // usCommandsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.Controls.Add(this.pnCommandsScreenContainer);
            this.Controls.Add(this.pnCommandControllers);
            this.Controls.Add(this.pnTitles);
            this.Name = "usCommandsScreen";
            this.Size = new System.Drawing.Size(769, 620);
            this.Load += new System.EventHandler(this.usCommandsScreen_Load);
            this.pnTitles.ResumeLayout(false);
            this.pnTitles.PerformLayout();
            this.pnCommandControllers.ResumeLayout(false);
            this.pnCommandsScreenContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox tbCommandInputer;
        private Guna.UI2.WinForms.Guna2Button btRunCommand;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Panel pnTitles;
        private System.Windows.Forms.Label lbBotState;
        private System.Windows.Forms.Panel pnCommandControllers;
        private Guna.UI2.WinForms.Guna2Panel pnCommandsScreenContainer;
        private Guna.UI2.WinForms.Guna2TextBox tbCommandsStateShower;
    }
}
