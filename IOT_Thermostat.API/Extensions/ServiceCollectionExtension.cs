using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.DeviceClient;
using Mqtt.Client.AspNetCore.Services;
using MQTTnet.Client.Options;
using System;

namespace Mqtt.Client.AspNetCore.Extensions
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
    }
}
