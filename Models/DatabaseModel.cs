using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;
using WeatherApp.Models.Json;
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
            json = new JsonParserController();
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

        public Current GetCurrentsWeather(string city)
        {
            return json.GetForecast(city).current;
        }

        public Forecast GetWeekWeather(string city)
        {
            return json.GetForecast(city).forecast;
        }

        public Location GetCurrentLocation(string city)
        {
            return json.GetForecast(city).location;
        }

        public void SetDefaultLocation(string city, string region, string country)
        {
            command.CommandText = "UPDATE DefaultLocation SET City = @city, Region = @region, Country = @country WHERE DefaultLocationId = 1;";
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@region", region);
            command.Parameters.AddWithValue("@country", country);
            command.ExecuteNonQuery();
        }

        public string[] GetDefaultLocation()
        {
            string[] result = new string[3];

            command.CommandText = "SELECT * FROM DefaultLocation";
            reader = command.ExecuteReader();
            reader.Read();

            result[0] = reader["City"].ToString();
            result[1] = reader["Region"].ToString();
            result[2] = reader["Country"].ToString();

            reader.Close();

            return result;
        }

        public void SaveUserCatalog(string[] cities, string[] regions, string[] countries)
        {
            SaveCatalog("City", cities);
            SaveCatalog("Region", regions);
            SaveCatalog("Country", countries);
        }

        private void SaveCatalog(string tableName, string[] catalog)
        {
            command.CommandText = string.Format("TRUNCATE TABLE {0};", tableName);
            command.ExecuteNonQuery();
            for(int i = 0; i < catalog.Length; i++)
            {
                while (catalog[i].IndexOf("'") != -1)
                {
                    catalog[i] = catalog[i].Insert(catalog[i].IndexOf("'"), " ");
                    catalog[i] = catalog[i].Remove(catalog[i].IndexOf("'"), 1);
                }
                command.CommandText = string.Format("INSERT INTO {0} (Name) VALUES ('{1}');", tableName, catalog[i]);
                command.ExecuteNonQuery();
            }
        }

        public void SaveForecastCopy()
        {

        }
    }
}
