﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DellWeatherApp.JsonParsing;
using Newtonsoft.Json;

namespace DellWeatherApp.Webservice
{
    public class WeatherService
    {
        private static readonly string apiCall = "https://api.openweathermap.org/data/2.5/weather";
        private static readonly string apiKey  = "&appid=4f094915199158e6e7d09c671410ba08";

        private static HttpClient client = new HttpClient();

        // TODO: Add cancellation token
        public static async Task<WeatherModel> GetWeather(int cityId)
        {
            try
            {
                string weatherRequest = apiCall + "?id=" + cityId + apiKey;
                Debug.WriteLine(weatherRequest);

                string response = await client.GetStringAsync(weatherRequest);

                WeatherModel weatherModel = JsonConvert.DeserializeObject<WeatherModel>(response, new JsonConverter[] { new WeatherJsonConverter() });

                return weatherModel;
            }

            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
