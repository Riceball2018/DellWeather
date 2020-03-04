using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using DellWeatherApp.Extensions;

namespace DellWeatherApp.JsonParsing
{
    public class WeatherJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (null == reader) return null;
            if (JsonToken.Null == reader.TokenType) return null;

            JToken t = JToken.Load(reader);

            int dt = t["dt"].ToObject<int>();
            DateTime dateTime = ((long)dt).UnixToDateTime();
            string sysStr = t["sys"].ToString();
            Sys sys = JsonConvert.DeserializeObject<Sys>(sysStr, new JsonConverter[] { new SysJsonConverter() });
            //Sys sys = JsonConvert.DeserializeObject<Sys>(sysStr);

            return new WeatherModel
            {
                WeatherConditions = JsonConvert.DeserializeObject<List<Weather>>(t["weather"].ToString())
                , Main = t["main"].ToObject<Main>()
                , Id = t["id"].ToObject<int>()
                , Sys = sys
                , Name = t["name"].ToString()
                , Dt = dateTime
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
    }
}
