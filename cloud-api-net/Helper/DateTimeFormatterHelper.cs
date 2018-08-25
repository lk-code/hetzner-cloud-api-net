using System;
using System.Globalization;

namespace lkcode.hetznercloudapi.Helper
{
    public static class DateTimeFormatterHelper
    {
        private const string format_iso8601 = "yyyy-MM-dd'T'HH:mm:ssZ";  //2017-01-01T00:00:00Z

        public static string GetAsIso8601String(DateTime dateTime)
        {
            string dateTimeIso8601 = dateTime.ToString(format_iso8601, CultureInfo.InvariantCulture);

            return dateTimeIso8601;
        }
    }
}
