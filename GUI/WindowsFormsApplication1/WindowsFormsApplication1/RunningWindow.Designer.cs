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
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.speedLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Image = global::WindowsFormsApplication1.Properties.Resources.rightO;
            this.pictureBoxRight.InitialImage = global::WindowsFormsApplication1.Properties.Resources.rightO;
            this.pictureBoxRight.Location = new System.Drawing.Point(117, 58);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxRight.TabIndex = 4;
            this.pictureBoxRight.TabStop = false;
            this.pictureBoxRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRight_MouseDown);
            this.pictureBoxRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRight_MouseUp);
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
            this.pictureBoxDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDown_MouseDown);
            this.pictureBoxDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDown_MouseUp);
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
            this.pictureBoxLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLeft_MouseDown);
            this.pictureBoxLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLeft_MouseUp);
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
            this.pictureBoxMid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMid_MouseDown);
            this.pictureBoxMid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMid_MouseUp);
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
            this.pictureBoxUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxUp_MouseDown);
            this.pictureBoxUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxUp_MouseUp);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.CausesValidation = false;
            this.trackBarSpeed.Location = new System.Drawing.Point(17, 190);
            this.trackBarSpeed.Maximum = 100;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(150, 45);
            this.trackBarSpeed.TabIndex = 10;
            this.trackBarSpeed.Value = 20;
            this.trackBarSpeed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarSpeed_MouseUp);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(14, 174);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(121, 13);
            this.speedLabel.TabIndex = 11;
            this.speedLabel.Text = "c\'t Bot Geschwindigkeit:";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(17, 228);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(106, 13);
            this.levelLabel.TabIndex = 12;
            this.levelLabel.Text = "EEG-Empfindlichkeit:";
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.Location = new System.Drawing.Point(17, 244);
            this.trackBarLevel.Maximum = 9;
            this.trackBarLevel.Minimum = 1;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.Size = new System.Drawing.Size(150, 45);
            this.trackBarLevel.TabIndex = 13;
            this.trackBarLevel.Value = 5;
            this.trackBarLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarLevel_MouseUp);
            // 
            // startRunningWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 286);
            this.Controls.Add(this.trackBarLevel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.pictureBoxRight);
            this.Controls.Add(this.pictureBoxDown);
            this.Controls.Add(this.pictureBoxLeft);
            this.Controls.Add(this.pictureBoxMid);
            this.Controls.Add(this.pictureBoxUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "startRunningWindow";
            this.Text = "Steuerung";
            this.Load += new System.EventHandler(this.startRunningWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxUp;
        private System.Windows.Forms.PictureBox pictureBoxMid;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxDown;
        private System.Windows.Forms.PictureBox pictureBoxRight;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.TrackBar trackBarLevel;
    }
}