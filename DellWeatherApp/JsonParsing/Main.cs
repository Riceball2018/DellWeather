using System;
using Newtonsoft.Json;


namespace DellWeatherApp.JsonParsing
{
    /// <summary>
    /// Temperature and pressure information
    /// </summary>
    public class Main : BaseJson
    {
        [JsonIgnore]
        private float temp;
        [JsonProperty("temp")]
        public float Temp
        {
            get
            {
                return temp;
            }

            set
            {
                if (value != temp)
                {
                    temp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private int pressure;
        [JsonProperty("pressure")]
        public int Pressure
        { 
            get
            {
                return pressure;
            }

            set
            {
                if (value != pressure)
                {
                    pressure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Humidity in %
        /// </summary>
        [JsonIgnore]
        private int humidity;
        [JsonProperty("humidity")]
        public int Humidity
        {
            get
            {
                return humidity;
            }

            set
            {
                if (value != humidity)
                {
                    humidity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private float tempMin;
        [JsonProperty("temp_min")]
        public float TempMin
        {
            get
            {
                return tempMin;
            }

            set
            {
                if (value != tempMin)
                {
                    tempMin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        private float tempMax;
        [JsonProperty("temp_max")]
        public float TempMax
        {
            get
            {
                return tempMax;
            }

            set
            {
                if (value != tempMax)
                {
                    tempMax = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
