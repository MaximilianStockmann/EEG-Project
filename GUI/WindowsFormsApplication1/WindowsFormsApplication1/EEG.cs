using System;
using System.Collections.Generic;
using Emotiv;
using System.IO;
using System.Threading;

class EEG
{
    static EmoEngine engine = EmoEngine.Instance;
    static int userCloudID = 0;
    static string userName = "";
    static string password = "";
    static string profileName = "";
    static int version = -1;
    static int allowedTime = 50;
    static EdkDll.IEE_MentalCommandAction_t currentAction;
    
    // TODO: ENUM MentalCommandAction_t to String;

    static bool init()
    {
        engine.Connect();

        if (EmotivCloudClient.EC_Connect() != EdkDll.EDK_OK)
        {
            //Console.WriteLine("Cannot connect to Emotiv Cloud.");
            return false;
        }

        if (EmotivCloudClient.EC_Login(userName, password) != EdkDll.EDK_OK)
        {
            //Console.WriteLine("Your login attempt has failed. The username or password may be incorrect");
            return false;
        }

        if (EmotivCloudClient.EC_GetUserDetail(ref userCloudID) != EdkDll.EDK_OK)
            return false;

        setActions();
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
        getSkillEngineCommands(EdkDll.IEE_MentalCommandAction_t.MC_LEFT);
        /** TODO: CALL WITH DICTIONARY via skillString
         * EdkDll.IEE_MentalCommandAction_t.MC_LEFT 
         * EdkDll.IEE_MentalCommandAction_t.MC_RIGHT
         * EdkDll.IEE_MentalCommandAction_t.MC_PUSH
         * EdkDll.IEE_MentalCommandAction_t.MC_PULL 
         * EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL */
        execute();



    }

    static void getSkillEngineCommands(EdkDll.IEE_MentalCommandAction_t action)
    {
        EmoEngine.Instance.MentalCommandSetTrainingAction(0, action);
        EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
    }

    static int getSens()
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

    /*static void Main(string[] args)
    {
        if (!init())
        {
            return;
        }
        //BS
        try
        {
            engine.ProcessEvents(5);
        }
        catch (EmoEngineException e)
        {
            Console.WriteLine("{0}", e.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
        }


        close();

    }*/
}
