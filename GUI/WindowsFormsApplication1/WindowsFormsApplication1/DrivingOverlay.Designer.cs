namespace WindowsFormsApplication1
{
    partial class DrivingOverlay
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
            this.leftPB = new System.Windows.Forms.PictureBox();
            this.forwardPB = new System.Windows.Forms.PictureBox();
            this.neutralPB = new System.Windows.Forms.PictureBox();
            this.rightPB = new System.Windows.Forms.PictureBox();
            this.backwardPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.leftPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutralPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backwardPB)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPB
            // 
            this.leftPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.leftPB.Location = new System.Drawing.Point(12, 172);
            this.leftPB.Name = "leftPB";
            this.leftPB.Size = new System.Drawing.Size(150, 150);
            this.leftPB.TabIndex = 0;
            this.leftPB.TabStop = false;
            // 
            // forwardPB
            // 
            this.forwardPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.forwardPB.Location = new System.Drawing.Point(168, 15);
            this.forwardPB.Name = "forwardPB";
            this.forwardPB.Size = new System.Drawing.Size(150, 150);
            this.forwardPB.TabIndex = 1;
            this.forwardPB.TabStop = false;
            // 
            // neutralPB
            // 
            this.neutralPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.neutralPB.Location = new System.Drawing.Point(168, 172);
            this.neutralPB.Name = "neutralPB";
            this.neutralPB.Size = new System.Drawing.Size(150, 150);
            this.neutralPB.TabIndex = 2;
            this.neutralPB.TabStop = false;
            // 
            // rightPB
            // 
            this.rightPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rightPB.Location = new System.Drawing.Point(324, 172);
            this.rightPB.Name = "rightPB";
            this.rightPB.Size = new System.Drawing.Size(150, 150);
            this.rightPB.TabIndex = 3;
            this.rightPB.TabStop = false;
            // 
            // backwardPB
            // 
            this.backwardPB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.backwardPB.Location = new System.Drawing.Point(168, 328);
            this.backwardPB.Name = "backwardPB";
            this.backwardPB.Size = new System.Drawing.Size(150, 150);
            this.backwardPB.TabIndex = 4;
            this.backwardPB.TabStop = false;
            // 
            // DrivingOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 494);
            this.Controls.Add(this.backwardPB);
            this.Controls.Add(this.rightPB);
            this.Controls.Add(this.neutralPB);
            this.Controls.Add(this.forwardPB);
            this.Controls.Add(this.leftPB);
            this.Name = "DrivingOverlay";
            this.Text = "DrivingOverlay";
            this.Load += new System.EventHandler(this.DrivingOverlay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leftPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutralPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backwardPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox leftPB;
        private System.Windows.Forms.PictureBox forwardPB;
        private System.Windows.Forms.PictureBox neutralPB;
        private System.Windows.Forms.PictureBox rightPB;
        private System.Windows.Forms.PictureBox backwardPB;
    }
}