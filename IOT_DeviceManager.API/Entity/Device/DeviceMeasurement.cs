using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class DeviceMeasurement
    {
        [Key]
        public Guid Id { get; set; }
        public DeviceStatus Status { get; set; } = new DeviceStatus();
        public IEnumerable<MeasurementValue> Values { get; set; } = new List<MeasurementValue>();
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
        public string DeviceId { get; set; }
    }
}
