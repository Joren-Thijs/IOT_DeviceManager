using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO
{
    public class ThermostatDeviceDto : IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public float SetPoint { get; set; }
    }
}
