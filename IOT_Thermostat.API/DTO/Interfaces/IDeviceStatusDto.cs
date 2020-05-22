using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.DTO.Interfaces
{
    public interface IDeviceStatusDto
    {
        public bool OnStatus { get; set; }
    }
}
