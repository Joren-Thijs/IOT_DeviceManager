using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class DeviceMeasurement : IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public IDeviceStatus Status { get; set; } = new DeviceStatus();
        public IDictionary<string, object> Values { get; set; } = new Dictionary<string, object>();
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}
