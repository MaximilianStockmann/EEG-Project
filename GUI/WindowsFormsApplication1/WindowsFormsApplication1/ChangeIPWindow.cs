using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace changeIPWindow
{
    public partial class startChangeIPWindow : Form
    {
        public startChangeIPWindow()
        {
            InitializeComponent();
        }

        private void startChangeIPWindow_Load(object sender, EventArgs e)
        {
            this.textBoxIP.Text = GUI_Namespace.MainWindow.host;
        }

        private void textBoxIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GUI_Namespace.MainWindow.host = this.textBoxIP.Text;
                GUI_Namespace.Program.mainWindow.setIPLabel(this.textBoxIP.Text);
                this.Close();
            }
        }
    }
}
