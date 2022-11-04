using System;
using System.Globalization;

namespace lkcode.hetznercloudapi.Helper
{
    public static class DateTimeHelper
    {
        private const string format_iso8601 = "yyyy-MM-dd'T'HH:mm:ssZ";  //2017-01-01T00:00:00Z

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetAsIso8601String(DateTime dateTime)
        {
            string dateTimeIso8601 = dateTime.ToString(format_iso8601, CultureInfo.InvariantCulture);

            return dateTimeIso8601;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime GetFromUnixTimestamp(long timestamp)
        {
            DateTime localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime.ToLocalTime();

            return localDateTimeOffset;
        }
    }
}
