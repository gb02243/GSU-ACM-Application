using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace GSUACM.ViewModels
{
    class DB
    {
        // the connection
        private MySqlConnection connection = new MySqlConnection("server=acmdbv2.cjdtpyz15vp0.us-east-1.rds.amazonaws.com;port=3306;username=admin;password=dm001192;database=acm_db");


        // create a function to open the connection
        public bool openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                if (IsServerConnected())
                {

                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
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
        public bool IsServerConnected()
        {


            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {

                return false;
            }

        }
    }
}

