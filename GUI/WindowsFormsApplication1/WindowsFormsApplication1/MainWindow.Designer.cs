namespace GUI_Namespace
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.loadProfileButton = new System.Windows.Forms.Button();
            this.profileSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.resetActionButton = new System.Windows.Forms.Button();
            this.trainActionSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.trainActionButton = new System.Windows.Forms.Button();
            this.driveButton = new System.Windows.Forms.Button();
            this.ctBotStatusLabel = new System.Windows.Forms.Label();
            this.engineStatusLabel = new System.Windows.Forms.Label();
            this.Company = new System.Windows.Forms.Label();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.newProfileButton = new System.Windows.Forms.Button();
            this.saveProfileButton = new System.Windows.Forms.Button();
            this.engineLabel = new System.Windows.Forms.Label();
            this.eegTicker = new System.Windows.Forms.Timer(this.components);
            this.label_for_ip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadProfileButton
            // 
            this.loadProfileButton.Location = new System.Drawing.Point(138, 64);
            this.loadProfileButton.Name = "loadProfileButton";
            this.loadProfileButton.Size = new System.Drawing.Size(69, 21);
            this.loadProfileButton.TabIndex = 0;
            this.loadProfileButton.Text = "Laden";
            this.loadProfileButton.UseVisualStyleBackColor = true;
            this.loadProfileButton.Click += new System.EventHandler(this.loadProfileButton_Click);
            // 
            // profileSelectionComboBox
            // 
            this.profileSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileSelectionComboBox.FormattingEnabled = true;
            this.profileSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.profileSelectionComboBox.Name = "profileSelectionComboBox";
            this.profileSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.profileSelectionComboBox.TabIndex = 1;
            // 
            // resetActionButton
            // 
            this.resetActionButton.Enabled = false;
            this.resetActionButton.Location = new System.Drawing.Point(287, 91);
            this.resetActionButton.Name = "resetActionButton";
            this.resetActionButton.Size = new System.Drawing.Size(83, 21);
            this.resetActionButton.TabIndex = 2;
            this.resetActionButton.Text = "Zurücksetzen";
            this.resetActionButton.UseVisualStyleBackColor = true;
            this.resetActionButton.Click += new System.EventHandler(this.resetActionButton_Click);
            // 
            // trainActionSelectionComboBox
            // 
            this.trainActionSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainActionSelectionComboBox.FormattingEnabled = true;
            this.trainActionSelectionComboBox.Items.AddRange(new object[] {
            "Stop / Neutral",
            "Vorwärts",
            "Links",
            "Rückwärts",
            "Rechts"});
            this.trainActionSelectionComboBox.Location = new System.Drawing.Point(11, 91);
            this.trainActionSelectionComboBox.Name = "trainActionSelectionComboBox";
            this.trainActionSelectionComboBox.Size = new System.Drawing.Size(122, 21);
            this.trainActionSelectionComboBox.TabIndex = 3;
            this.trainActionSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.trainActionSelectionComboBox_SelectedIndexChanged);
            // 
            // trainActionButton
            // 
            this.trainActionButton.Enabled = false;
            this.trainActionButton.Location = new System.Drawing.Point(139, 91);
            this.trainActionButton.Name = "trainActionButton";
            this.trainActionButton.Size = new System.Drawing.Size(142, 21);
            this.trainActionButton.TabIndex = 4;
            this.trainActionButton.Text = "Trainieren";
            this.trainActionButton.UseVisualStyleBackColor = true;
            this.trainActionButton.Click += new System.EventHandler(this.trainActionButton_Click);
            // 
            // driveButton
            // 
            this.driveButton.Location = new System.Drawing.Point(10, 122);
            this.driveButton.Name = "driveButton";
            this.driveButton.Size = new System.Drawing.Size(360, 51);
            this.driveButton.TabIndex = 5;
            this.driveButton.Text = "Fahren";
            this.driveButton.UseVisualStyleBackColor = true;
            this.driveButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // ctBotStatusLabel
            // 
            this.ctBotStatusLabel.AutoSize = true;
            this.ctBotStatusLabel.Location = new System.Drawing.Point(129, 9);
            this.ctBotStatusLabel.Name = "ctBotStatusLabel";
            this.ctBotStatusLabel.Size = new System.Drawing.Size(33, 13);
            this.ctBotStatusLabel.TabIndex = 6;
            this.ctBotStatusLabel.Text = "NEIN";
            // 
            // engineStatusLabel
            // 
            this.engineStatusLabel.AutoSize = true;
            this.engineStatusLabel.Location = new System.Drawing.Point(129, 22);
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
            this.Company.Location = new System.Drawing.Point(246, 1);
            this.Company.Name = "Company";
            this.Company.Size = new System.Drawing.Size(124, 24);
            this.Company.TabIndex = 12;
            this.Company.Text = "EEG-Projekt";
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionLabel.Location = new System.Drawing.Point(8, 9);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(116, 13);
            this.connectionLabel.TabIndex = 13;
            this.connectionLabel.Text = "c\'t Bot-Verbindung:";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(129, 38);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(52, 13);
            this.ipLabel.TabIndex = 14;
            this.ipLabel.Text = "127.0.0.1";
            this.ipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ipLabel.Click += new System.EventHandler(this.ipLabel_Click);
            // 
            // newProfileButton
            // 
            this.newProfileButton.Location = new System.Drawing.Point(287, 64);
            this.newProfileButton.Name = "newProfileButton";
            this.newProfileButton.Size = new System.Drawing.Size(83, 21);
            this.newProfileButton.TabIndex = 15;
            this.newProfileButton.Text = "Neues Profil";
            this.newProfileButton.UseVisualStyleBackColor = true;
            this.newProfileButton.Click += new System.EventHandler(this.newProfileButton_Click);
            // 
            // saveProfileButton
            // 
            this.saveProfileButton.Location = new System.Drawing.Point(213, 64);
            this.saveProfileButton.Name = "saveProfileButton";
            this.saveProfileButton.Size = new System.Drawing.Size(68, 21);
            this.saveProfileButton.TabIndex = 16;
            this.saveProfileButton.Text = "Speichern";
            this.saveProfileButton.UseVisualStyleBackColor = true;
            this.saveProfileButton.Click += new System.EventHandler(this.saveProfileButton_Click);
            // 
            // engineLabel
            // 
            this.engineLabel.AutoSize = true;
            this.engineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineLabel.Location = new System.Drawing.Point(47, 22);
            this.engineLabel.Name = "engineLabel";
            this.engineLabel.Size = new System.Drawing.Size(76, 13);
            this.engineLabel.TabIndex = 17;
            this.engineLabel.Text = "EEG Status:";
            // 
            // eegTicker
            // 
            this.eegTicker.Tick += new System.EventHandler(this.eegTicker_Tick);
            // 
            // label_for_ip
            // 
            this.label_for_ip.AutoSize = true;
            this.label_for_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_for_ip.Location = new System.Drawing.Point(100, 38);
            this.label_for_ip.Name = "label_for_ip";
            this.label_for_ip.Size = new System.Drawing.Size(23, 13);
            this.label_for_ip.TabIndex = 18;
            this.label_for_ip.Text = "IP:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 185);
            this.Controls.Add(this.label_for_ip);
            this.Controls.Add(this.engineStatusLabel);
            this.Controls.Add(this.engineLabel);
            this.Controls.Add(this.saveProfileButton);
            this.Controls.Add(this.newProfileButton);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.ctBotStatusLabel);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.Company);
            this.Controls.Add(this.driveButton);
            this.Controls.Add(this.trainActionButton);
            this.Controls.Add(this.trainActionSelectionComboBox);
            this.Controls.Add(this.resetActionButton);
            this.Controls.Add(this.profileSelectionComboBox);
            this.Controls.Add(this.loadProfileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "EEG-Projekt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadProfileButton;
        private System.Windows.Forms.ComboBox profileSelectionComboBox;
        private System.Windows.Forms.Button resetActionButton;
        private System.Windows.Forms.ComboBox trainActionSelectionComboBox;
        private System.Windows.Forms.Button trainActionButton;
        private System.Windows.Forms.Button driveButton;
        private System.Windows.Forms.Label ctBotStatusLabel;
        private System.Windows.Forms.Label engineStatusLabel;
        private System.Windows.Forms.Label Company;
        private System.Windows.Forms.Label connectionLabel;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Button newProfileButton;
        private System.Windows.Forms.Button saveProfileButton;
        private System.Windows.Forms.Label engineLabel;
        private System.Windows.Forms.Timer eegTicker;
        private System.Windows.Forms.Label label_for_ip;
    }
}

