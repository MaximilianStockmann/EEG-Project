﻿namespace GUI_Namespace
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadProfileButton = new System.Windows.Forms.Button();
            this.profileSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.resetProfileButton = new System.Windows.Forms.Button();
            this.trainActionSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.trainActionButton = new System.Windows.Forms.Button();
            this.driveButton = new System.Windows.Forms.Button();
            this.ctBotStatusLabel = new System.Windows.Forms.Label();
            this.engineStatusLabel = new System.Windows.Forms.Label();
            this.Company = new System.Windows.Forms.Label();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadProfileButton
            // 
            this.loadProfileButton.Location = new System.Drawing.Point(138, 51);
            this.loadProfileButton.Name = "loadProfileButton";
            this.loadProfileButton.Size = new System.Drawing.Size(68, 21);
            this.loadProfileButton.TabIndex = 0;
            this.loadProfileButton.Text = "Load";
            this.loadProfileButton.UseVisualStyleBackColor = true;
            // 
            // profileSelectionComboBox
            // 
            this.profileSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileSelectionComboBox.FormattingEnabled = true;
            this.profileSelectionComboBox.Items.AddRange(new object[] {
            "Testprofil 1",
            "Testprofil 2"});
            this.profileSelectionComboBox.Location = new System.Drawing.Point(11, 51);
            this.profileSelectionComboBox.Name = "profileSelectionComboBox";
            this.profileSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.profileSelectionComboBox.TabIndex = 1;
            // 
            // resetProfileButton
            // 
            this.resetProfileButton.Location = new System.Drawing.Point(212, 51);
            this.resetProfileButton.Name = "resetProfileButton";
            this.resetProfileButton.Size = new System.Drawing.Size(68, 21);
            this.resetProfileButton.TabIndex = 2;
            this.resetProfileButton.Text = "Reset";
            this.resetProfileButton.UseVisualStyleBackColor = true;
            // 
            // trainActionSelectionComboBox
            // 
            this.trainActionSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainActionSelectionComboBox.FormattingEnabled = true;
            this.trainActionSelectionComboBox.Items.AddRange(new object[] {
            "Stop",
            "Forward",
            "Left",
            "Backward",
            "Right"});
            this.trainActionSelectionComboBox.Location = new System.Drawing.Point(11, 78);
            this.trainActionSelectionComboBox.Name = "trainActionSelectionComboBox";
            this.trainActionSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.trainActionSelectionComboBox.TabIndex = 3;
            // 
            // trainActionButton
            // 
            this.trainActionButton.Location = new System.Drawing.Point(138, 78);
            this.trainActionButton.Name = "trainActionButton";
            this.trainActionButton.Size = new System.Drawing.Size(139, 21);
            this.trainActionButton.TabIndex = 4;
            this.trainActionButton.Text = "Train";
            this.trainActionButton.UseVisualStyleBackColor = true;
            this.trainActionButton.Click += new System.EventHandler(this.trainActionButton_Click);
            // 
            // driveButton
            // 
            this.driveButton.Location = new System.Drawing.Point(11, 105);
            this.driveButton.Name = "driveButton";
            this.driveButton.Size = new System.Drawing.Size(266, 51);
            this.driveButton.TabIndex = 5;
            this.driveButton.Text = "Start Driving";
            this.driveButton.UseVisualStyleBackColor = true;
            this.driveButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // ctBotStatusLabel
            // 
            this.ctBotStatusLabel.AutoSize = true;
            this.ctBotStatusLabel.Location = new System.Drawing.Point(72, 9);
            this.ctBotStatusLabel.Name = "ctBotStatusLabel";
            this.ctBotStatusLabel.Size = new System.Drawing.Size(23, 13);
            this.ctBotStatusLabel.TabIndex = 6;
            this.ctBotStatusLabel.Text = "NO";
            // 
            // engineStatusLabel
            // 
            this.engineStatusLabel.AutoSize = true;
            this.engineStatusLabel.Location = new System.Drawing.Point(12, 22);
            this.engineStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.engineStatusLabel.Name = "engineStatusLabel";
            this.engineStatusLabel.Size = new System.Drawing.Size(69, 13);
            this.engineStatusLabel.TabIndex = 11;
            this.engineStatusLabel.Text = "engineStatus";
            // 
            // Company
            // 
            this.Company.AutoSize = true;
            this.Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Company.Location = new System.Drawing.Point(156, 9);
            this.Company.Name = "Company";
            this.Company.Size = new System.Drawing.Size(124, 24);
            this.Company.TabIndex = 12;
            this.Company.Text = "EEG-Projekt";
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Location = new System.Drawing.Point(11, 9);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(64, 13);
            this.connectionLabel.TabIndex = 13;
            this.connectionLabel.Text = "Connection:";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(157, 33);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(68, 13);
            this.ipLabel.TabIndex = 14;
            this.ipLabel.Text = "IP: 127.0.0.1";
            this.ipLabel.Click += new System.EventHandler(this.ipLabel_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 167);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.ctBotStatusLabel);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.Company);
            this.Controls.Add(this.engineStatusLabel);
            this.Controls.Add(this.driveButton);
            this.Controls.Add(this.trainActionButton);
            this.Controls.Add(this.trainActionSelectionComboBox);
            this.Controls.Add(this.resetProfileButton);
            this.Controls.Add(this.profileSelectionComboBox);
            this.Controls.Add(this.loadProfileButton);
            this.Name = "MainWindow";
            this.Text = "Emotiv";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadProfileButton;
        private System.Windows.Forms.ComboBox profileSelectionComboBox;
        private System.Windows.Forms.Button resetProfileButton;
        private System.Windows.Forms.ComboBox trainActionSelectionComboBox;
        private System.Windows.Forms.Button trainActionButton;
        private System.Windows.Forms.Button driveButton;
        private System.Windows.Forms.Label ctBotStatusLabel;
        private System.Windows.Forms.Label engineStatusLabel;
        private System.Windows.Forms.Label Company;
        private System.Windows.Forms.Label connectionLabel;
        private System.Windows.Forms.Label ipLabel;
    }
}

