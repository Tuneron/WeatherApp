using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Json;

namespace WeatherApp.Controllers
{
    class DatabaseController
    {
        private DatabaseModel databaseModel;

        public DatabaseController(string connect)
        {
            databaseModel = new DatabaseModel(connect);
        }

        public void StartConnection()
        {
            databaseModel.StartConnection();
        }

        public void CloseConnection()
        {
            databaseModel.CloseConnection();
        }

        public bool CheckDatabaseConnect()
        {
            return databaseModel != null ? true : false;
        }

        public Current GetCurrentWeather(string city)
        {
            return databaseModel.GetCurrentsWeather(city);
        }

        public List<Forecastday> GetWeekForecast(string city)
        {
            return databaseModel.GetWeekWeather(city).forecastday;
        }
    }
}
