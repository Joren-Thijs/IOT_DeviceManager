using IOT_DeviceManager.APP.DTO.Interfaces;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceDto : IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
    }
}
