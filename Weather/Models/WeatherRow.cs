using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class WeatherRow
    {
        public DateTime DateTime { get; set; }
        public string Temperature { get; set; }
        public string FeelsLike { get; set; }
        public int Pressure { get; set; }
        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Description { get; set; }
    }
}
