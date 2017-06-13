namespace acceptTraining
{
    partial class acceptTrainingDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.delineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Would you like to accept this training?";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(15, 40);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 1;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // delineButton
            // 
            this.delineButton.Location = new System.Drawing.Point(124, 40);
            this.delineButton.Name = "delineButton";
            this.delineButton.Size = new System.Drawing.Size(75, 23);
            this.delineButton.TabIndex = 2;
            this.delineButton.Text = "Decline";
            this.delineButton.UseVisualStyleBackColor = true;
            this.delineButton.Click += new System.EventHandler(this.delineButton_Click);
            // 
            // acceptTrainingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 82);
            this.Controls.Add(this.delineButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label1);
            this.Name = "acceptTrainingDialog";
            this.Text = "Training successful";
            this.Load += new System.EventHandler(this.acceptTrainingDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button delineButton;
    }
}