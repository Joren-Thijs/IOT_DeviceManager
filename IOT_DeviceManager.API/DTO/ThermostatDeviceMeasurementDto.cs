using System;
using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO
{
    public class ThermostatDeviceMeasurementDto : IDeviceMeasurementDto
    {
        public string Id { get; set; }
        public double Temperature { get; set; }
        public double SetPoint { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
