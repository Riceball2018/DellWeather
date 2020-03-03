using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DellWeatherApp.JsonParsing
{
    public class WeatherJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (null == reader) return null;
            if (JsonToken.Null == reader.TokenType) return null;

            JToken t = JToken.Load(reader);

            return new WeatherModel
            {
                  WeatherConditions = JsonConvert.DeserializeObject<List<Weather>>(t["weather"].ToString())
                , Main = t["main"].ToObject<Main>()
                , Id = t["id"].ToObject<int>()
                , Sys = t["sys"].ToObject<Sys>()
                , Name = t["name"].ToString()
                , Dt = UnixToDateTime(t["dt"].ToObject<int>())
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(WeatherModel) == objectType;
        }

        private DateTime UnixToDateTime(long time)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(time).ToLocalTime();
            return dt;
        }
    }
}
