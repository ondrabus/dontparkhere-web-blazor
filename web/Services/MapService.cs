using AspNetMonsters.Blazor.Geolocation;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class MapService
    {
        static Action<Location> _callback;

        public void WatchLocation(Action<Location> watchCallback)
        {
            _callback = watchCallback;
        }

        [JSInvokable]
        public static void SetLocation(double latitude, double longitude, double accuracy)
        {
            var location = new Location
            {
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Accuracy = Convert.ToDecimal(accuracy)
            };
            SetLocation(location);
        }

        public static void SetLocation(Location location)
        {
            _callback.Invoke(location);
        }
    }
}
