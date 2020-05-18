using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Thermostat.API.Test.TestHelpers
{
    class DeviceStatusOnByDefault : IDeviceStatus
    {
        public bool OnStatus { get; set; } = true;
    }
}
