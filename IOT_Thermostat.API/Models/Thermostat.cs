using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Models
{
    public class Thermostat
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public Status Status { get; set; }
        public IEnumerable<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
