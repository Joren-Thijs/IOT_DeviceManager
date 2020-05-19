using System;
using System.Threading.Tasks;
using IOT_Thermostat.API.DeviceClient;
using IOT_Thermostat.API.Models;

namespace Mqtt.Client.AspNetCore.DeviceClient
{
    public interface IDeviceClient
    {
        Task StartClientAsync();
        Task StopClientAsync();
        Task SetDeviceStatus(string deviceName);
        event EventHandler<DeviceMeasurementEventArgs> DeviceMeasurementReceived;
        void OnDeviceMeasurementReceived(DeviceMeasurementEventArgs eventArgs);
    }
}
