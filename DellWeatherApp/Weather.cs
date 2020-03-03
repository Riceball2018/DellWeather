using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellWeatherApp
{
    public class Weather
    {
        public float LowTemp { get; set; }
        public float HighTemp { get; set; }
        public float CurrTemp { get; set; }
        public float Humidity { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public Weather(float lowTemp, float highTemp, float currTemp, float humidity, DateTime sunrise, DateTime sunset)
        {
            LowTemp  = lowTemp;
            HighTemp = highTemp;
            CurrTemp = currTemp;
            Humidity = humidity;
            Sunrise  = sunrise;
            Sunset   = sunset;
        }

        public Weather() : this(0, 0, 0, 0, DateTime.MinValue, DateTime.MinValue) { }
    }
}
