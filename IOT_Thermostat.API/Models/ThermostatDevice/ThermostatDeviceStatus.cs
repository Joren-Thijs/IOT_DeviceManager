﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Models.ThermostatDevice
{
    public class ThermostatDeviceStatus : IDeviceStatus
    {
        public ThermostatDeviceStatus() { }
        public ThermostatDeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
    }
}
