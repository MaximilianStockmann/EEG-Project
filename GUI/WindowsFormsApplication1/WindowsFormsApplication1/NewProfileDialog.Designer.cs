namespace NewProfileDialog
{
    partial class NewProfileDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProfileDialog));
            this.confirmNewProfileButton = new System.Windows.Forms.Button();
            this.stashNewProfile = new System.Windows.Forms.Button();
            this.profileNameTextBox = new System.Windows.Forms.TextBox();
            this.elaborationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirmNewProfileButton
            // 
            this.confirmNewProfileButton.Location = new System.Drawing.Point(8, 95);
            this.confirmNewProfileButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmNewProfileButton.Name = "confirmNewProfileButton";
            this.confirmNewProfileButton.Size = new System.Drawing.Size(132, 28);
            this.confirmNewProfileButton.TabIndex = 0;
            this.confirmNewProfileButton.Text = "Profil Erstellen";
            this.confirmNewProfileButton.UseVisualStyleBackColor = true;
            this.confirmNewProfileButton.Click += new System.EventHandler(this.confirmNewProfileButton_Click);
            // 
            // stashNewProfile
            // 
            this.stashNewProfile.Location = new System.Drawing.Point(145, 95);
            this.stashNewProfile.Margin = new System.Windows.Forms.Padding(2);
            this.stashNewProfile.Name = "stashNewProfile";
            this.stashNewProfile.Size = new System.Drawing.Size(132, 28);
            this.stashNewProfile.TabIndex = 1;
            this.stashNewProfile.Text = "Abbrechen";
            this.stashNewProfile.UseVisualStyleBackColor = true;
            this.stashNewProfile.Click += new System.EventHandler(this.stashNewProfile_Click);
            // 
            // profileNameTextBox
            // 
            this.profileNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileNameTextBox.Location = new System.Drawing.Point(30, 55);
            this.profileNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.profileNameTextBox.Name = "profileNameTextBox";
            this.profileNameTextBox.Size = new System.Drawing.Size(224, 26);
            this.profileNameTextBox.TabIndex = 2;
            this.profileNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.profileNameTextBox_KeyDown);
            // 
            // elaborationLabel
            // 
            this.elaborationLabel.AutoSize = true;
            this.elaborationLabel.Location = new System.Drawing.Point(28, 24);
            this.elaborationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.elaborationLabel.Name = "elaborationLabel";
            this.elaborationLabel.Size = new System.Drawing.Size(226, 13);
            this.elaborationLabel.TabIndex = 3;
            this.elaborationLabel.Text = "Bitte einen Namen für das neue Profil eigeben:";
            // 
            // NewProfileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 141);
            this.Controls.Add(this.elaborationLabel);
            this.Controls.Add(this.profileNameTextBox);
            this.Controls.Add(this.stashNewProfile);
            this.Controls.Add(this.confirmNewProfileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NewProfileDialog";
            this.Text = "Neues Profil";
            this.Load += new System.EventHandler(this.NewProfileDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmNewProfileButton;
        private System.Windows.Forms.Button stashNewProfile;
        private System.Windows.Forms.TextBox profileNameTextBox;
        private System.Windows.Forms.Label elaborationLabel;
    }
}