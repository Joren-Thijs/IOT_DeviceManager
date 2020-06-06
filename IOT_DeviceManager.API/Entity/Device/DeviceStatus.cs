using System.Collections.Generic;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class DeviceStatus
    {
        public DeviceStatus() { }
        public DeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
        public IEnumerable<DeviceSetting> Settings { get; set; } = new List<DeviceSetting>();
    }
}