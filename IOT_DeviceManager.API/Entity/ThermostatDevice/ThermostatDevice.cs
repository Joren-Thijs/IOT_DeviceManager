using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Entity.ThermostatDevice
{
    public class ThermostatDevice : IDevice
    {
        [Key]
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatus Status { get; set; }
        public double SetPoint { get; set; }
        public IEnumerable<IDeviceMeasurement> Measurements { get; set; } = new List<ThermostatDeviceMeasurement>();
    }
}
