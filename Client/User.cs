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
    public int account = 0;
    
    public string name = string.Empty;
    public int id = 0;

    public User()
    {
    }
}