using AspNetMonsters.Blazor.Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KenticoCloudModels
{
    public partial class ParkingMachine
    {
        public Location LocationObj
        {
            get
            {
                var data = JsonSerializer.Deserialize<List<double>>(this.Location);
                return new Location
                {
                    Latitude = Convert.ToDecimal(data[0]),
                    Longitude = Convert.ToDecimal(data[1])
                };
            }
        }
    }
}
