using KenticoCloudModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class RestrictionService
    {
        public List<VisitorRestriction> GetActiveVisitorRestrictions(List<VisitorRestriction> restrictions)
        {
            return restrictions.Where(r => r.Duration.Cast<Duration>().Any(d => d.IsActive)).ToList();
        }
    }
}
