using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOT_DeviceManager.API.Entity.Interfaces
{
    public interface IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public IDeviceStatus Status { get; set; }
        public IDictionary<string, object> Values { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}
