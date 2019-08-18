using AspNetMonsters.Blazor.Geolocation;
using KenticoCloudModels;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontParkHere.Helpers;

namespace DontParkHere.Services
{
    public class ParkingMachineService
    {
        private CloudDeliveryService _cloudDeliveryService;
        private IJSRuntime _jsRuntime;
        private static IReadOnlyList<ParkingMachine> _parkingMachines = null;

        public ParkingMachineService(CloudDeliveryService cloudDeliveryService, IJSRuntime jsRuntime)
        {
            _cloudDeliveryService = cloudDeliveryService;
            _jsRuntime = jsRuntime;
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

        public async Task<List<ParkingMachine>> GetNearestParkingMachines(Location point, int count = 2)
        {
            var parkingMachines = await GetAllParkingMachinesAsync();
            return parkingMachines.OrderBy(p => p.LocationObj.GetDistanceTo(point)).Take(count).ToList();
        }
    }
}
