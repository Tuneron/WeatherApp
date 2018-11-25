using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WeatherApp.Models
{
    class JsonParserModel
    {
        private string baseUrl;

        private string jsonAnswer;
        public JsonParserModel ()
        {
            baseUrl = "http://api.apixu.com/v1/forecast.json?key=a224baa42924464ba6e101300182511&q=";
            jsonAnswer = "";
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
