using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.DeviceClient;
using MQTTnet.Client.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.Services
{
    public class DeviceClientService : IHostedService
    {
        private IDeviceClient Client;

        public DeviceClientService(IDeviceClient client)
        {
            Client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Client.StartClientAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Client.StopClientAsync();
        }

        public Task SetDeviceStatusAsync(CancellationToken cancellationToken)
        {
            return Client.StopClientAsync();
        }
    }
}
