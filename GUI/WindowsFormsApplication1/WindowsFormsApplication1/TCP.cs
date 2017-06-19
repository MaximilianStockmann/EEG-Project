using System;
using System.Net.Sockets;

public class TCP
{
    static private TcpClient client;
    static private NetworkStream clientStream;
    public delegate void callBackServerLost();
    static private callBackServerLost callback;

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
        catch (ArgumentNullException)
        {
            return false;
        }
        catch (SocketException)
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
        // TODO: Try block rumdrum
        Int32 bytes;
        try
        {
            bytes = clientStream.Read(data, 0, data.Length);
        }
        catch (Exception)
        {
            return "ERROR";
        }
        return System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    }

    static public bool sendCommand(String cmd)    //Eingaben: "forward", "backward", "left", "right", "stop"
    {
        if (clientStream == null)
        {
            //"No Server to comunicate"
            return false;
        }

        Byte[] data = System.Text.Encoding.ASCII.GetBytes(cmd);

        try
        {
            clientStream.Write(data, 0, data.Length);
        }
        catch
        {
            callback();
            return false;
        }

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

    static public void setServerLostCallBack(callBackServerLost cb)
    {
        callback = cb;
    }
}
