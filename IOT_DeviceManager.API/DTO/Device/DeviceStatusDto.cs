using System.Collections.Generic;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceStatusDto
    {
        public bool OnStatus { get; set; }
        public IEnumerable<DeviceSettingDto> Settings { get; set; }
    }
}
