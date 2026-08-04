using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace WinFormsApp1.DataAccess
{
    public class DBConnection
    {
        private static string connString =
            ConfigurationManager.ConnectionStrings["AnomalyVersusDB"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
    }
}
