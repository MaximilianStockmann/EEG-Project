using Emotiv;
using System;
using System.Windows.Forms;
using acceptTraining;
using runningWindow;


namespace GUI_Namespace
{
    public partial class MainWindow : Form
    {
        public EmoEngine engine = EmoEngine.Instance;

        // driving related information
        public static EdkDll.IEE_MentalCommandAction_t currentAction;
        public static bool drivingAllowed = false;  //always check, before sending a command
        public static String currentCommand;
        public static bool profileManagementInCloud; // true ==> cloudProfile, false ==> localeProfile

        // current Values from comboboxes
        private static EdkDll.IEE_MentalCommandAction_t selectedAction;
        private static string selectedActionString;
        private static string selectedProfile;

        // Cloud-Profile related information
        private static int userCloudID = 0;
        private static string userName = "";
        private static string password = "";
        private static int version = -1; // Lastest version

        // TCP infos
        public static Int32 port = 13337;
        public static String host = "192.168.1.1";

        static startRunningWindow runWin;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // passing event handlers to the engine
            engine.EmoEngineConnected +=
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded +=
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved +=
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
            engine.EmoStateUpdated +=
                new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.EmoEngineEmoStateUpdated +=
                new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
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

            // connecting the engine
            engine.Connect();

            // give Feedback in case of not working CloudProfiling
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
            //EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);
            engine.MentalCommandSetActiveActions(0, listAction);
            engine.ProcessEvents(5);

            TCP.setServerLostCallBack(serverLostCallBack);
            ipLabel.Text = "IP: "+host;

            // eegTicker.Enabled = true;

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

        public void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            Single timeFromStart = es.GetTimeFromStart();
        }

        static void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            Int32 headsetOn = es.GetHeadsetOn();

            EdkDll.IEE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();
            Int32 chargeLevel = 0;
            Int32 maxChargeLevel = 0;
            es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);
        }

        public void engine_MentalCommandTrainingStartedEvent(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("Training started");
        }

        public void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            new acceptTraining.acceptTrainingDialog(); // opens dialog
        }

        public void engine_MentalCommandTrainingFailed(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("Training failed");
        }

        public void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            eegStatusLB.Items.Add("Training completed");
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

        private bool SavingLoadingFunction(int mode)
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);
            if (mode == 0)
            {
                int profileID = -1;
                EmotivCloudClient.EC_GetProfileId(userCloudID, selectedProfile, ref profileID);

                if (profileID >= 0)
                    return (EmotivCloudClient.EC_UpdateUserProfile(userCloudID, 0, profileID) == EdkDll.EDK_OK);
                else
                    return (EmotivCloudClient.EC_SaveUserProfile(userCloudID, (int)0, selectedProfile,
                        EmotivCloudClient.profileFileType.TRAINING) == EdkDll.EDK_OK);
            }
            else if (mode == 1)
            {
                if (getNumberProfile > 0)
                {
                    int profileID = -1;
                    EmotivCloudClient.EC_GetProfileId(userCloudID, selectedProfile, ref profileID);
                    return (EmotivCloudClient.EC_LoadUserProfile(userCloudID, 0, profileID, version) == EdkDll.EDK_OK);
                }
                else return false;
            }
            else return false;
        }

        private bool SaveProfile()
        {
            return SavingLoadingFunction(0);
        }

        private bool LoadProfile()
        {
            return SavingLoadingFunction(1);
        }


        private String currentActionToString()
        {
            switch(currentAction)
            {
                case EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL:   return "stop";
                case EdkDll.IEE_MentalCommandAction_t.MC_LEFT:      return "left";
                case EdkDll.IEE_MentalCommandAction_t.MC_RIGHT:     return "right";
                case EdkDll.IEE_MentalCommandAction_t.MC_PUSH:      return "forward";
                case EdkDll.IEE_MentalCommandAction_t.MC_PULL:      return "backward";
                default: return ""; // there is no active command (this should never happen (neutral is a command), so we don't handle it)
            }
        }

        private void resetProfileButton_Click(object sender, EventArgs e)
        {
            /*EmoEngine.Instance.MentalCommandSetTrainingAction(0, selectedAction);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);*/

            engine.MentalCommandSetTrainingAction(0, selectedAction);
            engine.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            engine.ProcessEvents(5);
        }

        private void trainActionButton_Click(object sender, EventArgs e)
        {             
            EmoEngine.Instance.MentalCommandSetTrainingAction(0, selectedAction);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
            engine.ProcessEvents(5);
        }

        private void ipLabel_Click(object sender, EventArgs e)
        {
            new changeIPWindow.startChangeIPWindow().ShowDialog();
        }

        public void setIPLabel(String s)
        {
            ipLabel.Text = s;
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

        private void eegTicker_Tick(object sender, EventArgs e)
        {
        }

        private void loadProfileButton_Click(object sender, EventArgs e)
        {
            eegStatusLB.Items.Add(LoadProfile() ? "Laden hat funktioniert" : "Laden hat nicht funktioniert");
        }

        private void saveProfileButton_Click(object sender, EventArgs e)
        {
            eegStatusLB.Items.Add(SaveProfile() ? "Speichern hat funktioniert" : "Speichern hat nicht funktioniert");
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            engine.Disconnect();
        }

        private void trainActionSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selAct = trainActionSelectionComboBox.SelectedItem.ToString();
            switch (selAct)
            {
                case "stop":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL;
                    break;
                case "forward":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
                    break;
                case "backward":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_PULL;
                    break;
                case "right":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
                    break;
                case "left":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
                    break;
            }
        }

        private void profileSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProfile = profileSelectionComboBox.SelectedItem.ToString();
        }
    }
}
