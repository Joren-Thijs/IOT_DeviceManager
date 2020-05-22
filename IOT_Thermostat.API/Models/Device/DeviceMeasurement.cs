using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Models.Device
{
    public class DeviceMeasurement : IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public IDeviceStatus Status { get; set; } = new DeviceStatus();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}
