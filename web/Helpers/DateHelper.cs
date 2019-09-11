using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Helpers
{
    public static class DateHelper
    {
        public static int DayOfWeekInt(this DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek - 1;
            if (dayOfWeek == -1)
            {
                dayOfWeek = 6;
            }

            return dayOfWeek;
        }

        public static int SecondsFromMidnight(this DateTime date)
        {
            return ((date.Hour * 60 + date.Minute) * 60) + date.Second;
        }
    }
}
