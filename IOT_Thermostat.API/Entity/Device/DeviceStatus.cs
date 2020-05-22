using IOT_Thermostat.API.Entity.Interfaces;

namespace IOT_Thermostat.API.Entity.Device
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