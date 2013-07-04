using System;
using System.Configuration;
using System.Web.Mvc;
using TPCv3.Domain.Entities;

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

        public static string Href(this Post post, UrlHelper helper){
            return
                helper.RouteUrl(
                    new
                        {
                            controller = "Blog",
                            action = "Post",
                            year = post.PostedOn.Year,
                            month = post.PostedOn.Month,
                            title = post.UrlSlug
                        });
        }

        #endregion
    }
}