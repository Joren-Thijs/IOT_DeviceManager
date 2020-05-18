using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOT_Thermostat.API.Models
{
    public class DeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}