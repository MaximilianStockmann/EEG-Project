using System;
using System.Windows.Forms;

namespace runningWindow
{
    public partial class startRunningWindow : Form
    {
        public startRunningWindow()
        {
            InitializeComponent();
        }

        private void startRunningWindow_Load(object sender, EventArgs e)
        {
            TCP.sendCommand("speed " + trackBarSpeed.Value);
        }

        private void pictureBoxUp_MouseDown(object sender, System.EventArgs e)
        {
            pictureBoxUp.Image = WindowsFormsApplication1.Properties.Resources.up;
            TCP.sendCommand("forward");
        }
        private void pictureBoxUp_MouseUp(object sender, System.EventArgs e)
        {
            pictureBoxUp.Image = WindowsFormsApplication1.Properties.Resources.upO;
            TCP.sendCommand("stop");
        }
        private void pictureBoxRight_MouseDown(object sender, System.EventArgs e)
        {
            pictureBoxRight.Image = WindowsFormsApplication1.Properties.Resources.right;
            TCP.sendCommand("right");
        }
        private void pictureBoxRight_MouseUp(object sender, System.EventArgs e)
        {
            pictureBoxRight.Image = WindowsFormsApplication1.Properties.Resources.rightO;
            TCP.sendCommand("stop");
        }
        private void pictureBoxDown_MouseDown(object sender, System.EventArgs e)
        {
            pictureBoxDown.Image = WindowsFormsApplication1.Properties.Resources.down;
            TCP.sendCommand("backward");
        }
        private void pictureBoxDown_MouseUp(object sender, System.EventArgs e)
        {
            pictureBoxDown.Image = WindowsFormsApplication1.Properties.Resources.downO;
            TCP.sendCommand("stop");
        }
        private void pictureBoxLeft_MouseDown(object sender, System.EventArgs e)
        {
            pictureBoxLeft.Image = WindowsFormsApplication1.Properties.Resources.left;
            TCP.sendCommand("left");
        }
        private void pictureBoxLeft_MouseUp(object sender, System.EventArgs e)
        {
            pictureBoxLeft.Image = WindowsFormsApplication1.Properties.Resources.leftO;
            TCP.sendCommand("stop");
        }
        private void pictureBoxMid_MouseDown(object sender, System.EventArgs e)
        {
            pictureBoxMid.Image = WindowsFormsApplication1.Properties.Resources.mid;
            TCP.sendCommand("stop");
        }
        private void pictureBoxMid_MouseUp(object sender, System.EventArgs e)
        {
            pictureBoxMid.Image = WindowsFormsApplication1.Properties.Resources.midO;
        }

        private void trackBarSpeed_MouseUp(object sender, EventArgs e)
        {
            TCP.sendCommand("speed " + trackBarSpeed.Value);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            GUI_Namespace.Program.mainWindow.closeRunning();
            
        }
    }
}
