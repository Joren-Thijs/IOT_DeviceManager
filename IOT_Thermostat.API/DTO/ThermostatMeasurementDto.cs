using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.DTO
{
    public class ThermostatMeasurementDto
    {
        public string Id { get; set; }
        public float Temperature { get; set; }
        public float SetPoint { get; set; }
        public IDeviceStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
