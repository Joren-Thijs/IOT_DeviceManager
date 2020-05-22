using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.DTO.Interfaces;
using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.DTO
{
    public class ThermostatDeviceDto : IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public float SetPoint { get; set; }
    }
}
