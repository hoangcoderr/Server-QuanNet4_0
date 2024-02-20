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
                Console.WriteLine("Disonnected to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, "Failed");
            }
        }
    }


