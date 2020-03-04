using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using DellWeatherApp.Extensions;

namespace DellWeatherApp.JsonParsing
{
    public class SysJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (null == reader) return null;
            if (JsonToken.Null == reader.TokenType) return null;

            JToken t = JToken.Load(reader);

            // Convert sunrise to date time
            int sunrise = t["sunrise"].ToObject<int>();
            DateTime sunriseDt = ((long)sunrise).UnixToDateTime();

            // Convert sunset to date time
            int sunset = t["sunset"].ToObject<int>();
            DateTime sunsetDt = ((long)sunset).UnixToDateTime();

            return new Sys
            {
                  Sunset = sunsetDt
                , Sunrise = sunriseDt
                , Country = t["country"].ToString()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Sys) == objectType;
        }
    }
}
