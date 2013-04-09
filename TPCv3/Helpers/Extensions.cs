using System;
using System.Configuration;

namespace TPCv3.Helpers{
    public static class Extensions{
        #region Public Methods and Operators

        public static string ToConfigLocalTime(this DateTime utcDt){
            var istTz = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return utcDt.ToLongDateString() + " " + String.Format(
                "{0} ({1})",
                TimeZoneInfo.ConvertTimeFromUtc(utcDt, istTz).ToLongTimeString(),
                ConfigurationManager.AppSettings["TimezoneAbbr"]);
        }

        #endregion
    }
}