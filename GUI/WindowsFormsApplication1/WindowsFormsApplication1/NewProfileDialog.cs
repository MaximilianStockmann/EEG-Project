using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewProfileDialog
{
    public partial class NewProfileDialog : Form
    {
        public NewProfileDialog()
        {
            InitializeComponent();
        }



        private void NewProfileDialog_Load(object sender, EventArgs e)
        {

        }

        private void stashNewProfile_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmNewProfileButton_Click(object sender, EventArgs e)
        {
            if (GUI_Namespace.Program.mainWindow.addNewProfile(profileNameTextBox.Text))
                this.Close();
            else
                System.Windows.Forms.MessageBox.Show("Der Eintrag ist bereits vorhanden oder es wurde kein Name eingegeben!");
        }
    }
}
