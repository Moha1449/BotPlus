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
            this.tbCommandBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.btRunCommand = new Guna.UI2.WinForms.Guna2Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pnTitles = new System.Windows.Forms.Panel();
            this.lbBotState = new System.Windows.Forms.Label();
            this.pnCommandScreenControllers = new System.Windows.Forms.Panel();
            this.pnCommandsScreenContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.tbCommandsStatesBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnTitles.SuspendLayout();
            this.pnCommandScreenControllers.SuspendLayout();
            this.pnCommandsScreenContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCommandBox
            // 
            this.tbCommandBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommandBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.tbCommandBox.BorderRadius = 10;
            this.tbCommandBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCommandBox.DefaultText = "";
            this.tbCommandBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbCommandBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbCommandBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(25)))));
            this.tbCommandBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandBox.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            this.tbCommandBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbCommandBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandBox.Location = new System.Drawing.Point(19, 47);
            this.tbCommandBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbCommandBox.Name = "tbCommandBox";
            this.tbCommandBox.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(133)))));
            this.tbCommandBox.PlaceholderText = "Type Command\r\n";
            this.tbCommandBox.SelectedText = "";
            this.tbCommandBox.Size = new System.Drawing.Size(608, 47);
            this.tbCommandBox.TabIndex = 1;
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
            // pnCommandScreenControllers
            // 
            this.pnCommandScreenControllers.Controls.Add(this.tbCommandBox);
            this.pnCommandScreenControllers.Controls.Add(this.btRunCommand);
            this.pnCommandScreenControllers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnCommandScreenControllers.Location = new System.Drawing.Point(0, 479);
            this.pnCommandScreenControllers.Name = "pnCommandScreenControllers";
            this.pnCommandScreenControllers.Size = new System.Drawing.Size(769, 141);
            this.pnCommandScreenControllers.TabIndex = 5;
            // 
            // pnCommandsScreenContainer
            // 
            this.pnCommandsScreenContainer.Controls.Add(this.tbCommandsStatesBox);
            this.pnCommandsScreenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCommandsScreenContainer.Location = new System.Drawing.Point(0, 78);
            this.pnCommandsScreenContainer.Name = "pnCommandsScreenContainer";
            this.pnCommandsScreenContainer.Size = new System.Drawing.Size(769, 401);
            this.pnCommandsScreenContainer.TabIndex = 6;
            // 
            // tbCommandsStatesBox
            // 
            this.tbCommandsStatesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommandsStatesBox.BackColor = System.Drawing.Color.Transparent;
            this.tbCommandsStatesBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(194)))), ((int)(((byte)(183)))));
            this.tbCommandsStatesBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCommandsStatesBox.DefaultText = "";
            this.tbCommandsStatesBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbCommandsStatesBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbCommandsStatesBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandsStatesBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbCommandsStatesBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(25)))));
            this.tbCommandsStatesBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandsStatesBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbCommandsStatesBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbCommandsStatesBox.Location = new System.Drawing.Point(19, 7);
            this.tbCommandsStatesBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbCommandsStatesBox.Multiline = true;
            this.tbCommandsStatesBox.Name = "tbCommandsStatesBox";
            this.tbCommandsStatesBox.PlaceholderText = "";
            this.tbCommandsStatesBox.ReadOnly = true;
            this.tbCommandsStatesBox.SelectedText = "";
            this.tbCommandsStatesBox.Size = new System.Drawing.Size(736, 394);
            this.tbCommandsStatesBox.TabIndex = 0;
            // 
            // usCommandsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.Controls.Add(this.pnCommandsScreenContainer);
            this.Controls.Add(this.pnCommandScreenControllers);
            this.Controls.Add(this.pnTitles);
            this.Name = "usCommandsScreen";
            this.Size = new System.Drawing.Size(769, 620);
            this.pnTitles.ResumeLayout(false);
            this.pnTitles.PerformLayout();
            this.pnCommandScreenControllers.ResumeLayout(false);
            this.pnCommandsScreenContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox tbCommandBox;
        private Guna.UI2.WinForms.Guna2Button btRunCommand;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Panel pnTitles;
        private System.Windows.Forms.Label lbBotState;
        private System.Windows.Forms.Panel pnCommandScreenControllers;
        private Guna.UI2.WinForms.Guna2Panel pnCommandsScreenContainer;
        private Guna.UI2.WinForms.Guna2TextBox tbCommandsStatesBox;
    }
}
