using System;
using System.Collections.Generic;
using Emotiv;
using System.IO;
using System.Threading;

static class EEG
{
    private static EmoEngine engine = EmoEngine.Instance;

    #region Attributes, Cloud-Profile-Management
    private static string userName = "dhbw";
    private static string password = "tinf16itns";
    private static int userCloudID = 0;
    private static int version = -1;
    #endregion

    private static string profileName = "Stefan Doing Stuff"; // name of the current-Profile
    private static int allowedTime = 50; // max number of ms for processing EmoengineEvents
    private static bool initialized = false;
    private static bool trainingInProgress = false;
    private static bool drivingAllowed = false;
    private static bool userConnected = false;

    private static EdkDll.IEE_MentalCommandAction_t currentAction;
    private static Dictionary<string, EdkDll.IEE_MentalCommandAction_t> skillDictionary =
        new Dictionary<string, EdkDll.IEE_MentalCommandAction_t>()
    {
        { "Stop", EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL },
        { "backward", EdkDll.IEE_MentalCommandAction_t.MC_PULL },
        { "Forward", EdkDll.IEE_MentalCommandAction_t.MC_PUSH },
        { "Right", EdkDll.IEE_MentalCommandAction_t.MC_RIGHT },
        { "Left", EdkDll.IEE_MentalCommandAction_t.MC_LEFT }
    };

    #region Methods, EEG main initialisation and operation
    public static void Build(int mode)
    {
        if (!initialized)
        {
            AssignHandlers();
            engine.Connect();
            switch (mode)
            {
                case 0: initialized = true; break;
                case 1: initialized = CloudConnectWorked(); break;
                default: break;
            }
        }
        else
        {
            Close();
            Build(mode);
        }
    }
    private static bool CloudConnectWorked()
    {

        if (EmotivCloudClient.EC_Connect() != EdkDll.EDK_OK)
        {
            return false; // cannot connect to Emotiv Cloud
        }

        if (EmotivCloudClient.EC_Login(userName, password) != EdkDll.EDK_OK)
        {
            return false; // login failed - username or password incorrect
        }

        if (EmotivCloudClient.EC_GetUserDetail(ref userCloudID) != EdkDll.EDK_OK)
            return false; // used in Emotiv Example, seems redundant though

        return true;
    }

    private static void SetActions()
    {
        ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
        ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
        ulong action3 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
        ulong action4 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PULL;
        ulong listAction = action1 | action2 | action3 | action4;
        EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);
    }

    public static void Close()
    {
        engine.Disconnect();
        initialized = false;
    }

    private static void Execute() // voll wayne
    {
        try
        {
            engine.ProcessEvents(allowedTime);
        }
        catch (EmoEngineException e)
        {
        }
        catch (Exception e)
        {
        }
    }
    #endregion

    #region Methods, Different Eventhandler-Types
    /* public event EventHandler CommandNeutral = delegate { };
    public event EventHandler CommandLeft = delegate { };
    public event EventHandler CommandRight = delegate { };
    public event EventHandler CommandForward = delegate { };
    public event EventHandler CommandBackward = delegate { }; */

    static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_UserAdded(object sender, EmoEngineEventArgs e)
    {
        userConnected = true;
        //thrower.TriggerEvent("DongleConnected");
    }

    static void engine_UserRemoved(object sender, EmoEngineEventArgs e)
    {
        userConnected = false;
        //thrower.TriggerEvent("DongleDisConnected");
    }

    static void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
    {
        EmoState es = e.emoState;
        Single timeFromStart = es.GetTimeFromStart();
    }

    // WHAT THE FUCKING HELL IS THIS USED FOR?
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

    static void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
    {
        EmoState es = e.emoState;
        Single timeFromStart = es.GetTimeFromStart();
        EdkDll.IEE_MentalCommandAction_t cogAction = es.MentalCommandGetCurrentAction();
        Single power = es.MentalCommandGetCurrentActionPower();
        Boolean isActive = es.MentalCommandIsActive();

        // TO DO: Throw the neccessary stuff via thrower
        if (isActive)
        {
            currentAction = cogAction;
        }

        currentAction = cogAction;
    }

    static void engine_MentalCommandTrainingStarted(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_MentalCommandTrainingRejected(object sender, EmoEngineEventArgs e)
    {
    }
    #endregion

    private static void SavingLoadingFunction(int mode)
    /** Source: Emotiv Community SDK
     *  Path:   Examples-Basic > C# > MentalcommandWithCloudProfile > Program.cs
     *  URL:    http://bit.ly/2szE9FP */
    {
        int getNumberProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudID);
        if (mode == 0)
        {
            int profileID = -1;
            EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

            if (profileID >= 0)
            {
                Console.WriteLine("Profile with " + profileName + " is existed");
                Console.WriteLine("Updating....");
                if (EmotivCloudClient.EC_UpdateUserProfile(userCloudID, 0, profileID) == EdkDll.EDK_OK)
                {
                    Console.WriteLine("Updating finished");
                }
                else Console.WriteLine("Updating failed");
            }
            else
            {
                Console.WriteLine("Saving...");

                if (EmotivCloudClient.EC_SaveUserProfile(userCloudID, (int)0, profileName,
                    EmotivCloudClient.profileFileType.TRAINING) == EdkDll.EDK_OK)
                {
                    Console.WriteLine("Saving finished");
                }
                else Console.WriteLine("Saving failed");
            }

            return;
        }
        else if (mode == 1)
        {
            if (getNumberProfile > 0)
            {
                Console.WriteLine("Loading...");

                int profileID = -1;
                EmotivCloudClient.EC_GetProfileId(userCloudID, profileName, ref profileID);

                if (EmotivCloudClient.EC_LoadUserProfile(userCloudID, 0, profileID, version) == EdkDll.EDK_OK)
                    Console.WriteLine("Loading finished");
                else
                    Console.WriteLine("Loading failed");

            }

            return;
        }
    }

    public static void Load()
    {
        SavingLoadingFunction(1);
    }

    public static void Save()
    {
        SavingLoadingFunction(0);
    }

    public static void TrainSkill(string skillString)
    {
        EmoEngine.Instance.MentalCommandSetTrainingAction(0, skillDictionary[skillString]);
        EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
        trainingInProgress = true;
        Execute();
    }

    public static void AcceptTraining(bool judgement)
    {
        if (trainingInProgress)
        {
            if (judgement)
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
            }
            else
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
            }
            trainingInProgress = false;
            Execute();
        }
    }

    public static int GetSensitivity()
    {
        return EmoEngine.Instance.MentalCommandGetActivationLevel(0);
    }

    public static float GetSkillRating()
    {
        return EmoEngine.Instance.MentalCommandGetOverallSkillRating(0);
    }

    public static EdkDll.IEE_MentalCommandAction_t getCurrentAction()
    {
        return currentAction;
    }

    public static bool EngineWorking()
    {
        return initialized;
    }

    public static bool userIsConnected()
    {
        return userConnected;
    }

    public static void SetSens(int dest)
    {
        if (dest > 0 && dest <= 10)
        {
            EmoEngine.Instance.MentalCommandSetActivationLevel(0, dest);
            Execute();
        }
    }

    public static void SetDrivingStatus(bool whatItIsSupposedToBe)
    {
        drivingAllowed = whatItIsSupposedToBe;
    }

    private static void AssignHandlers()
    {
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
    }
}
