using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IOT_Thermostat.API.Models.Device;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Models.ThermostatDevice
{
    public class ThermostatMeasurement : IDeviceMeasurement
    {
        [Key]
        public string Id { get; set; }
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public IDeviceStatus Status { get; set; } = new ThermostatDeviceStatus();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [ForeignKey("DeviceId")]
        public IDevice Device { get; set; }
        public string DeviceId { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var objectToCompare = (ThermostatMeasurement)obj;
            return objectToCompare.Id == Id &&
                   objectToCompare.Temperature == Temperature &&
                   objectToCompare.SetPoint == SetPoint &&
                   objectToCompare.Status.OnStatus == Status.OnStatus &&
                   objectToCompare.TimeStamp == TimeStamp &&
                   objectToCompare.Device == Device &&
                   objectToCompare.DeviceId == DeviceId;
        }
    }
}