﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlProgession
{
    public static bool IsUserAvaiable(MySqlConnection connection, string username, string password)
    {
        string query = "SELECT COUNT(*) FROM userAccount WHERE account = @Username AND password = @Password";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            try
            {
                long count = Convert.ToInt64(command.ExecuteScalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return false;

    }
    public static bool IsUserRegistered(MySqlConnection connection, string username)
    {
        string query = "SELECT COUNT(*) FROM userAccount WHERE account = @Username";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            try
            {
                command.Parameters.AddWithValue("@Username", username);
                long count = Convert.ToInt64(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return false;
    }
    public static void addAccountToServer(MySqlConnection connection, string username, string password)
    {
        string query = "INSERT INTO userAccount (account, password)\r\nVALUES\r\n  (@username, @password);";
        try
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
