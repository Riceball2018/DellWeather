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
            CacheTime = weatherModel.Dt;
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
        /// Get Weather information via web service
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<WeatherModel> GetWeather(int cityId)
        {
            if (!cityMap.ContainsKey(cityId)) return null;

            if (cityMap[cityId].CityWeather == null)
            {
                WeatherModel wm = await WeatherService.GetWeather(cityId);
                cityMap[cityId].CacheWeatherInfo(wm);
                Debug.WriteLine($"temp {wm.Main.Temp}, time request {wm.Dt}");
                return wm;
            }

            // TODO: Caching
            else
            {
                return cityMap[cityId].CityWeather;
            }
        }

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
