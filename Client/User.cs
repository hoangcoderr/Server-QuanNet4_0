using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

public class User
{
    public string user = string.Empty;
    
    public string name = string.Empty;
    public int id = 0;
    public int amount = 0;
    public string clientId = string.Empty;
    public User()
    {
    }
}