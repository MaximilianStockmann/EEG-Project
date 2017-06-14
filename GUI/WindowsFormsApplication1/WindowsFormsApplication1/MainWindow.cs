using Emotiv;
using System;
using System.Windows.Forms;


namespace GUI_Namespace
{
    public partial class MainWindow : Form
    {
        // driving related information
        static bool drivingAllowed = false;  //always check, before sending a command

        static EEG_EventSubscriber EEGListener = new EEG_EventSubscriber();

        //TCP infos
        static Int32 port = 13337;
        static String host = "127.0.0.1";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            /*public event EventHandler TrainingStarted = delegate { };
        public event EventHandler TrainingSucceeded = delegate { };
        public event EventHandler TrainingCompleted = delegate { };
        public event EventHandler TrainingRejected = delegate { };
        public event EventHandler CommandNeutral = delegate { };
        public event EventHandler CommandLeft = delegate { };
        public event EventHandler CommandRight = delegate { };
        public event EventHandler CommandForward = delegate { };
        public event EventHandler CommandBackward = delegate { };
        public event EventHandler DongleConnected = delegate { };
        public event EventHandler DongleDisconnected = delegate { };
        public event EventHandler EngineConnected = delegate { };
        public event EventHandler EngineDisconnected = delegate { };
        */

            mainLB.Items.Add("Stuff is starting.");
            EEGListener.AssignTrainingStarted(new Action(TrainingStarted));
            EEGListener.AssignTrainingSucceeded(new Action(TrainingSucceeded));
            EEGListener.AssignTrainingCompleted(new Action(TrainingCompleted));
            EEGListener.AssignTrainingRejected(new Action(TrainingRejected));
            EEGListener.AssignCommandNeutral(new Action(CommandNeutral));
            EEGListener.AssignCommandLeft(new Action(CommandLeft));
            EEGListener.AssignCommandRight(new Action(CommandRight));
            EEGListener.AssignCommandForward(new Action(CommandForward));
            EEGListener.AssignCommandBackward(new Action(CommandBackward));
            EEGListener.AssignDongleConnected(new Action(DongleConnected));
            EEGListener.AssignDongleDisconnected(new Action(DongleDisconnected));
            EEGListener.AssignEngineDisconnected(new Action(EngineDisconnected));
            EEGListener.AssignEngineConnected(new Action(EngineConnected));
        }

        private void sendCommand(String str)
        {
            if (TCP.sendCommand(str))
                ctBotStatusLabel.Text = "Connection good!";
            else
                ctBotStatusLabel.Text = "Connection failed!";
        }

        private void driveButton_Click(object sender, EventArgs e)
        {
            //ctBotStatusLabel.Text = "Test";
            if (driveButton.Text == "Start Driving")
            {
                //driveButton.Text = "Connect to c't Bot..."; //aktualisiert nicht ???
                if (TCP.init(host, port))
                {
                    ctBotStatusLabel.Text = "Connected!";
                    driveButton.Text = "Stop Driving";
                    drivingAllowed = true;
                }
                else
                {
                    ctBotStatusLabel.Text = "No server found!";
                    drivingAllowed = false;
                }
                
            }
            else
            {
                driveButton.Text = "Start Driving";
                ctBotStatusLabel.Text = "No connection";
                drivingAllowed = false;
                TCP.closeConnection();
            }

        }


        /*public void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            currentAction = es.MentalCommandGetCurrentAction();
            currentCommand = currentActionToString();
            if (drivingAllowed)
            {
                sendCommand(currentCommand);
            }
        }*/


        //returns the command to send to the bot
        private String currentActionToString()
        {
            switch(EEG.getCurrentAction())
            {
                case EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL: return "stop";
                case EdkDll.IEE_MentalCommandAction_t.MC_LEFT: return "left";
                case EdkDll.IEE_MentalCommandAction_t.MC_RIGHT: return "right";
                case EdkDll.IEE_MentalCommandAction_t.MC_PUSH: return "forward";
                case EdkDll.IEE_MentalCommandAction_t.MC_PULL: return "backward";
                default: return ""; // there is no active command (this should never happen (neutral is a command), so we don't handle it)
            }
        }

        private void resetProfileButton_Click(object sender, EventArgs e)
        {
            // reset
            // EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL);
            // EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
        }

        private void trainActionButton_Click(object sender, EventArgs e)
        {         
            EEG.TrainSkill(trainActionSelectionComboBox.Text);
        }

        /*public event EventHandler TrainingStarted = delegate { };
        public event EventHandler TrainingSucceeded = delegate { };
        public event EventHandler TrainingCompleted = delegate { };
        public event EventHandler TrainingRejected = delegate { };
        public event EventHandler CommandNeutral = delegate { };
        public event EventHandler CommandLeft = delegate { };
        public event EventHandler CommandRight = delegate { };
        public event EventHandler CommandForward = delegate { };
        public event EventHandler CommandBackward = delegate { };
        public event EventHandler DongleConnected = delegate { };
        public event EventHandler DongleDisconnected = delegate { };
        public event EventHandler EngineConnected = delegate { };
        public event EventHandler EngineDisconnected = delegate { };
        */

        #region rubbish functions for listbox-output
        void TrainingStarted()
        {
            mainLB.Items.Add("TrainingStarted");
        }
        void TrainingSucceeded()
        {
            mainLB.Items.Add("TrainingSucceeded");
        }
        void TrainingCompleted()
        {
            mainLB.Items.Add("TrainingCompleted");
        }
        void TrainingRejected()
        {
            mainLB.Items.Add("TrainingRejected");
        }
        void CommandNeutral()
        {
            mainLB.Items.Add("CommandNeutral");
        }
        void CommandLeft()
        {
            mainLB.Items.Add("CommandLeft");
        }
        void CommandRight()
        {
            mainLB.Items.Add("CommandRight");
        }
        void CommandForward()
        {
            mainLB.Items.Add("CommandForward");
        }
        void CommandBackward()
        {
            mainLB.Items.Add("CommandBackward");
        }
        void DongleConnected()
        {
            mainLB.Items.Add("DongleConnected");
        }
        void DongleDisconnected()
        {
            mainLB.Items.Add("DongleDisconnected");
        }
        void EngineConnected()
        {
            mainLB.Items.Add("EngineConnected");
        }
        void EngineDisconnected()
        {
            mainLB.Items.Add("EngineDisconnected");
        }
        #endregion
    }
}
