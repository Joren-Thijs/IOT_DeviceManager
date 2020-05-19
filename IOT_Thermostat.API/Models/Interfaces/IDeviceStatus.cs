using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Models
{
    public interface IDeviceStatus
    {
        public bool OnStatus { get; set; }
    }
}
