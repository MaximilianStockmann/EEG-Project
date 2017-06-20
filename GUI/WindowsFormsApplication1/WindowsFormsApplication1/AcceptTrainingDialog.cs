using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emotiv;

namespace acceptTraining
{
    public partial class acceptTrainingDialog : Form
    {
        public EmoEngine engine = EmoEngine.Instance;

        public acceptTrainingDialog()
        {
            InitializeComponent();
        }

        private void acceptTrainingDialog_Load(object sender, EventArgs e)
        {

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            engine.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
            engine.ProcessEvents(5);
            this.Close();
        }

        private void delineButton_Click(object sender, EventArgs e)
        {
            engine.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
            engine.ProcessEvents(5);
            this.Close();
        }
    }
}
