namespace WindowsFormsApplication1
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
            this.confirmNewProfileButton = new System.Windows.Forms.Button();
            this.stashNewProfile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.elaborationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirmNewProfileButton
            // 
            this.confirmNewProfileButton.Location = new System.Drawing.Point(11, 117);
            this.confirmNewProfileButton.Name = "confirmNewProfileButton";
            this.confirmNewProfileButton.Size = new System.Drawing.Size(176, 35);
            this.confirmNewProfileButton.TabIndex = 0;
            this.confirmNewProfileButton.Text = "Profil Erstellen";
            this.confirmNewProfileButton.UseVisualStyleBackColor = true;
            // 
            // stashNewProfile
            // 
            this.stashNewProfile.Location = new System.Drawing.Point(193, 117);
            this.stashNewProfile.Name = "stashNewProfile";
            this.stashNewProfile.Size = new System.Drawing.Size(176, 35);
            this.stashNewProfile.TabIndex = 1;
            this.stashNewProfile.Text = "Abbrechen";
            this.stashNewProfile.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(40, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(297, 30);
            this.textBox1.TabIndex = 2;
            // 
            // elaborationLabel
            // 
            this.elaborationLabel.AutoSize = true;
            this.elaborationLabel.Location = new System.Drawing.Point(37, 30);
            this.elaborationLabel.Name = "elaborationLabel";
            this.elaborationLabel.Size = new System.Drawing.Size(303, 17);
            this.elaborationLabel.TabIndex = 3;
            this.elaborationLabel.Text = "Bitte einen Namen für das neue Profil eigeben:";
            // 
            // NewProfileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 174);
            this.Controls.Add(this.elaborationLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stashNewProfile);
            this.Controls.Add(this.confirmNewProfileButton);
            this.Name = "NewProfileDialog";
            this.Text = "NewProfileDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmNewProfileButton;
        private System.Windows.Forms.Button stashNewProfile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label elaborationLabel;
    }
}