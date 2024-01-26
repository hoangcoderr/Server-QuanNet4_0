﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlProgession
{
    public static bool IsUserAvaiable(MySqlConnection connection, string username, string password, string id)
    {
        string query = "SELECT COUNT(*) FROM userAccount WHERE account = @Username AND password = @Password";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            try
            {
                long count = Convert.ToInt64(command.ExecuteScalar());

                if (count > 0)
                {
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return false;
    }
    public static string getClientUsing(MySqlConnection connection, string username)
    {   object clientResuft;
        string query = "SELECT client FROM userInformation WHERE account = @Username";
        using (MySqlCommand commands = new MySqlCommand(query, connection))
        {
            commands.Parameters.AddWithValue("@Username", username);
            clientResuft = commands.ExecuteScalar();
        }
        if (clientResuft != DBNull.Value)
        {
            return clientResuft.ToString();
        }
        return string.Empty;
    }
    public static void setClientForUser(MySqlConnection connection, string username, string id)
    {
        string query = "UPDATE userInformation SET client = @id WHERE account = @username;";
        using (MySqlCommand commands = new MySqlCommand(query, connection))
        {
            commands.Parameters.AddWithValue("@id", id);
            commands.Parameters.AddWithValue("@username", username);
            commands.ExecuteNonQuery();
        }
    }
    public static void LoadDataUser(MySqlConnection connection, string username)
    {
        string query = "SELECT id, account,name,amount FROM userInformation WHERE account = @Username";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", username);
            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Kiểm tra xem có dữ liệu không
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        Console.WriteLine("ID: " + id + "||Name: " + name);
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy dữ liệu với điều kiện cho trước.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
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
    public static void addAccountToServer(MySqlConnection connection, string username, string password, string name)
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
        query = "INSERT INTO userInformation (account, name, amount) VALUES (@username, @name, 0)";
        try
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
