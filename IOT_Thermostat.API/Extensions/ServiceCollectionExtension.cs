using IOT_Thermostat.API.DeviceClient;
using IOT_Thermostat.API.DeviceClient.MqttClient;
using IOT_Thermostat.API.Repositories;
using IOT_Thermostat.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace IOT_Thermostat.API.Extensions
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
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<DeviceClientService>();
            });
            return services;
        }
    }
}
