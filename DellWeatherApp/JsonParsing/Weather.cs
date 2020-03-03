using System;
using Newtonsoft.Json;

namespace DellWeatherApp.JsonParsing
{
    public class Weather : BaseJson
    {
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

        // Group of weather parameters
        [JsonIgnore]
        private string main;
        [JsonProperty("main")]
        public string Main
        {
            get
            {
                return main;
            }

            set
            {
                if (string.Compare(value, main, StringComparison.Ordinal) != 0)
                {
                    main = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private string description;
        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (string.Compare(value, description, StringComparison.Ordinal) != 0)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private string icon;
        [JsonProperty("icon")]
        public string Icon
        { 
            get
            {
                return icon;
            }

            set
            {
                if (string.Compare(value, icon, StringComparison.Ordinal) != 0)
                {
                    icon = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
