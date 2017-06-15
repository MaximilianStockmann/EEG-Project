using Emotiv;
using System;
using System.Windows.Forms;
using acceptTraining;
using runningWindow;


namespace GUI_Namespace
{
    public partial class MainWindow : Form
    {
        // Getting SDK-Instance (EmoEngine is a Singleton)
        static EmoEngine engine = EmoEngine.Instance;

        // driving related information
        static EdkDll.IEE_MentalCommandAction_t currentAction;
        static bool drivingAllowed = false;  //always check, before sending a command
        static String currentCommand;

        // Cloud-Profile related information
        static int userCloudID = 0;
        static string userName = "dhbw";
        static string password = "tinf12itns";
        static string profileName = "Stefan Doing Stuff";
        static int version = -1; // Lastest version

        //TCP infos
        static Int32 port = 13337;
        static public String host = "192.168.1.1";

        static startRunningWindow runWin;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //passing event handlers to the engine
            engine.EmoEngineConnected +=
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded +=
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved +=
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
            engine.MentalCommandTrainingStarted +=
                new EmoEngine.MentalCommandTrainingStartedEventEventHandler(engine_MentalCommandTrainingStartedEvent);
            engine.MentalCommandTrainingSucceeded +=
                new EmoEngine.MentalCommandTrainingSucceededEventHandler(engine_MentalCommandTrainingSucceeded);
            engine.MentalCommandTrainingFailed +=
                new EmoEngine.MentalCommandTrainingFailedEventHandler(engine_MentalCommandTrainingFailed);
            engine.MentalCommandTrainingCompleted +=
                new EmoEngine.MentalCommandTrainingCompletedEventHandler(engine_MentalCommandTrainingCompleted);
            engine.MentalCommandEmoStateUpdated +=
                new EmoEngine.MentalCommandEmoStateUpdatedEventHandler(engine_MentalCommandEmoStateUpdated);

            engine.Connect();
            eegStatusLB.Items.Add("engineConnect aufgerufen.");

            if (EmotivCloudClient.EC_Connect() != EdkDll.EDK_OK)
            {
                eegStatusLB.Items.Add("Cannot connect to Emotiv Cloud.");
            }

            if (EmotivCloudClient.EC_Login(userName, password) != EdkDll.EDK_OK)
            {
                eegStatusLB.Items.Add("Your login attempt has failed. The username or password may be incorrect");
            }

            eegStatusLB.Items.Add("Logged in as " + userName);

            if (EmotivCloudClient.EC_GetUserDetail(ref userCloudID) != EdkDll.EDK_OK)
                eegStatusLB.Items.Add("Unknown stuff destroying shit.");

            // setting mentalCommandActive actions for new user profile
            ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
            ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
            ulong action3 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
            ulong action4 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PULL;
            ulong listAction = action1 | action2 | action3 | action4;
            EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);

            TCP.setServerLostCallBack(serverLostCallBack);
            ipLabel.Text = "IP: "+host;

            eegTicker.Enabled = true;
        }

        private void sendCommand(String str)
        {
            if (TCP.sendCommand(str))
                ctBotStatusLabel.Text = "JA";
            else
               ctBotStatusLabel.Text = "Keine Verbindung!";
        }

        private void serverLostCallBack()
        {
            System.Windows.Forms.MessageBox.Show("c't Bot Verbindung verloren!");
            runWin.Close();
        }

        public void closeRunning()
        {
            driveButton.Text = "Fahren";
            ctBotStatusLabel.Text = "NEIN";
            TCP.closeConnection();
        }

        private void driveButton_Click(object sender, EventArgs e)
        {
            //ctBotStatusLabel.Text = "Test";
            if (driveButton.Text == "Fahren")
            {
                //driveButton.Text = "Connect to c't Bot..."; //aktualisiert nicht ???
                if (TCP.init(host, port))
                {
                    ctBotStatusLabel.Text = "JA";
                    driveButton.Text = "Fahren Beenden";
                    drivingAllowed = true;
                    runWin = new startRunningWindow();
                    runWin.Show();
                }
                else
                {
                    ctBotStatusLabel.Text = "Kein Server!";
                    drivingAllowed = false;
                     // has to be in the if-branch
                }
                
            }
            else
            {
                driveButton.Text = "Fahren";
                ctBotStatusLabel.Text = "NEIN";
                drivingAllowed = false;
                TCP.closeConnection();
                runWin.Close();
            }

        }

        //save cloud profile
        private void save()
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);

            int profileID = -1;
            EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

            if (profileID >= 0)
            {
                engineStatusLabel.Text = "Saving profile..."; // Saving...
                if (EmotivCloudClient.EC_UpdateUserProfile(userCloudID, 0, profileID) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profile saved"; // Saving finished
                else
                    engineStatusLabel.Text = "Saving failed"; // Saving failed
            }
            else
            {
                // a new profile gets created
                engineStatusLabel.Text = "Saving profile..."; // Saving...

                if (EmotivCloudClient.EC_SaveUserProfile(userCloudID, (int)0, profileName,
                EmotivCloudClient.profileFileType.TRAINING) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profile saved"; // Saving finished
                else
                    engineStatusLabel.Text = "Saving failed"; // Saving failed
            }

            return;
        }

        //load cloud profile
        private void load()
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);

            if (getNumberProfile > 0)
            {
                engineStatusLabel.Text = "Loading profile..."; // Loading...

                int profileID = -1;
                EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

                if (EmotivCloudClient.EC_LoadUserProfile(userCloudID, 0, profileID, version) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profile loaded"; // Loading finished
                else
                    engineStatusLabel.Text = "Loading failed"; // Loading failed
            }
            return;
        }

        //event handlers for the engine
        public void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("Engine Connected");
        }

        public void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("Engine Disconnected");
        }

        public void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("User added");
        }

        public void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("User removed");
        }

        public void engine_MentalCommandTrainingStartedEvent(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training started";
        }

        public void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            new acceptTraining.acceptTrainingDialog(); // opens dialog
        }

        public void engine_MentalCommandTrainingFailed(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training failed";
        }

        public void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training completed";
        }

        public void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            currentAction = es.MentalCommandGetCurrentAction();
            currentCommand = currentActionToString();
            if (drivingAllowed)
            {
                sendCommand(currentCommand);
            }
        }

        //returns the command to send to the bot
        private String currentActionToString()
        {
            switch(currentAction)
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
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PUSH);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PULL);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_LEFT);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_RIGHT);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
        }

        private void trainActionButton_Click(object sender, EventArgs e)
        {             
            switch (trainActionSelectionComboBox.Text)
            {
                case "Stop":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Forward":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PUSH);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "backward":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PULL);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Left":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_LEFT);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Right":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_RIGHT);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                default:
                    break;
            }


        }

        private void ipLabel_Click(object sender, EventArgs e)
        {
            new changeIPWindow.startChangeIPWindow().ShowDialog();

        }

        public void setIPLabel(String s)
        {
            ipLabel.Text = s;
        }

        private void resetProfileButton_Click_1(object sender, EventArgs e)
        {

        }

        private void newProfileButton_Click(object sender, EventArgs e)
        {
            new NewProfileDialog.NewProfileDialog().ShowDialog();
        }

        public bool addNewProfile(String s)
        {
            if (!profileSelectionComboBox.Items.Contains(s) && (s != ""))
            {
                profileSelectionComboBox.Items.Add(s);
                return true;
            }
            else
                return false;

        }

        private void connectionLabel_Click(object sender, EventArgs e)
        {

        }

        private void eegTicker_Tick(object sender, EventArgs e)
        {
            engine.ProcessEvents();
        }
    }
}
