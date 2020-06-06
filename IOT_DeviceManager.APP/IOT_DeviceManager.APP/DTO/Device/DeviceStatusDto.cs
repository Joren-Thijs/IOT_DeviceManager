using System.Collections.Generic;
using IOT_DeviceManager.APP.DTO.Interfaces;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceStatusDto : IDeviceStatusDto
    {
        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
    }
}
