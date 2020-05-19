using IOT_Thermostat.API.DeviceClient.MqttClient;
using IOT_Thermostat.API.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.DeviceClient;
using Mqtt.Client.AspNetCore.Services;

namespace IOT_Thermostat.API.Extensions
{
    public static class ServiceCollectionExtension
    {
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

        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddTransient<IDeviceRepository, DeviceInMemoryRepository>();
            return services;
        }
    }
}
