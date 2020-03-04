using System;
using Newtonsoft.Json;

namespace DellWeatherApp.JsonParsing
{
    /// <summary>
    /// For sunrise
    /// </summary>
    public class Sys : BaseJson
    {
        /// <summary>
        /// Country code
        /// </summary>
        [JsonIgnore]
        private string country;
        [JsonProperty("country")]
        public string Country
        { 
            get
            {
                return country;
            }

            set
            {
                if (string.Compare(value, country, StringComparison.Ordinal) != 0)
                {
                    country = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private DateTime sunrise;
        [JsonIgnore]
        public DateTime Sunrise
        {
            get
            {
                return sunrise;
            }

            set
            {
                if (value != sunrise)
                {
                    sunrise = value;
                    NotifyPropertyChanged();
                }
            }
        }


        [JsonIgnore]
        private DateTime sunset;
        [JsonIgnore]
        public DateTime Sunset
        {
            get
            {
                return sunset;
            }

            set
            {
                if (value != sunset)
                {
                    sunset = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
