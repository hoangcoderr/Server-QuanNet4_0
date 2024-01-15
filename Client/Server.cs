using System;
using System.Net;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
public class Server
{
    public static int PORT = 8080;
    public static List<IWebSocketSession> ConnectedClients = new List<IWebSocketSession>();
    static void Main(string[] args)
    {
        var wssv = new WebSocketServer($"ws://localhost:{PORT}");
        wssv.AddWebSocketService<Echo>("/Echo");
        wssv.Start();
        Console.ReadKey(true);
        wssv.Stop();
    }
}

class Echo : WebSocketBehavior
{    protected override void OnOpen()
    {
        Console.WriteLine("Client connected: " + ID);
        Server.ConnectedClients.Add(this);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("Client disconnected: " + ID);
        Server.ConnectedClients.Remove(this);
    }
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Server received: " + e.Data); 
        string[] receiveData = Progress.stringProcessing(e.Data);
        Progress.processData(receiveData);
        Send(Progress.sendBackToClient(Progress.sendDataToClient));


    }
}
