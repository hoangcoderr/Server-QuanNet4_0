using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    {
        object clientResuft;
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
        LoadDataUser(SqlConnection.mySqlConnection, username, id);
    }
    public static void LoadDataUser(MySqlConnection connection, string username, string id)
    {
        string query = "SELECT id, account,name,amount,client FROM userInformation WHERE account = @Username";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", username);
            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = Communication.connectedUsers.FirstOrDefault(u => u.Value.clientId == id).Value;
                        if (user != null)
                        {
                            user.id = reader.GetInt32(0);
                            user.user = reader.GetString(1);
                            user.name = reader.GetString(2);
                            user.amount = reader.GetInt32(3);
                            Progress.sendDataToClient.Add(user.id.ToString());
                            Progress.sendDataToClient.Add(user.user);
                            Progress.sendDataToClient.Add(user.name);
                            Progress.sendDataToClient.Add(user.amount.ToString());
                            Console.WriteLine("ID: " + user.id + "||Username: " + user.user + "||Name: " + user.name + "||Amount: " + user.amount + "||Client: " + user.clientId);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Cannot find the data with username: " + username);
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
    public static void loadItemFromServer(MySqlConnection connection, List<Item> items)
    {
        Console.WriteLine("Loading Item!!!");

        string query = "SELECT * FROM item";
        try
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item newItem = new Item();
                        newItem.Id = reader.GetInt32(0);
                        newItem.itemName = reader.GetString(1);
                        newItem.amount = reader.GetInt32(2);
                        newItem.description = reader.GetString(3);
                        Console.WriteLine("Item ID: " + newItem.Id + "||Item Name: " + newItem.itemName + "||Item Amount: " + newItem.amount.ToString() + "||Decription: " + newItem.description);
                        items.Add(newItem);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SqlConnection.sqlClose(connection);
            Console.WriteLine(ex.Message);
        }
    }
}
