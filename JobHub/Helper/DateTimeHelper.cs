using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace JobHub.Helper
{
    public class DateTimeHelper
    {
        public static DateTime ConvertJavaMiliSecondToDateTime(long javaMS)
        {

            DateTime UTCBaseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            DateTime dt = UTCBaseTime.Add(new TimeSpan(javaMS *

            TimeSpan.TicksPerMillisecond)).ToLocalTime();

            return dt;

        }
        public static long ConvertDateTimeToJavaMiliSecond(DateTime dt)
        {

           // DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - dt;
            long time = (long)javaSpan.TotalMilliseconds / 1000;
            return time;
        }
    }
}
