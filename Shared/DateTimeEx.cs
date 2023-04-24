using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Ozarin.Shared
{
    /// <summary>
    /// DateTimeEx
    /// </summary>
    public static class DateTimeEx
    {
        public static DateTime ToJst(this DateTime utc)
        {
            var jstZoneInfo = TZConvert.GetTimeZoneInfo("Tokyo Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, jstZoneInfo);
        }

    }
}
