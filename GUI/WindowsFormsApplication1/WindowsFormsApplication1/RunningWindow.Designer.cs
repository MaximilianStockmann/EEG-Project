namespace runningWindow
{
    partial class startRunningWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startRunningWindow));
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxDown = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxMid = new System.Windows.Forms.PictureBox();
            this.pictureBoxUp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Image = global::WindowsFormsApplication1.Properties.Resources.rightO;
            this.pictureBoxRight.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxRight.InitialImage")));
            this.pictureBoxRight.Location = new System.Drawing.Point(117, 58);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxRight.TabIndex = 4;
            this.pictureBoxRight.TabStop = false;
            // 
            // pictureBoxDown
            // 
            this.pictureBoxDown.Image = global::WindowsFormsApplication1.Properties.Resources.downO;
            this.pictureBoxDown.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxDown.InitialImage")));
            this.pictureBoxDown.Location = new System.Drawing.Point(67, 108);
            this.pictureBoxDown.Name = "pictureBoxDown";
            this.pictureBoxDown.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxDown.TabIndex = 3;
            this.pictureBoxDown.TabStop = false;
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Image = global::WindowsFormsApplication1.Properties.Resources.leftO;
            this.pictureBoxLeft.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLeft.InitialImage")));
            this.pictureBoxLeft.Location = new System.Drawing.Point(17, 58);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxLeft.TabIndex = 2;
            this.pictureBoxLeft.TabStop = false;
            // 
            // pictureBoxMid
            // 
            this.pictureBoxMid.Image = global::WindowsFormsApplication1.Properties.Resources.midO;
            this.pictureBoxMid.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxMid.InitialImage")));
            this.pictureBoxMid.Location = new System.Drawing.Point(67, 58);
            this.pictureBoxMid.Name = "pictureBoxMid";
            this.pictureBoxMid.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxMid.TabIndex = 1;
            this.pictureBoxMid.TabStop = false;
            // 
            // pictureBoxUp
            // 
            this.pictureBoxUp.Image = global::WindowsFormsApplication1.Properties.Resources.upO;
            this.pictureBoxUp.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxUp.InitialImage")));
            this.pictureBoxUp.Location = new System.Drawing.Point(67, 8);
            this.pictureBoxUp.Name = "pictureBoxUp";
            this.pictureBoxUp.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxUp.TabIndex = 0;
            this.pictureBoxUp.TabStop = false;
            // 
            // startRunningWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 162);
            this.Controls.Add(this.pictureBoxRight);
            this.Controls.Add(this.pictureBoxDown);
            this.Controls.Add(this.pictureBoxLeft);
            this.Controls.Add(this.pictureBoxMid);
            this.Controls.Add(this.pictureBoxUp);
            this.Name = "startRunningWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxUp;
        private System.Windows.Forms.PictureBox pictureBoxMid;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxDown;
        private System.Windows.Forms.PictureBox pictureBoxRight;
    }
}