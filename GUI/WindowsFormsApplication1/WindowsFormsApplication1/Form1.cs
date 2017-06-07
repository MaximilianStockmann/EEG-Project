﻿using Emotiv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Namespace
{
    public partial class MainWindow : Form
    {
        // Creating SDK-Instance
        //static EmoEngine engine = EmoEngine.Instance;

        // TCP-Connection to Pi
        static TcpClient client;
        static NetworkStream clientStream;
        static Int32 port = 13337;

        // Cloud-Profile related information
        static int userCloudID = 0;
        static string userName = "";
        static string password = "";
        static string profileName = "Stefan Doing Stuff";
        static int version = -1; // Lastest version
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            /*
            // Connection to Engine (from the SDK-project)
            engine.EmoEngineConnected +=
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            
            // What happens, if a Dongle is connected or disconnected.
            engine.UserAdded +=
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved +=
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);

            // Called in the Background
            engine.EmoStateUpdated +=
                new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.EmoEngineEmoStateUpdated +=
                new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);

            // What happens when a Mental Command is recognized. (Bound to static drive var)
            engine.MentalCommandEmoStateUpdated +=
                new EmoEngine.MentalCommandEmoStateUpdatedEventHandler(engine_MentalCommandEmoStateUpdated);

            // Training the Mental Commands
            engine.MentalCommandTrainingStarted += // starts the process (block)
                new EmoEngine.MentalCommandTrainingStartedEventEventHandler(engine_MentalCommandTrainingStarted);
            engine.MentalCommandTrainingSucceeded += // asks for accept/reject
                new EmoEngine.MentalCommandTrainingSucceededEventHandler(engine_MentalCommandTrainingSucceeded);
            engine.MentalCommandTrainingCompleted += // closes Block, when accepted
                new EmoEngine.MentalCommandTrainingCompletedEventHandler(engine_MentalCommandTrainingCompleted);
            engine.MentalCommandTrainingRejected += // closes Block, when rejected
                new EmoEngine.MentalCommandTrainingRejectedEventHandler(engine_MentalCommandTrainingRejected);

            // Connect the Engine
            engine.Connect();
            */
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            loadProfileButton.Text = "Hallo";
        }

        private void driveButton_Click(object sender, EventArgs e)
        {
            ctBotStatusLabel.Text = "Test";
            if (driveButton.Text == "Start Driving")
            {
                driveButton.Text = "Stop Driving";
                try
                {


                    // Initalisierung
                    client = new TcpClient("192.168.178.33", port);
                    clientStream = client.GetStream();

                    sendCommand("speed 9000");
                    


                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("ArgumentNullException: {0}", ex);
                    ctBotStatusLabel.Text = "ArgumentNullException";
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException: {0}", ex);
                    ctBotStatusLabel.Text = "SocketException";
                }


               
            }
            else
            {
                driveButton.Text = "Start Driving";
                if (clientStream == null) return;
                // Close everything.
                clientStream.Close();
                client.Close();
            }
           
        }

        private void getStatusResponse()
        {

            // Buffer to store the response bytes.
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = clientStream.Read(data, 0, data.Length);
            ctBotStatusLabel.Text = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

        }

        private void sendCommand(String cmd)
        {

            if (clientStream == null)
            {
                ctBotStatusLabel.Text = "No Server to connect";
                return;
            }

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(cmd);

            clientStream.Write(data, 0, data.Length);

            getStatusResponse();
        }

        private void trainActionButton_Click(object sender, EventArgs e)
        {

            sendCommand("left");
        }
    }

}
