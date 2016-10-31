using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationSimilator
{
    public static class Extenstion
    {
        public static DateTime Truncate(this DateTime datetime, TimeSpan timespan)
        {
            if (timespan == TimeSpan.Zero)
                return datetime;

            return datetime.AddTicks(-(datetime.Ticks % timespan.Ticks));
        }
    }
}
