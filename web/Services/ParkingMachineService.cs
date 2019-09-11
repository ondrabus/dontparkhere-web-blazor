using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Helpers;
using KenticoCloudModels;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class ParkingMachineService
    {
        private CloudDeliveryService _cloudDeliveryService;
        private static IReadOnlyList<ParkingMachine> _parkingMachines;
        // get parking machines from Kentico Cloud
        // invoke frontend JavaScript
        
        public ParkingMachineService(CloudDeliveryService cloudDeliveryService)
        {
            _cloudDeliveryService = cloudDeliveryService;
        }

        private async Task<IReadOnlyList<ParkingMachine>> GetAllParkingMachinesAsync()
        {
            if (_parkingMachines == null)
            {
                var data = await _cloudDeliveryService.GetDeliveryClient().GetItemsAsync<ParkingMachine>();
                _parkingMachines = data.Items;
            }

            return _parkingMachines;
        }

        public async Task<List<ParkingMachine>> GetNearestParkingMachines(Location point)
        {
            var parkingMachines = await GetAllParkingMachinesAsync();
            return parkingMachines.OrderBy(p => p.LocationObj.GetDistanceTo(point)).Take(2).ToList();
        }
    }
}
