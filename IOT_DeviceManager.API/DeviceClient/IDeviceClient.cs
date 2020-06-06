using System;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.DeviceClient
{
    public interface IDeviceClient
    {
        Task StartClientAsync();
        Task StopClientAsync();
        Task<DeviceStatus> SetDeviceStatus(Device device, DeviceStatus status);
        event EventHandler<DeviceMeasurementEventArgs> DeviceMeasurementReceived;
        void OnDeviceMeasurementReceived(DeviceMeasurementEventArgs eventArgs);
    }
}
