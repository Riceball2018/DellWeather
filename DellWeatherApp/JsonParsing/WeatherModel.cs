using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DellWeatherApp.JsonParsing
{
    public class WeatherModel : BaseJson
    {
        // Not implemented
        //public Coord coord { get; set; }
        [JsonIgnore]
        private List<Weather> weatherConditions;

        [JsonProperty("weather")]
        public List<Weather> WeatherConditions
        {
            get
            {
                return weatherConditions;
            }

            set
            {
                if (value != weatherConditions)
                {
                    weatherConditions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Not implemented
        //public string Base { get; set; }

        [JsonIgnore]
        private Main main;

        [JsonProperty("main")]
        public Main Main
        {
            get
            {
                return main;
            }

            set
            {
                if (value != main)
                {
                    main = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Not implemented
        //public int visibility { get; set; }
        // Not implemented
        //public Wind wind { get; set; }
        // Not implemented
        //public Clouds clouds { get; set; }

        // Time of data calculation
        [JsonIgnore]
        private DateTime dt;
        [JsonIgnore]
        public DateTime Dt
        {
            get
            {
                return dt;
            }

            set
            {
                if (value != dt)
                {
                    dt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // For sunrise
        [JsonIgnore]
        private Sys sys;
        //[JsonProperty("sys")]
        [JsonIgnore]
        public Sys Sys
        {
            get
            {
                return sys;
            }

            set
            {
                if (value != sys)
                {
                    sys = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // City id
        [JsonIgnore]
        private int id;
        [JsonProperty("id")]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // City name
        [JsonIgnore]
        private string name;
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (string.Compare(value, name, StringComparison.Ordinal) != 0)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Not implemented
        //public int cod { get; set; }
    }
}
