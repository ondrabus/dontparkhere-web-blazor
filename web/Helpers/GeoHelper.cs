using AspNetMonsters.Blazor.Geolocation;
using GeoCoordinatePortable;
using System;

namespace DontParkHere.Helpers
{
    public static class GeoHelper
    {
        public static double GetDistanceTo(this Location source, Location target)
        {
            var sourceCoord = new GeoCoordinate(Convert.ToDouble(source.Latitude), Convert.ToDouble(source.Longitude));
            var targetCoord = new GeoCoordinate(Convert.ToDouble(target.Latitude), Convert.ToDouble(target.Longitude));

            return sourceCoord.GetDistanceTo(targetCoord);
        }
    }
}
