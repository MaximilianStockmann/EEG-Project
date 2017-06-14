using System;
using System.Collections.Generic;
using Emotiv;
using System.IO;
using System.Threading;

class EEG
{
    private static EmoEngine engine = EmoEngine.Instance;

    #region Stuff related to Cloud-Profile-Management
    private static string userName = "";
    private static string password = "";
    private static int userCloudID = 0;
    private static int version = -1;
    #endregion

    private static string profileName = ""; // name of the current-Profile
    private static int allowedTime = 50; // max number of ms for processing EmoengineEvents
    private static bool initialized = false;
    private static bool drivingAllowed = false;

    private static string currentAction;
    private static Dictionary<string, EdkDll.IEE_MentalCommandAction_t> skillDictionary =
        new Dictionary<string, EdkDll.IEE_MentalCommandAction_t>()
    {
        { "neutral", EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL },
        { "backward", EdkDll.IEE_MentalCommandAction_t.MC_PULL },
        { "forward", EdkDll.IEE_MentalCommandAction_t.MC_PUSH },
        { "right", EdkDll.IEE_MentalCommandAction_t.MC_RIGHT },
        { "left", EdkDll.IEE_MentalCommandAction_t.MC_LEFT }
    };

    static void build(int mode)
    {
        if (!initialized)
        {
            engine.Connect();
            switch (mode)
            {
                case 0: initialized = true; break;
                case 1: initialized = cloudConnectWorked(); break;
                default: break;
            }
            assignHandlers();
        }
        else
        {
            close();
            build(mode);
        }
    }

    static bool cloudConnectWorked()
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

    static void setActions()
    {
        ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
        ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
        ulong action3 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PUSH;
        ulong action4 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_PULL;
        ulong listAction = action1 | action2 | action3 | action4;
        EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);
    }

    static void close()
    {
        engine.Disconnect();
        initialized = false;
    }

    static void execute()
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

    static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_UserAdded(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_UserRemoved(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
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

    static void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
    {
        EmoState es = e.emoState;
        Single timeFromStart = es.GetTimeFromStart();
        EdkDll.IEE_MentalCommandAction_t cogAction = es.MentalCommandGetCurrentAction();
        Single power = es.MentalCommandGetCurrentActionPower();
        Boolean isActive = es.MentalCommandIsActive();

        currentAction = cogAction;
    }

    static void engine_MentalCommandTrainingStarted(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
    {
        Console.WriteLine("MentalCommand Training Success. (A)ccept/Reject?");
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.A)
        {
            Console.WriteLine("Training Accepted.");
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
        }
        else
        {
            Console.WriteLine("Training Rejected.");
            EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
        }
    }

    static void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
    {
    }

    static void engine_MentalCommandTrainingRejected(object sender, EmoEngineEventArgs e)
    {
    }


    static void SavingLoadingFunction(int mode)
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

    static void load()
    {
        SavingLoadingFunction(1);
    }

    static void save()
    {
        SavingLoadingFunction(0);
    }

    static void trainSkill(string skillString)
    {
        EmoEngine.Instance.MentalCommandSetTrainingAction(0, skillDictionary[skillString]);
        EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
        execute();
    }

    static int getSensitivity()
    {
        return EmoEngine.Instance.MentalCommandGetActivationLevel(0);
    }

    static float getSkillRating()
    {
        return EmoEngine.Instance.MentalCommandGetOverallSkillRating(0);
    }

    static void setSens(int dest)
    {
        if (dest > 0 && dest <= 10)
        {
            EmoEngine.Instance.MentalCommandSetActivationLevel(0, dest);
            execute();
        }
    }

    static void setDrivingStatus(bool whatItIsSupposedToBe)
    {
        drivingAllowed = whatItIsSupposedToBe;
    }

    static void assignHandlers()
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
