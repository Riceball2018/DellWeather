using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DellWeatherApp.JsonParsing;
using DellWeatherApp.Webservice;

namespace DellWeatherApp
{
    public class City
    {
        public WeatherModel CityWeather { get; set; }    // Weather information for the city
        public string Name { get; private set; }         // Name of the city 
        public int CityId { get; private set; }          // City id for weather request
        public DateTime CacheTime { get; private set; }  // Time weather information was retrieved

        // TODO: Auto update even when refresh not clicked
        private WeatherModel weatherCache;

        public City(string cityName)
        {
            Name = cityName;

            if (Name.Equals("Singapore"))
            {
                CityId = 1880251;
            }

            else if (Name.Equals("Taipei"))
            {
                CityId = 1668341;
            }

            else if (Name.Equals("Austin"))
            {
                CityId = 4254010;
            }

            // There are a lot of san franciscos..
            else if (Name.Equals("San Francisco"))
            {
                CityId = 5391959;
            }
        }

        public void CacheWeatherInfo(WeatherModel weatherModel)
        {
            CityWeather = weatherModel;
            weatherCache = weatherModel;
            
            // Request time is not reliable to check cache status
            // Using current time instead
            //CacheTime = weatherModel.Dt;
            CacheTime = DateTime.Now;
            Debug.WriteLine($"caching weather {CacheTime}");
        }
    }

    public class CityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<City> cities = new ObservableCollection<City>();
        public ObservableCollection<City> Cities { get { return cities; } }

        public WeatherModel ActiveWeather
        {
            get
            {
                if (cityMap.ContainsKey(activeCityId))
                {
                    return cityMap[activeCityId].CityWeather;
                }

                else
                {
                    return null;
                }
            }
        }

        public City ActiveCity
        {
            get
            {
                if (cityMap.ContainsKey(activeCityId))
                {
                    return cityMap[activeCityId];
                }

                else
                {
                    return null;
                }
            }
        }
        
        private Dictionary<int, City> cityMap = new Dictionary<int, City>();
        private int activeCityId;

        /// <summary>
        /// Cache lifetime before cache is dirty, 10 minutes because openweatherapi is accurate at 10 min intervals
        /// </summary>
        private readonly double cacheLife = 600;  // 10 mins

        public CityViewModel()
        {
            City singaporeCity = new City("Singapore");
            City taipeiCity = new City("Taipei");
            City austinCity = new City("Austin");
            City sfCity = new City("San Francisco");

            cities.Add(singaporeCity);
            cities.Add(taipeiCity);
            cities.Add(austinCity);
            cities.Add(sfCity);

            cityMap.Add(singaporeCity.CityId, singaporeCity);
            cityMap.Add(taipeiCity.CityId, taipeiCity);
            cityMap.Add(austinCity.CityId, austinCity);
            cityMap.Add(sfCity.CityId, sfCity);
        }

        public void ShowWeatherInfo(int cityId)
        {
            activeCityId = cityId;
            //NotifyPropertyChanged("ActiveCity");
            //NotifyPropertyChanged("ActiveWeather");
        }

        /// <summary>
        /// Get weather information for active city
        /// </summary>
        /// <returns></returns>
        public async Task<WeatherModel> GetWeather()
        {
            return await GetWeather(activeCityId);
        }

        /// <summary>
        /// Get Weather information via web service
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<WeatherModel> GetWeather(int cityId)
        {
            if (!cityMap.ContainsKey(cityId)) return null;

            if (cityMap[cityId].CityWeather == null)
            {
                Debug.WriteLine("From service");

                WeatherModel wm = await WeatherService.GetWeather(cityId);
                cityMap[cityId].CacheWeatherInfo(wm);

                Debug.WriteLine($"temp {wm.Main.Temp}, time request {wm.Dt}");
                return wm;
            }

            else
            {
                if (IsWeatherClean(cityMap[cityId]))
                {
                    Debug.WriteLine("From cache");
                    return cityMap[cityId].CityWeather;
                }

                else
                {
                    Debug.WriteLine("From service");

                    WeatherModel wm = await WeatherService.GetWeather(cityId);
                    cityMap[cityId].CacheWeatherInfo(wm);
                    Debug.WriteLine($"temp {wm.Main.Temp}, time request {wm.Dt}");
                    return wm;
                }
            }
        }

        /// <summary>
        /// Converts Kelvin temperature to Celsius
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public float KelvinToC(float temp)
        {
            return temp - 273.15f;
        }
        
        /// <summary>
        /// Convert Kelvin temperature to Fahrenheit
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public float KelvinToF(float temp)
        {
            return (temp - 273.15f) * (9.0f / 5.0f) + 32;
        }

        private bool IsWeatherClean(City city)
        {
            DateTime dt = city.CacheTime.AddSeconds(cacheLife);
            double seconds = DateTime.Now.Subtract(dt).TotalSeconds;
            Debug.WriteLine($"now {DateTime.Now} requestTime {city.CityWeather.Dt} cache {dt} seconds {seconds}");
            return seconds < 0;
        }

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
