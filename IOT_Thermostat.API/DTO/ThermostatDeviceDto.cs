using IOT_Thermostat.API.DTO.Interfaces;

namespace IOT_Thermostat.API.DTO
{
    public class ThermostatDeviceDto : IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public float SetPoint { get; set; }
    }
}
