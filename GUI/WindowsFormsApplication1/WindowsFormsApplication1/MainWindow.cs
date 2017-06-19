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
        static EmoEngine engine = EmoEngine.Instance;

        // driving related information
        static EdkDll.IEE_MentalCommandAction_t currentAction;
        static bool drivingAllowed = false;  //always check, before sending a command
        static String currentCommand;

        // Cloud-Profile related information
        static int userCloudID = 0;
        static string userName = "";
        static string password = "";
        static string profileName = "Stefan Doing Stuff";
        static int version = -1; // Lastest version

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
            // setting mentalCommandActive actions for new user profile
            ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
            ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
            ulong action3 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
            ulong action4 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PULL;
            ulong listAction = action1 | action2 | action3 | action4;
            EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);

            //passing event handlers to the engine
            engine.EmoEngineConnected += 
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += 
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
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

            // init TCP stuff
            TCP.setServerLostCallBack(serverLostCallBack);
            ipLabel.Text = "IP: "+host;
            
            

            // init File Stream for saving profiles and read profiles
            readProfiles();

            // select first ComboBox entrys
            trainActionSelectionComboBox.SelectedIndex = 0;
            profileSelectionComboBox.SelectedIndex = 0;

            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            writeProfiles();
        }

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
                SW.Close();
            }
            catch (Exception)
            { }
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

        //save cloud profile
        private void save()
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);

            int profileID = -1;
            EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

            if (profileID >= 0)
            {
                engineStatusLabel.Text = "Speichere Profil..."; // Saving...
                if (EmotivCloudClient.EC_UpdateUserProfile(userCloudID, 0, profileID) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profil gespeichert"; // Saving finished
                else
                    engineStatusLabel.Text = "Speichern gescheitert"; // Saving failed
            }
            else
            {
                // a new profile gets created
                engineStatusLabel.Text = "Speichere Profil..."; // Saving...

                if (EmotivCloudClient.EC_SaveUserProfile(userCloudID, (int)0, profileName,
                EmotivCloudClient.profileFileType.TRAINING) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profil gespeichert"; // Saving finished
                else
                    engineStatusLabel.Text = "Speichern gescheitert"; // Saving failed
            }

            return;
        }

        //load cloud profile
        private void load()
        {
            int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);

            if (getNumberProfile > 0)
            {
                engineStatusLabel.Text = "Lade Profil..."; // Loading...

                int profileID = -1;
                EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

                if (EmotivCloudClient.EC_LoadUserProfile(userCloudID, 0, profileID, version) == EdkDll.EDK_OK)
                    engineStatusLabel.Text = "Profil geladen"; // Loading finished
                else
                    engineStatusLabel.Text = "Laden gescheitert"; // Loading failed
            }
            return;
        }

        //event handlers for the engine
        public void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "EEG verbunden";
        }

        public void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "EEG getrennt";
        }

        public void engine_MentalCommandTrainingStartedEvent(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training gestartet";
        }

        public void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            new acceptTraining.acceptTrainingDialog().ShowDialog(); // opens dialog
        }

        public void engine_MentalCommandTrainingFailed(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training gescheitert";
        }

        public void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            engineStatusLabel.Text = "Training fertig";
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
                case "Stop / Neutral":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Vorwärts":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PUSH);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Rückwärts":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PULL);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Links":
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_LEFT);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case "Rechts":
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

        private void resetActionButton_Click(object sender, EventArgs e)
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
                profileSelectionComboBox.SelectedItem = s;
                return true;
            }
            else
                return false;

        }

    }
}
