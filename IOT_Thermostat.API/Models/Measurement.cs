using System;

namespace IOT_Thermostat.API.Models
{
    public class Measurement
    {
        public string Id { get; set; }
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public bool On { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public IDevice Device { get; set; }

        public string DeviceId { get; set; }
    }
}