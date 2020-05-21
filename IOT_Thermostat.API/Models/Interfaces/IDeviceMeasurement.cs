using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOT_Thermostat.API.Models.Interfaces
{
    public interface IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public IDeviceStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }
    }
}
