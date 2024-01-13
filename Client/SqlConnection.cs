using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

    public class SqlConnection
    {

    public static string mysqlCon = "server=localhost; user = root; database = quannet4cham0; passsword =";
    public static MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
    public static void sqlConnect(MySqlConnection mySqlConnection)
        {
            
            try
            {
                mySqlConnection.Open();
                Console.WriteLine("Connected to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message, "Failed");
            }
        }
        public static void sqlClose(MySqlConnection mySqlConnection)
        {
            try
            {
                mySqlConnection.Close();
                Console.WriteLine("Disonnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, "Failed");
            }
        }
    }


