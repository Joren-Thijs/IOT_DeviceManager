namespace IOT_Thermostat.API.Models
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