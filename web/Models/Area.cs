using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace KenticoCloudModels
{
    public partial class Area
    {
        public List<Location> GetPolygon()
        {
            var data = JsonSerializer.Deserialize<List<List<double>>>(this.AreaData);
            return data.Select(c => new Location { Latitude = Convert.ToDecimal(c[0]), Longitude = Convert.ToDecimal(c[1]), Accuracy = 1 }).ToList();
        }

        public bool IsPointInside(Location point)
        {
            var polygon = GetPolygon();
            int i, j;
            bool c = false;
            for (i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((((polygon[i].Latitude <= point.Latitude) && (point.Latitude < polygon[j].Latitude))
                        || ((polygon[j].Latitude <= point.Latitude) && (point.Latitude < polygon[i].Latitude)))
                        && (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) * (point.Latitude - polygon[i].Latitude)
                            / (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
                {

                    c = !c;
                }
            }

            return c;
        }

        public AreaPriority PriorityEnum
        {
            get
            {
                var priority = this.Priority.Single();
                return (AreaPriority)Enum.Parse(typeof(AreaPriority), priority.Codename, true);
            }
        }
    }
}
