﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.Json
{
    class Forecast
    {
        public List<Forecastday> forecastday { get; set; }
    }
}
