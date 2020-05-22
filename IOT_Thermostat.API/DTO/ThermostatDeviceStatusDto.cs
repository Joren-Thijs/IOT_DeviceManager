using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.DTO.Interfaces;

namespace IOT_Thermostat.API.DTO
{
    public class ThermostatDeviceStatusDto : IDeviceStatusDto
    {
        public bool OnStatus { get; set; }
    }
}
