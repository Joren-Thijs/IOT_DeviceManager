using System;
using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceDto : IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public DateTime LastSeen { get; set; }
        public bool Online { get; set; }
    }
}
