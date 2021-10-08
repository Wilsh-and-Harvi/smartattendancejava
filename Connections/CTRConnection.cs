using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAttendance.Connections
{
    class CTRConnection
    {

        //Shanmukhananda
        //static string connectionString = "datasource=pulse-hmsawsdb.cxvfz81wqwim.ap-south-1.rds.amazonaws.com;port=3306;username=admin;password=Pulseadmin2021;";
        //Wilsh and Harvi
        static string connectionString = "datasource=kmanager.chzykisgk4xm.us-east-2.rds.amazonaws.com;port=3306;username=admin;password=Admin2021;";

        public static MySqlConnection con = new MySqlConnection(connectionString);


        public MySqlConnection openConnection()
        {

            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
                return con;
            }
            else
            {
                return con;
            }


        }
    }
}
