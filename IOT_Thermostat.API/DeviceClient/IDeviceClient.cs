using System;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.DeviceClient
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
