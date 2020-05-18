﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Models
{
    public interface IDevice
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public DeviceStatus Status { get; set; }
        public IEnumerable<DeviceMeasurement> Measurements { get; set; }
    }
}
