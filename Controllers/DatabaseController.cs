using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    class DatabaseController
    {
        private DatabaseModel database;

        public DatabaseController(string connect)
        {
            database = new DatabaseModel(connect);
        }

        public void StartConnection()
        {
            database.StartConnection();
        }

        public void CloseConnection()
        {
            database.CloseConnection();
        }

        public bool CheckDatabaseConnect()
        {
            return database != null ? true : false;
        }
    }
}
