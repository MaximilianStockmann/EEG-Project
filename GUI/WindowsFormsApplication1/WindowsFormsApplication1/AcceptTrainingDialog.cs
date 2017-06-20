using System;
using System.Windows.Forms;
using Emotiv;

namespace acceptTraining
{
    public partial class acceptTrainingDialog : Form
    {
        public EmoEngine engine;

        public acceptTrainingDialog()
        {
            InitializeComponent();
        }

        private void acceptTrainingDialog_Load(object sender, EventArgs e)
        {
            engine = EmoEngine.Instance;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
            engine.ProcessEvents();
            this.Close();
        }

        private void delineButton_Click(object sender, EventArgs e)
        {
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
            engine.ProcessEvents();
            this.Close();
        }
    }
}
