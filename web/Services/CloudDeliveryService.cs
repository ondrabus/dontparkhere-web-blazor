using System.Net.Http;
using KenticoCloud.Delivery;
using Microsoft.Extensions.Configuration;

namespace DontParkHere.Services
{
    public class CloudDeliveryService
    {
        private IConfiguration _configuration;
        private HttpClient _httpClient;
        private ITypeProvider _typeProvider;
        private IDeliveryClient _deliveryClient;

        public CloudDeliveryService(IConfiguration configuration, HttpClient httpClient, ITypeProvider typeProvider)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _typeProvider = typeProvider;
        }


        public IDeliveryClient GetDeliveryClient()
        {
            if (_deliveryClient == null)
            {
                var projectId = _configuration.GetValue<string>("DeliveryOptions:ProjectId");
                _deliveryClient = DeliveryClientBuilder.WithProjectId(projectId).WithHttpClient(_httpClient).WithTypeProvider(_typeProvider).Build();
            }

            return _deliveryClient;
        }
    }
}
