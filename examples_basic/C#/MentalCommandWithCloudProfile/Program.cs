/****************************************************************************
**
** Copyright 2016 by Emotiv. All rights reserved
** Example - Mental command with  cloud profile
** This example demonstrates how to detect mental command detection with 
** a profile.
** This example work on single connection
****************************************************************************/

using System;
using System.Collections.Generic;
using Emotiv;
using System.IO;
using System.Threading;

namespace MentalCommandWithCloudProfile
{
    class Program
    {
        static System.IO.StreamWriter cogLog = new System.IO.StreamWriter("MentalCommand.log"); // sinnloser Log

        static uint userCloudID = 0;
        static string userName = "dhbw";
        static string password = "tinf12itns";        
        static string profileName = "Profile_0";
        static int version = -1; // Lastest version
        // static EdkDll.IEE_MentalCommandAction_t currentAction = "MC_neutral....";
        // static EmoEngine engine = EmoEngine.Instance;
        // static boolean drivingAllowed = false;


        static Boolean enableLoger = false;

        // next 4 -> useful for debugging, etc. #BASIC

        static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Emoengine connected");
        }

        static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Emoengine disconnected");
        }
        static void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user added ({0})", e.userId);
        }
        static void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user removed");
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

            EdkDll.IEE_MentalCommandAction_t cogAction = es.MentalCommandGetCurrentAction(); // static EdkDll.IEE_MentalCommandAction_t currentAction ...
            Single power = es.MentalCommandGetCurrentActionPower(); // not necc., we only use one speed
            Boolean isActive = es.MentalCommandIsActive(); // not necc., due to static currentAction attribute
            Console.WriteLine("{0},{1},{2},{3}", timeFromStart, cogAction, power, isActive);
            // own logic, TCP-signal and stuff.

            cogLog.WriteLine( "{0},{1},{2},{3}", timeFromStart, cogAction, power, isActive);
            cogLog.Flush();
            if(enableLoger)
                 Console.WriteLine("{0},{1},{2},{3}", timeFromStart, cogAction, power, isActive);
        }

        static void engine_MentalCommandTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Start MentalCommand Training");
        }

        static void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Success. (A)ccept/Reject?");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                Console.WriteLine("Accept!!!");
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
            }
            else
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
            }
        }

        static void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Completed.");
        }

        static void engine_MentalCommandTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Rejected.");
        }

        static void SavingLoadingFunction(int mode) // Split up, if possible, but not necc.
        {
            if (mode == 0)
            {
                EdkDll.IEE_SaveUserProfile(userCloudID, profileName);
            }
            if (mode == 1)
            {
                EdkDll.IEE_LoadUserProfile(userCloudID, profileName);
            }
        }

        static void keyHandler(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    {
                        ulong action1 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_LEFT;
                        ulong action2 = (ulong)EdkDll.IEE_MentalCommandAction_t.MC_RIGHT;
                        ulong listAction = action1 | action2;
                        EmoEngine.Instance.MentalCommandSetActiveActions(0, listAction);
                        Console.WriteLine("Setting MentalCommand active actions for user" + profileName);
                        break;
                    }
                case ConsoleKey.F2:
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START); // MC_ERASE for clearing Training Data on a special move (prob. same Code)
                    break;
                case ConsoleKey.F3:
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_RIGHT);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case ConsoleKey.F4:
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_LEFT);
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case ConsoleKey.F5:
                    EmoEngine.Instance.MentalCommandSetActivationLevel(0, 2);
                    Console.WriteLine("MentalCommand Activateion level set to {0}", EmoEngine.Instance.MentalCommandGetActivationLevel(0));
                    break;
                case ConsoleKey.F6:
                    Console.WriteLine("MentalCommand Activateion level is {0}", EmoEngine.Instance.MentalCommandGetActivationLevel(0));
                    break;
                case ConsoleKey.F7:
                    Console.WriteLine("Get the current overall skill rating: {0}", EmoEngine.Instance.MentalCommandGetOverallSkillRating(0));
                    break;
                case ConsoleKey.F8:
                    SavingLoadingFunction(0);
                    break;
                case ConsoleKey.F9:
                    SavingLoadingFunction(1);
                    break;
                case ConsoleKey.F10:
                    enableLoger = !enableLoger;
                    break;
                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
            EmoEngine engine = EmoEngine.Instance;

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

            engine.Connect();

            Console.WriteLine("===========================================================================");
            Console.WriteLine("Example to show how to detect mental command detection with a profile.");
            Console.WriteLine("===========================================================================");

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
                       
            Console.WriteLine("Logged in as " + userName);

            while (true)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);
                        keyHandler(cki.Key);

                        if (cki.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }
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
            }
            engine.Disconnect();
        }
    }
}
