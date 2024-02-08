using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using WebSocketSharp;

public class Progress
{
    public static string account = string.Empty;
    public static string password = string.Empty;
    public static string name = string.Empty;
    public static List<string> sendDataToClient = new List<string>();
    public static string[] stringProcessing(string st)
    {
        string[] strArray = st.Split('|');
        return strArray;
    }
    public static string clientIdToKick = string.Empty;
    public static void processData(string[] st, string id)
    {
        int type = int.Parse(st[st.Length - 1]);
        switch (type)
        {
            case 0:
                account = st[0];
                password = st[1];
                sendDataToClient = new List<string>();
                string clientUsing = string.Empty;
                SqlConnection.sqlConnect(SqlConnection.mySqlConnection);
                if (SqlProgession.IsUserAvaiable(SqlConnection.mySqlConnection, account, password, id))
                {
                    sendDataToClient.Add(0.ToString());

                    clientUsing = SqlProgession.getClientUsing(SqlConnection.mySqlConnection, account);
                    if (clientUsing != string.Empty)
                    {
                        Console.WriteLine("Kicked " + clientUsing);
                        Server.DisconnectClient(clientUsing);
                    }

                    SqlProgession.setClientForUser(SqlConnection.mySqlConnection, account, id);
                    //SqlProgession.
                }
                else
                    sendDataToClient.Add(1.ToString());
                SqlConnection.sqlClose(SqlConnection.mySqlConnection);
                sendDataToClient.Add(type.ToString());
                break;
            case 1:
                account = st[0];
                password = st[1];
                name = st[2];
                sendDataToClient = new List<string>();
                SqlConnection.sqlConnect(SqlConnection.mySqlConnection);
                if (SqlProgession.IsUserRegistered(SqlConnection.mySqlConnection, account))
                {
                    sendDataToClient.Add(1.ToString());
                }
                else
                {
                    SqlProgession.addAccountToServer(SqlConnection.mySqlConnection, account, password, name);
                    sendDataToClient.Add(0.ToString());
                }
                SqlConnection.sqlClose(SqlConnection.mySqlConnection);
                sendDataToClient.Add(type.ToString());
                break;
            case 2:
                account = st[0];
                var user = Communication.connectedUsers.FirstOrDefault(u => u.Value.user == account).Value;
                int typeBuy = int.Parse(st[1]);
                switch (typeBuy)
                {
                    case 5:
                        if (user.amount >= 10)
                        {
                            user.amount -= 10;
                            Console.WriteLine(user.amount.ToString());
                            sendDataToClient.Add(1.ToString());
                            sendDataToClient.Add("10");

                        }
                        break;
                }
            
                sendDataToClient.Add(type.ToString());
                break;


        }
    }
    public static string sendBackToClient(List<string> sendData)
    {
        string responseStr = string.Empty;
        for (int i = 0; i < sendData.Count; i++)
        {
            responseStr += sendData[i] + (i == sendData.Count - 1 ? string.Empty : "|");
        }
        sendDataToClient.Clear();
        return responseStr;
    }

}