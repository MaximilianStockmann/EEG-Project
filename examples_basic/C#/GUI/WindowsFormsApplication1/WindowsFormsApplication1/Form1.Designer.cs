namespace WindowsFormsApplication1
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
            this.SuspendLayout();
            // 
            // loadProfileButton
            // 
            this.loadProfileButton.Location = new System.Drawing.Point(138, 15);
            this.loadProfileButton.Name = "loadProfileButton";
            this.loadProfileButton.Size = new System.Drawing.Size(68, 21);
            this.loadProfileButton.TabIndex = 0;
            this.loadProfileButton.Text = "Load";
            this.loadProfileButton.UseVisualStyleBackColor = true;
            this.loadProfileButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // profileSelectionComboBox
            // 
            this.profileSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileSelectionComboBox.FormattingEnabled = true;
            this.profileSelectionComboBox.Items.AddRange(new object[] {
            "Testprofil 1",
            "Testprofil 2"});
            this.profileSelectionComboBox.Location = new System.Drawing.Point(11, 15);
            this.profileSelectionComboBox.Name = "profileSelectionComboBox";
            this.profileSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.profileSelectionComboBox.TabIndex = 1;
            // 
            // resetProfileButton
            // 
            this.resetProfileButton.Location = new System.Drawing.Point(212, 15);
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
            "Neutral",
            "Forward",
            "Left",
            "Backward",
            "Right"});
            this.trainActionSelectionComboBox.Location = new System.Drawing.Point(11, 42);
            this.trainActionSelectionComboBox.Name = "trainActionSelectionComboBox";
            this.trainActionSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.trainActionSelectionComboBox.TabIndex = 3;
            // 
            // trainActionButton
            // 
            this.trainActionButton.Location = new System.Drawing.Point(138, 42);
            this.trainActionButton.Name = "trainActionButton";
            this.trainActionButton.Size = new System.Drawing.Size(139, 21);
            this.trainActionButton.TabIndex = 4;
            this.trainActionButton.Text = "Train";
            this.trainActionButton.UseVisualStyleBackColor = true;
            // 
            // driveButton
            // 
            this.driveButton.Location = new System.Drawing.Point(11, 69);
            this.driveButton.Name = "driveButton";
            this.driveButton.Size = new System.Drawing.Size(266, 51);
            this.driveButton.TabIndex = 5;
            this.driveButton.Text = "Start Driving";
            this.driveButton.UseVisualStyleBackColor = true;
            this.driveButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 128);
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

        }

        #endregion

        private System.Windows.Forms.Button loadProfileButton;
        private System.Windows.Forms.ComboBox profileSelectionComboBox;
        private System.Windows.Forms.Button resetProfileButton;
        private System.Windows.Forms.ComboBox trainActionSelectionComboBox;
        private System.Windows.Forms.Button trainActionButton;
        private System.Windows.Forms.Button driveButton;
    }
}

