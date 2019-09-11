using DontParkHere.Helpers;
using KenticoCloud.Delivery;
using KenticoCloudModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class DurationService
    {
        private CloudDeliveryService _cloudDeliveryService;
        private static IReadOnlyList<PublicHoliday> _publicHolidays = null;

        public DurationService(CloudDeliveryService cloudDeliveryService)
        {
            _cloudDeliveryService = cloudDeliveryService;
        }

        private async Task<IReadOnlyList<PublicHoliday>> GetAllPublicHolidaysAsync()
        {
            if (_publicHolidays == null)
            {
                var data = await _cloudDeliveryService.GetDeliveryClient().GetItemsAsync<PublicHoliday>();
                _publicHolidays = data.Items;
            }

            return _publicHolidays;
        }

        public async Task<bool> IsActive(Duration duration)
        {
            var timeNow = new DateTime();

            // check day
            var isActiveDay = await IsActiveDay(timeNow, duration);

            if (!isActiveDay)
            {
                return false;
            }
            
            // check time
            var secondsFrom = duration.TimeFrom.Value.SecondsFromMidnight();
            var secondsTo = duration.TimeTo.Value.SecondsFromMidnight();
            if (secondsTo == 0)
            {
                secondsTo = 24 * 60 * 60;
            }

            var secondsNow = timeNow.SecondsFromMidnight();

            return secondsFrom <= secondsNow && secondsTo >= secondsNow;
        }

        private async Task<bool> IsActiveDay(DateTime time, Duration duration)
        {
            var publicHolidays = await GetAllPublicHolidaysAsync();

            if (duration.DaysOfWeekEnum.Any(d => d == Models.DurationDayOfWeek.Workdays))
            {
                // work days
                if (time.DayOfWeekInt() <= 4 && !publicHolidays.Any(h => h.Date.Month == time.Month && h.Date.Day == time.Day))
                {
                    return true;
                }

                return false;
            }

            if (duration.DaysOfWeekEnum.Any(d => d == Models.DurationDayOfWeek.Holidays))
            {
                if (publicHolidays.Any(h => h.Date.Month == time.Month && h.Date.Day == time.Day))
                {
                    // today is holiday
                    return true;
                }

                return false;
            }

            // any other day?
            if (duration.DaysOfWeekEnum.Any(d => (int)d == time.DayOfWeekInt()))
            {
                return true;
            }

            return false;
        }
    }
}
