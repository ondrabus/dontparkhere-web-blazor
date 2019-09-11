using DontParkHere.Models;
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

        public async Task<List<Car>> GetCarsAsync()
        {
            return (await _jsRuntime.InvokeAsync<Car[]>("getCars")).ToList();
        }

        public async void SaveCarAsync(Car car)
        {
            await _jsRuntime.InvokeAsync<object>("setCar", car);
        }
    }
}
