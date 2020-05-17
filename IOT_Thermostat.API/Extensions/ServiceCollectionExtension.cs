using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.DeviceClient;
using Mqtt.Client.AspNetCore.Options;
using Mqtt.Client.AspNetCore.Services;
using Mqtt.Client.AspNetCore.Settings;
using MQTTnet.Client.Options;
using System;

namespace Mqtt.Client.AspNetCore.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDeviceClientHostedService(this IServiceCollection services)
        {
            services.AddMqttDeviceClientConfigurationService(aspOptionBuilder =>
            {
                var clientSettings = AppSettingsProvider.ClientSettings;
                var brokerHostSettings = AppSettingsProvider.BrokerHostSettings;

                aspOptionBuilder
                .WithCredentials(clientSettings.UserName, clientSettings.Password)
                .WithClientId(clientSettings.Id)
                .WithTcpServer(brokerHostSettings.Host, brokerHostSettings.Port);
            });
            services.AddTransient<IDeviceClient, AspMqttClient>();
            services.AddSingleton<DeviceClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<DeviceClientService>();
            });
            return services;
        }

        private static IServiceCollection AddMqttDeviceClientConfigurationService(this IServiceCollection services, Action<AspCoreMqttClientOptionBuilder> configure)
        {
            services.AddSingleton(serviceProvider =>
            {
                var optionBuilder = new AspCoreMqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            return services;
        }
    }
}
