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
            this.eegTicker = new System.Windows.Forms.Timer(this.components);
            this.eegStatusLB = new System.Windows.Forms.ListBox();
            this.currentActionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadProfileButton
            // 
            this.loadProfileButton.Location = new System.Drawing.Point(223, 51);
            this.loadProfileButton.Name = "loadProfileButton";
            this.loadProfileButton.Size = new System.Drawing.Size(83, 21);
            this.loadProfileButton.TabIndex = 0;
            this.loadProfileButton.Text = "Laden";
            this.loadProfileButton.UseVisualStyleBackColor = true;
            this.loadProfileButton.Click += new System.EventHandler(this.loadProfileButton_Click);
            // 
            // profileSelectionComboBox
            // 
            this.profileSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileSelectionComboBox.FormattingEnabled = true;
            this.profileSelectionComboBox.Items.AddRange(new object[] {
            "Testprofil 1",
            "Testprofil 2",
            "Stefan Doing Stuff"});
            this.profileSelectionComboBox.Location = new System.Drawing.Point(11, 51);
            this.profileSelectionComboBox.Name = "profileSelectionComboBox";
            this.profileSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.profileSelectionComboBox.TabIndex = 1;
            this.profileSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.profileSelectionComboBox_SelectedIndexChanged);
            // 
            // resetActionButton
            // 
            this.resetActionButton.Enabled = false;
            this.resetActionButton.Location = new System.Drawing.Point(308, 79);
            this.resetActionButton.Name = "resetActionButton";
            this.resetActionButton.Size = new System.Drawing.Size(83, 21);
            this.resetActionButton.TabIndex = 2;
            this.resetActionButton.Text = "Zurücksetzen";
            this.resetActionButton.UseVisualStyleBackColor = true;
            this.resetActionButton.Click += new System.EventHandler(this.resetProfileButton_Click);
            // 
            // trainActionSelectionComboBox
            // 
            this.trainActionSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainActionSelectionComboBox.FormattingEnabled = true;
            this.trainActionSelectionComboBox.Items.AddRange(new object[] {
            "stop",
            "forward",
            "backward",
            "left",
            "right"});
            this.trainActionSelectionComboBox.Location = new System.Drawing.Point(11, 80);
            this.trainActionSelectionComboBox.Name = "trainActionSelectionComboBox";
            this.trainActionSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.trainActionSelectionComboBox.TabIndex = 3;
            this.trainActionSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.trainActionSelectionComboBox_SelectedIndexChanged);
            // 
            // trainActionButton
            // 
            this.trainActionButton.Enabled = false;
            this.trainActionButton.Location = new System.Drawing.Point(137, 79);
            this.trainActionButton.Name = "trainActionButton";
            this.trainActionButton.Size = new System.Drawing.Size(169, 21);
            this.trainActionButton.TabIndex = 4;
            this.trainActionButton.Text = "Trainieren";
            this.trainActionButton.UseVisualStyleBackColor = true;
            this.trainActionButton.Click += new System.EventHandler(this.trainActionButton_Click);
            // 
            // driveButton
            // 
            this.driveButton.Location = new System.Drawing.Point(11, 106);
            this.driveButton.Name = "driveButton";
            this.driveButton.Size = new System.Drawing.Size(380, 51);
            this.driveButton.TabIndex = 5;
            this.driveButton.Text = "Fahren";
            this.driveButton.UseVisualStyleBackColor = true;
            this.driveButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // ctBotStatusLabel
            // 
            this.ctBotStatusLabel.AutoSize = true;
            this.ctBotStatusLabel.Location = new System.Drawing.Point(72, 9);
            this.ctBotStatusLabel.Name = "ctBotStatusLabel";
            this.ctBotStatusLabel.Size = new System.Drawing.Size(33, 13);
            this.ctBotStatusLabel.TabIndex = 6;
            this.ctBotStatusLabel.Text = "NEIN";
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
            this.Company.Location = new System.Drawing.Point(133, 1);
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
            this.ipLabel.Location = new System.Drawing.Point(135, 25);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(68, 13);
            this.ipLabel.TabIndex = 14;
            this.ipLabel.Text = "IP: 127.0.0.1";
            this.ipLabel.Click += new System.EventHandler(this.ipLabel_Click);
            // 
            // newProfileButton
            // 
            this.newProfileButton.Location = new System.Drawing.Point(136, 51);
            this.newProfileButton.Name = "newProfileButton";
            this.newProfileButton.Size = new System.Drawing.Size(83, 21);
            this.newProfileButton.TabIndex = 15;
            this.newProfileButton.Text = "Neues Profil";
            this.newProfileButton.UseVisualStyleBackColor = true;
            this.newProfileButton.Click += new System.EventHandler(this.newProfileButton_Click);
            // 
            // saveProfileButton
            // 
            this.saveProfileButton.Location = new System.Drawing.Point(308, 51);
            this.saveProfileButton.Name = "saveProfileButton";
            this.saveProfileButton.Size = new System.Drawing.Size(83, 21);
            this.saveProfileButton.TabIndex = 16;
            this.saveProfileButton.Text = "Speichern";
            this.saveProfileButton.UseVisualStyleBackColor = true;
            this.saveProfileButton.Click += new System.EventHandler(this.saveProfileButton_Click);
            // 
            // eegTicker
            // 
            this.eegTicker.Tick += new System.EventHandler(this.eegTicker_Tick);
            // 
            // eegStatusLB
            // 
            this.eegStatusLB.FormattingEnabled = true;
            this.eegStatusLB.Location = new System.Drawing.Point(10, 197);
            this.eegStatusLB.Name = "eegStatusLB";
            this.eegStatusLB.Size = new System.Drawing.Size(210, 160);
            this.eegStatusLB.TabIndex = 17;
            // 
            // currentActionLabel
            // 
            this.currentActionLabel.AutoSize = true;
            this.currentActionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentActionLabel.Location = new System.Drawing.Point(227, 197);
            this.currentActionLabel.Name = "currentActionLabel";
            this.currentActionLabel.Size = new System.Drawing.Size(70, 25);
            this.currentActionLabel.TabIndex = 18;
            this.currentActionLabel.Text = "label1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 375);
            this.Controls.Add(this.currentActionLabel);
            this.Controls.Add(this.eegStatusLB);
            this.Controls.Add(this.saveProfileButton);
            this.Controls.Add(this.newProfileButton);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.ctBotStatusLabel);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.Company);
            this.Controls.Add(this.engineStatusLabel);
            this.Controls.Add(this.driveButton);
            this.Controls.Add(this.trainActionButton);
            this.Controls.Add(this.trainActionSelectionComboBox);
            this.Controls.Add(this.resetActionButton);
            this.Controls.Add(this.profileSelectionComboBox);
            this.Controls.Add(this.loadProfileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "Emotiv";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
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
        private System.Windows.Forms.Timer eegTicker;
        private System.Windows.Forms.ListBox eegStatusLB;
        private System.Windows.Forms.Label currentActionLabel;
    }
}

