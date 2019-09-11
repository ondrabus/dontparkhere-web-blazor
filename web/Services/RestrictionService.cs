using KenticoCloudModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class RestrictionService
    {
        private DurationService _durationService;

        public RestrictionService(DurationService durationService)
        {
            _durationService = durationService;
        }

        public async Task<List<VisitorRestriction>> GetActiveVisitorRestrictions(List<VisitorRestriction> restrictions)
        {
            var activeRestrictions = new List<VisitorRestriction>();

            foreach (var restriction in restrictions)
            {
                var active = false;

                foreach (Duration duration in restriction.Duration)
                {
                    var isActive = await _durationService.IsActive(duration);
                    if (isActive)
                    {
                        active = true;
                        break;
                    }
                }

                if (active)
                {
                    activeRestrictions.Add(restriction);
                }
            }

            return activeRestrictions;
        }
    }
}
