using IOT_DeviceManager.API.DeviceClient;
using IOT_DeviceManager.API.DeviceClient.MqttClient;
using IOT_DeviceManager.API.Repositories;
using IOT_DeviceManager.API.Services;
using IOT_DeviceManager.API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace IOT_DeviceManager.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddControllersWithInputOutputFormatters(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    setupAction =>
                    {
                        setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    })
                .AddXmlDataContractSerializerFormatters();
            return services;
        }

        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddTransient<IDeviceRepository, DeviceInMemoryRepository>();
            return services;
        }

        public static IServiceCollection AddDeviceClientHostedService(this IServiceCollection services)
        {
            services.AddTransient<IDeviceClient, MqttDeviceClient>();
            services.AddSingleton<DeviceClientService>();
            services.AddSingleton<IHostedService>(serviceProvider => serviceProvider.GetService<DeviceClientService>());
            return services;
        }

        public static IServiceCollection AddCalculationService(this IServiceCollection services)
        {
            return services.AddTransient<ICalculationService, CalculationService>();
        }
    }
}
