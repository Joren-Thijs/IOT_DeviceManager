using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceForUpdateDto : IDeviceDto
    {
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
    }
}
