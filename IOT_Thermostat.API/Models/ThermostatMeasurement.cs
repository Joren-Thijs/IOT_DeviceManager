using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOT_Thermostat.API.Models
{
    public class ThermostatMeasurement : IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public IDeviceStatus Status { get; set; } = new DeviceStatus();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}