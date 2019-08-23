using DontParkHere.Models;
using DontParkHere.Services;
using KenticoCloudModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
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

        protected Car NewCar = new Car();
        protected IEnumerable<Area> Areas { get; set; }
        protected IEnumerable<Car> Cars { get; set; }

        protected void AddCar()
        {
            try
            {
                var car = NewCar;
                CarService.SaveCar(car);
                NewCar = new Car();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Areas = new List<Area>();
            Cars = new List<Car>();

            Areas = await AreaService.GetAllAreasAsync();
            Console.WriteLine("Got " + Areas.Count() + " areas");
            Cars = await CarService.GetCars();
        }
    }
}
