﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO
{
    public class ThermostatDeviceStatusDto : IDeviceStatusDto
    {
        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
        public double SetPoint { get; set; }
    }
}
