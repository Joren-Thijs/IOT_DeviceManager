using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Models.ThermostatDevice
{
    public class ThermostatDevice : IDevice
    {
        [Key]
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatus Status { get; set; }
        public float SetPoint { get; set; }
        public IEnumerable<IDeviceMeasurement> Measurements { get; set; } = new List<ThermostatMeasurement>();
    }
}
