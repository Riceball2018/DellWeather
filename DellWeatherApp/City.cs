using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellWeatherApp
{
    public class City
    {
        public Weather CityWeather { get; set; }         // Weather information for the city
        public string Name { get; private set; }         // Name of the city 
        public DateTime CacheTime { get; private set; }  // Time weather information was retrieved

        private List<Weather> weatherCache;

        public City(string cityName)
        {
            Name = cityName;

            CityWeather = new Weather();
        }
    }

    public class CityViewModel
    {
        private ObservableCollection<City> cities = new ObservableCollection<City>();
        public ObservableCollection<City> Cities { get { return cities; } }

        public CityViewModel()
        {
            cities.Add(new City("Singapore"));
            cities.Add(new City("Taipei"));
            cities.Add(new City("Austin"));
            cities.Add(new City("San Francisco"));
        }
    }
}
