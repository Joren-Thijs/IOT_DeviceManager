using System;

namespace IOT_Thermostat.API.Models
{
    public class Measurement
    {
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public bool On { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}