﻿using System.Collections.Generic;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Entity.ThermostatDevice
{
    public class ThermostatDeviceStatus : IDeviceStatus
    {
        public ThermostatDeviceStatus() { }
        public ThermostatDeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
        public double SetPoint { get; set; }
    }
}
