//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//            Nam mô, chỉ có phật với mới tôi mới chạy được đống rác này
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using WebSocketSharp;
using System.Threading;

public class Progress
{
    public static string YES = "0";
    public static string NO = "1";

    public static string SERVER_NOT_REQUIRED = "2";
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

                if (SqlProgession.IsUserAvaiable(SqlConnection.mySqlConnection, account, password, id))
                {
                    sendDataToClient.Add(YES);

                    clientUsing = SqlProgession.getClientUsing(SqlConnection.mySqlConnection, account);
                    if (clientUsing != string.Empty)
                    {
                        Console.WriteLine("Kicked " + clientUsing);
                        Server.DisconnectClient(clientUsing);
                    }

                    SqlProgession.setClientForUser(SqlConnection.mySqlConnection, account, id);
                }
                else
                    sendDataToClient.Add(NO);
                sendDataToClient.Add(type.ToString());
                break;
            case 1:
                account = st[0];
                password = st[1];
                name = st[2];
                sendDataToClient = new List<string>();
                if (SqlProgession.IsUserRegistered(SqlConnection.mySqlConnection, account))
                {
                    sendDataToClient.Add(NO);
                }
                else
                {
                    SqlProgession.addAccountToServer(SqlConnection.mySqlConnection, account, password, name);
                    sendDataToClient.Add(YES);
                }
                sendDataToClient.Add(type.ToString());
                break;
            case 2:
                account = st[0];
                var user = Communication.connectedUsers.FirstOrDefault(u => u.Value.user == account).Value;
                int idBuy = int.Parse(st[1]) - 1;
                if (Item.items[idBuy].quantity > 0)
                {
                    if (user.amount >= Item.items[idBuy].amount)
                    {
                        user.amount -= Item.items[idBuy].amount;
                        Item.items[idBuy].quantity -= 1;
                        ThreadStart threadStart = new ThreadStart(() =>
                         {
                             SqlProgession.updateAmountToDatabase(SqlConnection.mySqlConnection, account, user.amount);
                             SqlProgession.updateItemQuantityToDatabase(SqlConnection.mySqlConnection, idBuy + 1);
                         }); 
                        Thread thread = new Thread(threadStart);
                        thread.Start();
                        Console.WriteLine(user.amount.ToString());
                        sendDataToClient.Add(YES);
                        sendDataToClient.Add(Item.items[idBuy].amount.ToString());
                    }
                    else
                    {
                        sendDataToClient.Add(NO);
                    }
                }
                else
                {
                    sendDataToClient.Add(SERVER_NOT_REQUIRED);
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