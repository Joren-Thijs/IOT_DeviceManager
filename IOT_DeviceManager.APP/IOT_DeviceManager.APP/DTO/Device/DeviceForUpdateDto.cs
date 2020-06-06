using IOT_DeviceManager.APP.DTO.Interfaces;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceForUpdateDto : IDeviceDto
    {
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
    }
}
