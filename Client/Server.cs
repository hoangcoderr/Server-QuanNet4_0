using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    public static int PORT = 8080;
    public static TcpListener server = new TcpListener(IPAddress.Any, PORT);
    public static void Main()
    {

        server.Start();
        Console.WriteLine("Server started at port " + PORT);
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string str = Encoding.UTF8.GetString(data, 0, bytesRead);
            Console.WriteLine("Received from client: " + str);
            string[] receiveData = Progress.stringProcessing(str);
            Progress.processData(receiveData);
            Progress.SendData(client,Progress.sendDataToClient);
            client.Close();
        }
    }
}
