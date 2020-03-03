using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DellWeatherApp.JsonParsing;

namespace DellWeatherApp.Webservice
{
    public class WeatherService
    {
        private readonly string apiCall = "api.openweathermap.org/data/2.5/weather";

        public static async WeatherModel GetWeather(int cityId)
        {
            HttpClient
        }
    }
}
