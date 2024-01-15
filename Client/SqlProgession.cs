using MySql.Data.MySqlClient;
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
    public static void LoadDataUser(MySqlConnection connection, string username){
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
