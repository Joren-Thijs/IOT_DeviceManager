using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace IOT_Thermostat.API.Services
{
    public class MQTTHostService : BackgroundService
    {
        public IServiceProvider Services { get; }

        public MQTTHostService(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartClient(stoppingToken);
        }

        protected async Task StartClient(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var mqttCLientService =
                    scope.ServiceProvider
                        .GetRequiredService<IDeviceClient>();

                await mqttCLientService.StartDeviceClientAsync(stoppingToken);
            }
        }
    }
}
