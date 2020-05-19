using System;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Mqtt.Client.AspNetCore.DeviceClient;
using System.Threading;
using System.Threading.Tasks;
using IOT_Thermostat.API.DeviceClient;
using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Repositories;

namespace Mqtt.Client.AspNetCore.Services
{
    public class DeviceClientService : IHostedService
    {
        private IDeviceClient _deviceClient;
        private IDeviceRepository _deviceRepository;

        public DeviceClientService(IDeviceClient deviceClient, IDeviceRepository deviceRepository)
        {
            _deviceClient = deviceClient ??
                throw new ArgumentNullException(nameof(deviceClient));
            _deviceRepository = deviceRepository ??
                throw new ArgumentNullException(nameof(deviceRepository));
            _deviceClient.DeviceMeasurementReceived += new EventHandler<DeviceMeasurementEventArgs>(
                async (s, e) =>
                {
                    await DeviceClientOnDeviceMeasurementReceived(s, e);
                });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _deviceClient.StartClientAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _deviceClient.StopClientAsync();
        }

        public Task SetDeviceStatusAsync(CancellationToken cancellationToken)
        {
            return _deviceClient.StopClientAsync();
        }

        private async Task DeviceClientOnDeviceMeasurementReceived(object? sender, DeviceMeasurementEventArgs e)
        {
            var device = await _deviceRepository.GetDevice(e.DeviceId);
            if (device == null)
            {
                // Create new device and ad it to the database
                device = new ThermostatDevice
                {
                    Id = e.DeviceId,
                    Status = e.DeviceMeasurement.Status
                };

                await _deviceRepository.AddDevice(device);
                await _deviceRepository.Save();
            }

            await _deviceRepository.AddMeasurement(e.DeviceId, e.DeviceMeasurement);
            await _deviceRepository.Save();
        }
    }
}
