using System;

namespace DellWeatherApp.Extensions
{
    public static class UtilityExtensions
    {
        public static DateTime UnixToDateTime(this long time)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(time).ToLocalTime();
            return dt;
        }
    }
}
