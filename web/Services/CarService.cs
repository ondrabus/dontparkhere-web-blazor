using DontParkHere.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class CarService
    {
        private IJSRuntime _jsRuntime;

        public CarService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<List<Car>> GetCars()
        {
            var carsData = await _jsRuntime.InvokeAsync<Car[]>("window.blazorExtensions.getCars");

            return carsData.ToList();
        }

        public async void SaveCar(Car car)
        {
            await _jsRuntime.InvokeAsync<object>("window.blazorExtensions.saveCar", car);
        }
    }
}
