using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WeatherApp.Models.Json;
using Newtonsoft.Json;

namespace WeatherApp.Models
{
    class JsonParserModel
    {
        private const string BASE_URL = "http://api.apixu.com/v1/forecast.json?key=a224baa42924464ba6e101300182511&q=";
        private const string BASE_FORECAST_DAYS = "&days=7";

        private RootObject rootObject;

        private bool JsonRequest(string City)
        {
            WebClient wc = new WebClient();
            string json = wc.DownloadString(BASE_URL + City + BASE_FORECAST_DAYS);
            wc.Dispose();

            if (json != "")
            {
                rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                return true;
            }

            return false;
        }

        public RootObject GetForecast(string city)
        {
            if (Program.CheckForInternetConnection())
            {
                if (JsonRequest(city))
                    return rootObject;
            }

            return null;
        }
    }
}
