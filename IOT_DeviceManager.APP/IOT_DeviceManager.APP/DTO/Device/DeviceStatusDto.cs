using System.Collections.Generic;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceStatusDto
    {
        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
    }
}
