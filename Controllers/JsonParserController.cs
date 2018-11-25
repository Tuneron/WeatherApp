using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    class JsonParserController
    {
        JsonParserModel model;

        public JsonParserController()
        {
            model = new JsonParserModel();
        }

        public string CheckForInternetConnection()
        {
            if (model.CheckForInternetConnection())
                return "Connectig succes !";
            else
                return "No internet connection";
        }
    }
}
