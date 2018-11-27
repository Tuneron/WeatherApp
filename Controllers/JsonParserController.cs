using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Json;

namespace WeatherApp.Controllers
{
    class JsonParserController
    {
        JsonParserModel model;

        public JsonParserController()
        {
            model = new JsonParserModel();
        }

        public RootObject GetForecast(string city)
        {
            return model.GetForecast(city);
        }
    }
}
