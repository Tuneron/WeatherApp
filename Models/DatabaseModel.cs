using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;
using WeatherApp.Models.Json;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WeatherApp.Models
{
    class DatabaseModel
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        private JsonParserController json;

        public DatabaseModel(string connectionString)
        {
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            command = new SqlCommand();
            command.Connection = connection;
            json = new JsonParserController();
        }

        private void ResetCommand()
        {
            command.Dispose();
            command = new SqlCommand();
            command.Connection = connection;
        }

        public bool CheckConnection()
        {
            if (connection != null)
                return true;
            else
                return false;
        }

        public void StartConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
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
            ResetCommand();

            command.CommandText = "UPDATE DefaultLocation SET City = @city, Region = @region, Country = @country WHERE DefaultLocationId = 1;";
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@region", region);
            command.Parameters.AddWithValue("@country", country);
            command.ExecuteNonQuery();
        }

        public string[] GetDefaultLocation()
        {
            string[] result = new string[3];

            ResetCommand();

            command.CommandText = "SELECT * FROM DefaultLocation";
            reader = command.ExecuteReader();
            reader.Read();

            result[0] = reader["City"].ToString();
            result[1] = reader["Region"].ToString();
            result[2] = reader["Country"].ToString();

            reader.Close();

            return result;
        }

        public List<string> LoadUserCatalog(string catalogName)
        {
            List<string> result = new List<string>();

            ResetCommand();

            command.CommandText = string.Format("SELECT * FROM {0}", catalogName);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["Name"].ToString());
            }

            reader.Close();

            return result;
        }

        private string ValidateValue(string value)
        {
            while (value.IndexOf("'") != -1)
            {
                value = value.Insert(value.IndexOf("'"), " ");
                value = value.Remove(value.IndexOf("'"), 1);
            }

            return value;
        }

        public void SaveUserCatalogs(string[] cities, string[] regions, string[] countries)
        {
            SaveCatalog("City", cities);
            SaveCatalog("Region", regions);
            SaveCatalog("Country", countries);
        }

        private void SaveCatalog(string tableName, string[] catalog)
        {
            ResetCommand();

            command.CommandText = string.Format("TRUNCATE TABLE {0};", tableName);
            command.ExecuteNonQuery();

            for(int i = 0; i < catalog.Length; i++)
            {
                ResetCommand();
                command.CommandText = string.Format("INSERT INTO {0} (Name) VALUES ('{1}');", tableName, ValidateValue(catalog[i]));
                command.ExecuteNonQuery();
            }
        }

        public void SaveWeekForecast(string city)
        {
            command.CommandText = string.Format("INSERT INTO ForecastWeek (City, Region, Country) " +
            "VALUES (@city, @region, @country);");

            command.Parameters.AddWithValue("@city", json.GetForecast(city).location.name);
            command.Parameters.AddWithValue("@region", json.GetForecast(city).location.region);
            command.Parameters.AddWithValue("@country", json.GetForecast(city).location.country);

            command.ExecuteNonQuery();

            command.CommandText = string.Format("SELECT @@IDENTITY AS [LastId] FROM ForecastWeek");
            reader = command.ExecuteReader();
            reader.Read();

            int ForecastWeekId = int.Parse(reader["LastId"].ToString());

            reader.Close();

            for (int i = 0; i < (int)MainForm.Days.week; i++)
            {
                SaveDayForecast(city, i, ForecastWeekId);
                ResetCommand();
            }
        }

        private void SaveDayForecast(string city, int day, int ForecastWeekId)
        {
            command.CommandText = string.Format("INSERT INTO Forecast (Date, LastUpdate, Icon, MinTemp, MaxTemp, Wind, Humidity, Pressure, ForecastWeek)" +
                "VALUES (@date, @lastUpdate, @icon, @minTemp, @maxTemp, @wind, @hymidity, @pressure, @forecastWeek);");

            command.Parameters.AddWithValue("@date", json.GetForecast(city).forecast.forecastday[day].date);
            command.Parameters.AddWithValue("@lastUpdate", json.GetForecast(city).current.last_updated);
            command.Parameters.AddWithValue("@icon", "http:" + json.GetForecast(city).forecast.forecastday[day].day.condition.icon);
            command.Parameters.AddWithValue("@minTemp", json.GetForecast(city).forecast.forecastday[day].day.mintemp_c);
            command.Parameters.AddWithValue("@maxTemp", json.GetForecast(city).forecast.forecastday[day].day.maxtemp_c);
            command.Parameters.AddWithValue("@wind", json.GetForecast(city).forecast.forecastday[day].day.avgvis_km);
            command.Parameters.AddWithValue("@hymidity", json.GetForecast(city).forecast.forecastday[day].day.avghumidity);
            command.Parameters.AddWithValue("@pressure", json.GetForecast(city).forecast.forecastday[day].day.totalprecip_in);
            command.Parameters.AddWithValue("@forecastWeek", ForecastWeekId);

            command.ExecuteNonQuery();
        }

        public string[] GetSavedLocations()
        {
            ResetCommand();

            command.CommandText = string.Format("SELECT COUNT(ForecastWeekId) AS CountForecast FROM ForecastWeek");
            reader = command.ExecuteReader();
            reader.Read();

            int count = int.Parse(reader["CountForecast"].ToString());

            reader.Close();

            string[] collection = new string[count];

            for (int i = 1; i < count + 1; i++)
            {
                collection[i-1] =
                    LoadForecastLocation(i)[0] + " " +
                    LoadForecastLocation(i)[1] + " " +
                    LoadForecastLocation(i)[2];
            }

            return collection;
        }

        private string[] LoadForecastLocation(int Id)
        {
            string[] location = new string[3];

            ResetCommand();

            command.CommandText = "SELECT City, Region, Country FROM ForecastWeek WHERE ForecastWeekId = @id";
            command.Parameters.AddWithValue("@id", Id);
            reader = command.ExecuteReader();
            reader.Read();

            location[0] = reader["City"].ToString();
            location[1] = reader["Region"].ToString();
            location[2] = reader["Country"].ToString();

            reader.Close();

            return location;
        }

        public string GetTimeInterval(int id)
        {
            int endId = id * 7;
            int startId = endId - 6;

            string interval = "";

            ResetCommand();

            command.CommandText = "SELECT Date FROM Forecast WHERE ForecastId = @id";
            command.Parameters.AddWithValue("@id", startId);
            reader = command.ExecuteReader();
            reader.Read();

            interval= reader["Date"].ToString() + " - ";

            reader.Close();

            ResetCommand();

            command.CommandText = "SELECT Date FROM Forecast WHERE ForecastId = @id";
            command.Parameters.AddWithValue("@id", endId);
            reader = command.ExecuteReader();
            reader.Read();

            interval += reader["Date"].ToString();

            return interval;
        }

        public string GetSavedValue(string value, int id)
        {
            command.CommandText = string.Format("SELECT {0} FROM Forecast WHERE ForecastId = {1};", value, id);
            reader = command.ExecuteReader();
            reader.Read();

            try
            {
                return reader[value].ToString();
            }
            finally
            {
                reader.Close();
                ResetCommand();
            }
        }

        public string GetOfflineLocation(string value, int id)
        {
            command.CommandText = string.Format("SELECT {0} FROM ForecastWeek WHERE ForecastWeekId = {1};", value, id);
            reader = command.ExecuteReader();
            reader.Read();

            try
            {
                return reader[value].ToString();
            }
            finally
            {
                reader.Close();
                ResetCommand();
            }
        }
    }
}
