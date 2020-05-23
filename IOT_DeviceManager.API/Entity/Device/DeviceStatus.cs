using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class DeviceStatus : IDeviceStatus
    {
        public DeviceStatus() { }
        public DeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
    }
}