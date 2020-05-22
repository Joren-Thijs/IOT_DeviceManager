using System;
using System.Threading;
using System.Threading.Tasks;
using IOT_Thermostat.API.DeviceClient;
using IOT_Thermostat.API.Entity.Device;
using IOT_Thermostat.API.Entity.Interfaces;
using IOT_Thermostat.API.Entity.ThermostatDevice;
using IOT_Thermostat.API.Repositories;
using Microsoft.Extensions.Hosting;

namespace IOT_Thermostat.API.Services
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

        private async Task DeviceClientOnDeviceMeasurementReceived(object sender, DeviceMeasurementEventArgs e)
        {
            var device = await _deviceRepository.GetDevice(e.DeviceId);
            if (device == null)
            {
                device = await AddDeviceToDeviceRepository(e);
            }

            await UpdateCurrentDeviceStatus(e, device);

            await _deviceRepository.AddMeasurement(e.DeviceId, e.DeviceMeasurement);
            await _deviceRepository.Save();
        }

        private async Task UpdateCurrentDeviceStatus(DeviceMeasurementEventArgs e, IDevice device)
        {
            device.Status = e.DeviceMeasurement.Status;
            await _deviceRepository.UpdateDevice(device);
            await _deviceRepository.Save();
        }

        private async Task<IDevice> AddDeviceToDeviceRepository(DeviceMeasurementEventArgs e)
        {
            IDevice device = e.DeviceType switch
            {
                "device" => new Device(),
                "thermostat" => new ThermostatDevice(),
                _ => new Device()
            };
            device.Id = e.DeviceId;
            device.Status = e.DeviceMeasurement.Status;

            device = await _deviceRepository.AddDevice(device);
            await _deviceRepository.Save();
            return device;
        }
    }
}
