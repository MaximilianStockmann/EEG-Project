using Emotiv;
using System;
using System.Windows.Forms;
using acceptTraining;
using runningWindow;
using System.IO;

namespace GUI_Namespace
{
    public partial class MainWindow : Form
    {
        // Creating SDK-Instance
        private EmoEngine engine;
        private System.IO.StreamWriter laggendeLogger;
        private int connectedUsers = 0;

        // driving related information
        static EdkDll.IEE_MentalCommandAction_t currentAction;
        static bool drivingAllowed = false;  //always check, before sending a command
        static String currentCommand;
        public static bool profileManagementInCloud; // true ==> cloudProfile, false ==> localeProfile

        // current Values from comboboxes
        private static EdkDll.IEE_MentalCommandAction_t selectedAction;
        private static string selectedActionString;
        private static string selectedProfile;

        // Cloud-Profile related information
        private static bool enableCloudProfile = false;
        private static int userCloudID = 0;
        private static string userName;
        private static string password;
        private static int version = -1; // Lastest version

        //TCP infos
        static Int32 port = 13337;
        static public String host = "192.168.1.1";

        // Window when driving bot, manuel driving
        static startRunningWindow runWin;

        private static String profileSavingPath = "profiles.dat";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            laggendeLogger = new System.IO.StreamWriter("MentalCommand.log");

            engine = EmoEngine.Instance;

            engine.EmoEngineConnected +=
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded +=
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved +=
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
            //engine.EmoStateUpdated +=
            //    new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            //engine.EmoEngineEmoStateUpdated +=
            //    new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
            engine.MentalCommandEmoStateUpdated +=
                new EmoEngine.MentalCommandEmoStateUpdatedEventHandler(engine_MentalCommandEmoStateUpdated);
            engine.MentalCommandTrainingStarted +=
                new EmoEngine.MentalCommandTrainingStartedEventEventHandler(engine_MentalCommandTrainingStarted);
            engine.MentalCommandTrainingSucceeded +=
                new EmoEngine.MentalCommandTrainingSucceededEventHandler(engine_MentalCommandTrainingSucceeded);
            engine.MentalCommandTrainingCompleted +=
                new EmoEngine.MentalCommandTrainingCompletedEventHandler(engine_MentalCommandTrainingCompleted);
            engine.MentalCommandTrainingRejected +=
                new EmoEngine.MentalCommandTrainingRejectedEventHandler(engine_MentalCommandTrainingRejected);

            // connecting the engine
            engine.Connect();
            laggendeLogger.WriteLine("Engine wird connected.");

            // enable Ticker
            eegTicker.Enabled = true;
            laggendeLogger.WriteLine("Ticker wird aktiviert.");

            // setting mentalCommandActive actions for new user profile
            ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
            ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
            ulong action3 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
            ulong action4 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PULL;
            ulong listAction = action1 | action2 | action3 | action4;
            //EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);
            EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);

            laggendeLogger.WriteLine("Setting Actions Processing called.");
            // init TCP stuff
            TCP.setServerLostCallBack(serverLostCallBack);
            ipLabel.Text = host;
            
            

            // init File Stream for saving profiles and read profiles
            readProfiles();

