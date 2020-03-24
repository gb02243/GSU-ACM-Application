using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace GSUACM.ViewModels
{
    class DB
    {
        // the connection
        private MySqlConnection connection  = new MySqlConnection("server=acm-db.cjdtpyz15vp0.us-east-1.rds.amazonaws.com;port=3306;username=admin;password=dm001192;database=ACM_DB");


        // create a function to open the connection
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // create a function to close the connection
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // create a function to return the connection
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}

