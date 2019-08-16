using System;

namespace KenticoCloudModels
{
    public partial class Duration
    {
        public bool IsActive
        {
            get
            {
                var secondsFrom = ((this.TimeFrom.Value.Hour * 60 + this.TimeFrom.Value.Minute) * 60) + this.TimeFrom.Value.Second;

                var secondsTo = ((this.TimeTo.Value.Hour * 60 + this.TimeTo.Value.Minute) * 60) + this.TimeTo.Value.Second;
                if (secondsTo == 0)
                {
                    secondsTo = 24 * 60 * 60;
                }

                var timeNow = new DateTime();
                var secondsNow = ((timeNow.Hour * 60 + timeNow.Minute) * 60) + timeNow.Second;

                return secondsFrom <= secondsNow && secondsTo >= secondsNow;
            }
        }
    }
}
