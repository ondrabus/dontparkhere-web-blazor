using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System;
using KenticoCloud.Delivery;
using KenticoCloudModels;
using DontParkHere.Services;
using AspNetMonsters.Blazor.Geolocation;

namespace DontParkHere
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton((s) =>
            {
                return GetConfiguration();
            });

            services.AddSingleton<ITypeProvider, CustomTypeProvider>();
            services.AddSingleton<CloudDeliveryService>();
            services.AddSingleton<AreaService>();
            services.AddSingleton<LocationService>();
            services.AddSingleton<MapService>();
            services.AddSingleton<RestrictionService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<ParkingMachineService>();
        }

        private IConfiguration GetConfiguration()
        {
            System.Reflection.Assembly assembly = GetType().Assembly;
            var resource = assembly.GetName().Name + ".Configuration.appsettings.json";
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            if (assembly.GetManifestResourceNames().Contains(resource))
            {
                string result = null;
                using (Stream stream = assembly.GetManifestResourceStream(resource))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                configurationBuilder.AddJsonFile(new InMemoryFileProvider(result), resource, false, false);
            }

            return configurationBuilder.Build();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
