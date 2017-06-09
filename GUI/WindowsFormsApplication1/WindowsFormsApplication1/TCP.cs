using System;
using System.Net.Sockets;

public class TCP
{
    static TcpClient client;
    static NetworkStream clientStream;

    static public bool init(String host, Int32 port)
    {
        try
        {
            // Initalisierung
            client = new TcpClient(host, port);
            clientStream = client.GetStream();

            sendCommand("stop");
            return true;
        }
        catch (ArgumentNullException ex)
        {
            return false;
        }
        catch (SocketException ex)
        {
            return false;
        }
    }

    static private String getStatusResponse()
    {
        // Buffer to store the response bytes.
        Byte[] data = new Byte[256];
        // String to store the response ASCII representation.
        String responseData = String.Empty;
        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = clientStream.Read(data, 0, data.Length);
        return System.Text.Encoding.ASCII.GetString(data, 0, bytes);

        //TODO: response auslesen und Flag stellen wenn server abschmiert
        //      Rückgabewert

    }

    static public bool sendCommand(String cmd)    //Eingaben: "forward", "backward", "left", "right", "stop"
    {
        if (clientStream == null)
        {
            //"No Server to comunicate"
            return false;
        }

        Byte[] data = System.Text.Encoding.ASCII.GetBytes(cmd);

        clientStream.Write(data, 0, data.Length);

        return getStatusResponse().Equals("ok");
    }

    static public void closeConnection()
    {
        if (clientStream == null) return;
        // Close everything.
        clientStream.Close();
        clientStream = null;
        client.Close();
    }
}
