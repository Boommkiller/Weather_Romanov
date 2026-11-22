using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class Forecast
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("hourly")]
        public HourlyData Hourly { get; set; }
    }

    public class HourlyData
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }
        [JsonProperty("temperature_2m")]
        public List<double?> Temperature { get; set; }
        [JsonProperty("apparent_temperature")]
        public List<double?> ApparentTemperature { get; set; }
        [JsonProperty("relative_humidity_2m")]
        public List<int?> Humidity { get; set; }
        [JsonProperty("surface_pressure")]
        public List<double?> Pressure { get; set; }
        [JsonProperty("wind_speed_10m")]
        public List<double?> WindSpeed { get; set; }
        [JsonProperty("weather_code")]
        public List<int?> WeatherCode { get; set; }
    }
}
