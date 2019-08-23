using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Services;
using KenticoCloudModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Pages
{
    public class MapBase : ComponentBase
    {
        [Inject]
        protected AreaService AreaService { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected LocationService LocationService { get; set; }

        [Inject]
        protected MapService MapService { get; set; }

        [Inject]
        protected RestrictionService RestrictionService { get; set; }

        [Inject]
        protected ParkingMachineService ParkingMachineService { get; set; }


        protected IReadOnlyList<Area> Areas { get; set; }

        public static decimal Latitude { get; set; }
        public static decimal Longitude { get; set; }
        public static Area IntersectingArea { get; set; }
        public static List<ParkingMachine> ParkingMachines { get; set; }
        public static List<VisitorRestriction> VisitorRestrictions { get; set; }
        public static bool OverlappingAreas { get; set; } = false;

        private bool _initialized = false;

        public MapBase()
        {
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (!_initialized)
            {
                _initialized = true;
                Console.WriteLine("Initializing map");
                await JSRuntime.InvokeAsync<object>("mapInit");
                MapService.WatchLocation(this.SetLocation);

                var currentLocation = await LocationService.GetLocationAsync();
                MapService.SetLocation(currentLocation);
            }
        }

        protected async void SetLocation(Location location)
        {
            Console.WriteLine($"Check this position: {location.Latitude} {location.Longitude}");
            var areas = (await AreaService.GetAreasByPoint(location)).OrderByDescending(a => a.PriorityEnum);
            Console.WriteLine($"Got {areas.Count()} areas.");

            var area = areas.OrderByDescending(a => a.PriorityEnum).FirstOrDefault();
            IntersectingArea = null;
            ParkingMachines = null;
            VisitorRestrictions = null;

            if (area != null)
            {
                if (areas.Count(a => a.PriorityEnum == area.PriorityEnum) > 1)
                {
                    OverlappingAreas = true;
                }
                else
                {
                    IntersectingArea = area;
                    VisitorRestrictions = RestrictionService.GetActiveVisitorRestrictions(area.Restrictions.Where(r => r as VisitorRestriction != null).Cast<VisitorRestriction>().ToList());

                    // draw parking machines on the map
                    var parkingMachines = await ParkingMachineService.GetNearestParkingMachines(location);
                    ParkingMachines = parkingMachines;
                    await JSRuntime.InvokeAsync<object>("mapSetParkingMachines", parkingMachines.First().LocationObj, parkingMachines.Skip(1).First().LocationObj);
                }
            }

            Latitude = location.Latitude;
            Longitude = location.Longitude;
            StateHasChanged();
        }
    }
}
