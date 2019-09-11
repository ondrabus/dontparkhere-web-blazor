using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Models;
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
    public class SettingsBase : ComponentBase
    {
        [Inject]
        protected AreaService AreaService { get; set; }

        [Inject]
        protected CarService CarService { get; set; }

        protected IReadOnlyList<Area> Areas { get; set; }

        protected Car NewCar = new Car();

        protected List<Car> Cars { get; set; }

        public SettingsBase()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            Areas = new List<Area>();
            Cars = new List<Car>();

            Areas = await AreaService.GetAllAreasAsync();
            Cars = await CarService.GetCarsAsync();
        }

        protected void AddCar()
        {
            var car = NewCar;
            CarService.SaveCarAsync(car);
            Cars.Add(car);
            NewCar = new Car();

            StateHasChanged();
        }

    }
}
