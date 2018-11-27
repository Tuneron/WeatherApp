﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;
using System.Data.SqlClient;

namespace WeatherApp.Models
{
    class DatabaseModel
    {
        private SqlConnection conncetion;
        private SqlCommand command;
        private SqlDataReader reader;
        private JsonParserController json;

        public DatabaseModel(string connectionString)
        {
            conncetion = new SqlConnection();
            conncetion.ConnectionString = connectionString;
            command = new SqlCommand();
            command.Connection = conncetion;
        }

        public bool CheckConnection()
        {
            if (conncetion != null)
                return true;
            else
                return false;
        }

        public void StartConnection()
        {
            conncetion.Open();
        }

        public void CloseConnection()
        {
            conncetion.Close();
        }

        public void Auntefication(string login, string password)
        {

        }

        public void AddUser(string login, string password, string defaultCity)
        {

        }

    }
}