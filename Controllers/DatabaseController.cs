﻿using System;
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

        public bool CheckDatabaseConnect()
        {
            return databaseModel != null ? true : false;
        }

        public Current GetCurrentWeather(string city)
        {
            databaseModel.StartConnection();
            try
            {
                return databaseModel.GetCurrentsWeather(city);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public List<Forecastday> GetWeekForecast(string city)
        {
            databaseModel.StartConnection();
            try
            {
                return databaseModel.GetWeekWeather(city).forecastday;
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public Location GetCurrentLocation(string city)
        {
            databaseModel.StartConnection();
            try
            {
                return databaseModel.GetCurrentLocation(city);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public void SetDefaultLocation(string city, string region, string country)
        {
            databaseModel.StartConnection();
            try
            {
                databaseModel.SetDefaultLocation(city, region, country);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public string[] GetDefaultLocation()
        {
            databaseModel.StartConnection();
            try
            {
                return databaseModel.GetDefaultLocation();
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public void SaveUsersCatalogs(string[] cities, string[] regions, string[] countries)
        {
            databaseModel.StartConnection();
            try
            {
                databaseModel.SaveUserCatalogs(cities, regions, countries);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public List<string> LoadUserCatalog(string catalogName)
        {
            databaseModel.StartConnection();
            try
            {
                return databaseModel.LoadUserCatalog(catalogName);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }

        public void SaveForecast(string city)
        {
            databaseModel.StartConnection();
            try
            {
                databaseModel.SaveForecastCopy(city);
            }
            finally
            {
                databaseModel.CloseConnection();
            }
        }
    }
}
