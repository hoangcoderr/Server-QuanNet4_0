using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

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
    public static string dataSend = string.Empty;
    public static void processData(string[] st, string id)
    {
        int type = int.Parse(st[st.Length - 1]);
        switch (type)
        {
            case 0:
                account = st[0];
                password = st[1];
                sendDataToClient = new List<string>();
                SqlConnection.sqlConnect(SqlConnection.mySqlConnection);
                if (SqlProgession.IsUserAvaiable(SqlConnection.mySqlConnection, account, password, id))
                {
                    sendDataToClient.Add(0.ToString());
                    SqlProgession.LoadDataUser(SqlConnection.mySqlConnection, account);
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
        }
    }
    public static void SendData(TcpClient client, List<string> sendData)
    {
        NetworkStream stream = client.GetStream();
        string responseStr = string.Empty;
        for (int i = 0; i < sendData.Count; i++)
        {
            responseStr += sendData[i] + (i == sendData.Count - 1 ? string.Empty : "|");
        }
        byte[] responseData = Encoding.UTF8.GetBytes(responseStr);
        stream.Write(responseData, 0, responseData.Length);
    }
    public static string sendBackToClient(List<string> sendData)
    {
        string responseStr = string.Empty;
        for (int i = 0; i < sendData.Count; i++)
        {
            responseStr += sendData[i] + (i == sendData.Count - 1 ? string.Empty : "|");
        }
        return responseStr;
    }
}