            // select first ComboBox entrys
            trainActionSelectionComboBox.SelectedIndex = 0;
            if(profileSelectionComboBox.Items.Count > 0)
                profileSelectionComboBox.SelectedIndex = 0;

            

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeProfiles();
            laggendeLogger.WriteLine("Engine wird disconnected.");
            engine.Disconnect();
            laggendeLogger.Flush();
            laggendeLogger.Close();
        }

        #region List of Local Profiles
        private void readProfiles()
        {
            try
            {
                StreamReader SR = new StreamReader(profileSavingPath);
                while (SR.Peek() >= 0)
                {
                    profileSelectionComboBox.Items.Add(SR.ReadLine());
                }
                SR.Close();

            }
            catch(Exception)
            { }
        }

        private void writeProfiles()
        {
            try
            {
                if (File.Exists(profileSavingPath))
                {
                    File.Delete(profileSavingPath);
                }
                var items = profileSelectionComboBox.Items;
                StreamWriter SW = new StreamWriter(profileSavingPath);
                
                foreach(var item in profileSelectionComboBox.Items)
                {
                    SW.WriteLine(item.ToString());
                }
                SW.Flush();
                SW.Close();
            }
            catch (Exception)
            { }

        }

        //private void CloudProfileConnect()
        //{
        //    // func in func ... WTF dosen't work in VS 13
        //    bool cloudConnection()
        //    {
        //        if (EmotivCloudClient.EC_Connect() != EdkDll.EDK_OK)
        //        {
        //            laggendeLogger.WriteLine("Kann keine Verbindung zur Emotiv-Cloud herstellen.");
        //            return false;
        //        }

        //        if (EmotivCloudClient.EC_Login(userName, password) != EdkDll.EDK_OK)
        //        {
        //            laggendeLogger.WriteLine("Login fehlgeschlagen, falscher Benutzername oder falsches Passwort.");
        //            return false;
        //        }

        //        if (EmotivCloudClient.EC_GetUserDetail(ref userCloudID) != EdkDll.EDK_OK)
        //        {
        //            laggendeLogger.WriteLine("Userdetail verursacht Probleme.");
        //            return false;
        //        }

        //        laggendeLogger.WriteLine("Verbunden mit Emotiv-Cloud. BN: " + userName);
        //        return true;
        //    }

        //    enableCloudProfile = cloudConnection();
        //}
        #endregion

        #region TCP Communication with Bot
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
                    ctBotStatusLabel.Text = "Kein c't Bot!";
                    drivingAllowed = false;
                    System.Windows.Forms.MessageBox.Show("Kein c't Bot gefunden!");
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
        #endregion

        #region Event-Handlers for engine
        public void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Engine ist jetzt connected.");
        }

        public void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Engine ist jetzt disconnected.");
        }

        public void enableTrainingButtons(bool mode)
        {
            trainActionButton.Enabled = mode;
            resetActionButton.Enabled = mode;
        }

        public void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            if (++connectedUsers == 1)
                enableTrainingButtons(true);
            laggendeLogger.WriteLine("User added. {0} User(s) found.", connectedUsers);

        }

        public void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            if (--connectedUsers == 0)
                enableTrainingButtons(false);
            laggendeLogger.WriteLine("User removed. {0} User(s) found.", connectedUsers);
        }

        public void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            Single timeFromStart = es.GetTimeFromStart();
        }

        public void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            Single timeFromStart = es.GetTimeFromStart();
            Int32 headsetOn = es.GetHeadsetOn();
            EdkDll.IEE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();
            Int32 chargeLevel = 0;
            Int32 maxChargeLevel = 0;
            es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);
        }

        public void engine_MentalCommandTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Training begonnen.");
            engineStatusLabel.Text = "Training läuft, bitte konzentrieren!";
        }

        public void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Training fertig.");
            engineStatusLabel.Text = "Training abgeschlossen, annehmen?";
            //new acceptTraining.acceptTrainingDialog(); // opens dialog

            DialogResult dialogResult = MessageBox.Show("Accept Training", "Using Training Data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
                //do something else
            }
        }

        public void engine_MentalCommandTrainingFailed(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Training fehlgeschlagen.");
        }

        public void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Training wurde angenommen. Training abgeschlossen.");
            engineStatusLabel.Text = "Training wurde verarbeitet!";
        }

        public void engine_MentalCommandTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            laggendeLogger.WriteLine("Training wurde abgelehnt. Training abgeschlossen.");
            engineStatusLabel.Text = "Training wurde abgelehnt!";
        }

        public void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            EdkDll.IEE_MentalCommandAction_t nextAction = es.MentalCommandGetCurrentAction();
            if (nextAction != currentAction)
            {
                currentAction = nextAction;
                currentCommand = currentActionToString();
                if (drivingAllowed)
                {
                    sendCommand(currentCommand);
                }
            }
        }
        #endregion

        #region Saving and Loading profile data from both cloud or local file
        private bool CloudSavingLoadingFunction(int mode)
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);
            if (mode == 0) // save
            {
                int profileID = -1;
                EmotivCloudClient.EC_GetProfileId(userCloudID, selectedProfile, ref profileID);

                if (profileID >= 0) // true -> profile exists -> update
                    return (EmotivCloudClient.EC_UpdateUserProfile(userCloudID, 0, profileID) == EdkDll.EDK_OK);
                else
                    return (EmotivCloudClient.EC_SaveUserProfile(userCloudID, (int)0, selectedProfile,
                        EmotivCloudClient.profileFileType.TRAINING) == EdkDll.EDK_OK);
            }
            else if (mode == 1) // load
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

        private bool LocalSavingLoadingFunction(int mode)
        {
            if (mode == 0) // save
            {
                EdkDll.IEE_SaveUserProfile((uint)userCloudID, selectedProfile);
            }
            else if (mode == 1) // load
            {
                EdkDll.IEE_LoadUserProfile((uint)userCloudID, selectedProfile);
            }
            return true;
        }

        private bool SaveProfile()
        {
            return (enableCloudProfile ? CloudSavingLoadingFunction(0) : LocalSavingLoadingFunction(0));
        }

        private bool LoadProfile()
        {
            return (enableCloudProfile ? CloudSavingLoadingFunction(1) : LocalSavingLoadingFunction(1));
        }

        private void loadProfileButton_Click(object sender, EventArgs e)
        {
            laggendeLogger.WriteLine("Ladenvorgang angefragt.");
            engineStatusLabel.Text = (LoadProfile() ? "Ladenvorgang läuft." : "Laden hat nicht funktioniert.");
        }

        private void saveProfileButton_Click(object sender, EventArgs e)
        {
            laggendeLogger.WriteLine("Speichervorgang angefragt.");
            engineStatusLabel.Text = (SaveProfile() ? "Speichervorgang läuft." : "Speichern hat nicht funktioniert.");
        }
        #endregion

        // returns the command to send to the bot
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

        // unsorted ClickEvent-Handler
        private void trainActionButton_Click(object sender, EventArgs e)
        {
            laggendeLogger.WriteLine("Versuche " + selectedActionString + " zu trainieren.");

            EmoEngine.Instance.MentalCommandSetTrainingAction(0, selectedAction);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
            engine.ProcessEvents();
        }


        private void ipLabel_Click(object sender, EventArgs e)
        {
            new changeIPWindow.startChangeIPWindow().ShowDialog();

        }

        public void setIPLabel(String s)
        {
            ipLabel.Text = s;
        }

        private void resetActionButton_Click(object sender, EventArgs e)
        {
            laggendeLogger.WriteLine("Lösche Training für " + selectedActionString + ".");

            EmoEngine.Instance.MentalCommandSetTrainingAction(0, selectedAction);
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
            engine.ProcessEvents();
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
                profileSelectionComboBox.SelectedItem = s;
                return true;
            }
            else
                return false;

        }

        private void eegTicker_Tick(object sender, EventArgs e)
        {
            engine.ProcessEvents();
        }

        private void trainActionSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selAct = trainActionSelectionComboBox.SelectedItem.ToString();
            switch (selAct)
            {
                case "Stop / Neutral":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL;
                    break;
                case "Vorwärts":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
                    break;
                case "Rückwärts":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_PULL;
                    break;
                case "Rechts":
                    selectedActionString = selAct;
                    selectedAction = EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
                    break;
                case "Links":
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
