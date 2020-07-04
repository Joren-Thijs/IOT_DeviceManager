using System;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.DeviceClient
{
    public interface IDeviceClient
    {
        Task StartClientAsync();
        Task StopClientAsync();
        Task<IDeviceStatus> SetDeviceStatus(IDevice device, IDeviceStatus status);
        event EventHandler<DeviceMeasurementEventArgs> DeviceMeasurementReceived;
        event EventHandler<DeviceDisconnectedEventArgs> DeviceDisconnected;
    }
}
