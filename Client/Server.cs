using System;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Reflection;
using System.Threading;

public class Server
{
    public static int PORT = 8080;
    public static List<IWebSocketSession> ConnectedClients = new List<IWebSocketSession>();

    static void Main(string[] args)
    {
        SqlConnection.sqlConnect(SqlConnection.mySqlConnection);
        SqlProgession.loadItemFromServer(SqlConnection.mySqlConnection,Item.items);
        var wssv = new WebSocketServer($"ws://localhost:{PORT}");
        wssv.Log.Output = (data, path) => { };
        wssv.AddWebSocketService<Communication>("/Communication");
        wssv.Start();
        Console.WriteLine("Server opened at localhost/Communication");
        Console.ReadKey(true);
        wssv.Stop();
    }
    public static void DisconnectClient(string id)
    {
        var client = ConnectedClients.FirstOrDefault(c => c.ID == id);
        if (client != null)
        {
            client.Context.WebSocket.Close();
            ConnectedClients.Remove(client);
            Console.WriteLine("Client disconnected by kicked: " + id);
        }
    }
}

public class Communication : WebSocketBehavior
{
    public static Dictionary<WebSocketSessionManager, User> connectedUsers = new Dictionary<WebSocketSessionManager, User>();
    protected override void OnOpen()
    {
        Console.WriteLine("Client connected: " + ID);
        Server.ConnectedClients.Add(this);
        User user = new User();
        user.clientId = ID;
        connectedUsers.Add(Sessions, user);
    }
    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("Client disconnected: " + ID);
        Server.ConnectedClients.Remove(this);
        if (connectedUsers.ContainsKey(Sessions))
        {
            connectedUsers.Remove(Sessions);
        }
    }
    protected override void OnMessage(MessageEventArgs e)
    {

        Console.WriteLine("Server received: " + e.Data);
        string[] receiveData = Progress.stringProcessing(e.Data);
        Progress.processData(receiveData, ID);
        Send(Progress.sendBackToClient(Progress.sendDataToClient));
    }
}
