using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.Json
{
    class Current
    {
        public int last_updated_epoch { get; set; }
        public string last_updated { get; set; }
        public string temp_c { get; set; }
        public double temp_f { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public string pressure_mb { get; set; }
        public double pressure_in { get; set; }
        public string precip_mm { get; set; }
        public string precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public double feelslike_c { get; set; }
        public string feelslike_f { get; set; }
        public string vis_km { get; set; }
        public string vis_miles { get; set; }
        public string uv { get; set; }
    }
}
