using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.Json
{
    class Day
    {
        public double maxtemp_c { get; set; }
        public double maxtemp_f { get; set; }
        public double mintemp_c { get; set; }
        public double mintemp_f { get; set; }
        public double avgtemp_c { get; set; }
        public double avgtemp_f { get; set; }
        public double maxwind_mph { get; set; }
        public double maxwind_kph { get; set; }
        public string totalprecip_mm { get; set; }
        public string totalprecip_in { get; set; }
        public double avgvis_km { get; set; }
        public string avgvis_miles { get; set; }
        public string avghumidity { get; set; }
        public Condition2 condition { get; set; }
        public double uv { get; set; }
    }
}
