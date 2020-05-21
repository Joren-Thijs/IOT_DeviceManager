using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var objectToCompare = (DeviceMeasurement)obj;
            return objectToCompare.Id == Id &&
                   objectToCompare.Status.OnStatus == Status.OnStatus &&
                   objectToCompare.TimeStamp == TimeStamp &&
                   objectToCompare.Device == Device &&
                   objectToCompare.DeviceId == DeviceId;
        }
    }
}
