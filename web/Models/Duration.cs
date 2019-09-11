using DontParkHere.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KenticoCloudModels
{
    public partial class Duration
    {
        public IEnumerable<DurationDayOfWeek> DaysOfWeekEnum
        {
            get
            {
                return this.DaysOfWeek.Select(d => (DurationDayOfWeek)Enum.Parse(typeof(DurationDayOfWeek), d.Codename.ToString(), true));
            }
        }
    }
}
