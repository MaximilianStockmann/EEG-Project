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
using Emotiv

namespace WindowsFormsApplication1
{
    public partial class MainWindow : Form
    {
        // Marcel
        static Int32 port = 13337;
        static TcpClient client;
        static NetworkStream clientStream;

        static EmoEngine engine = EmoEngine.Instance;
        static String currentCommand;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //passing the event handlers to the engine
            engine.EmoEngineConnected += engine_EmoEngineConnectedEventHandler;
            engine.EmoEngineDisconnected += engine_EmoEngineDisconnectedEventHandler;
            engine.MentalCommandTrainingStarted += engine_MentalCommandTrainingStartedEventEventHandler;
            engine.MentalCommandTrainingSucceeded += engine_MentalCommandTrainingSucceededEventHandler;
            engine.MentalCommandTrainingFailed += engine_MentalCommandTrainingFailedEventHandler;
            engine.MentalCommandTrainingCompleted += engine_MentalCommandTrainingCompletedEventHandler;
            engine.MentalCommandEmoStateUpdated += engine_MentalCommandEmoStateUpdatedEventHandler;
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


        //event handlers for the engine
        public void engine_EmoEngineConnectedEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        public void engine_EmoEngineDisconnectedEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        public void engine_MentalCommandTrainingStartedEventEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        public void engine_MentalCommandTrainingSucceededEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        public void engine_MentalCommandTrainingFailedEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        public void engine_MentalCommandTrainingCompletedEventHandler(object sender, EmoEngineEventArgs e)
        {
            //todo...
        }

        //note the different param
        public void engine_MentalCommandEmoStateUpdatedEventHandler(object sender, EmoStateUpdatedEventArgs e)
        {
            //todo...
        }
    }
}
