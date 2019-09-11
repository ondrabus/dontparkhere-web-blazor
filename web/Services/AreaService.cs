using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetMonsters.Blazor.Geolocation;
using KenticoCloud.Delivery;
using KenticoCloudModels;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace DontParkHere.Services
{
    public class AreaService
    {
        private CloudDeliveryService _cloudDeliveryService;
        private IJSRuntime _jsRuntime;
        private static IReadOnlyList<Area> _areas = null;

        public AreaService(CloudDeliveryService cloudDeliveryService, IJSRuntime jsRuntime)
        {
            _cloudDeliveryService = cloudDeliveryService;
            _jsRuntime = jsRuntime;
        }

        public async Task<IReadOnlyList<Area>> GetAllAreasAsync()
        {
            if (_areas == null)
            {
                var data = await _cloudDeliveryService.GetDeliveryClient().GetItemsAsync<Area>(new DepthParameter(5));
                _areas = data.Items;
            }

            return _areas;
        }

        public async Task<List<Area>> GetAreasByPoint(Location point)
        {
            var areas = await GetAllAreasAsync();
            var intersectingAreas = areas.Where(a => a.IsPointInside(point)).ToList();
            return intersectingAreas;
        }

        public async Task<Area> GetAreaById(string Id)
        {
            var areas = await GetAllAreasAsync();
            return areas.SingleOrDefault(a => a.System.Id == Id);
        }

    }
}
