﻿using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Models.Device
{
    public class DeviceStatus : IDeviceStatus
    {
        public DeviceStatus() { }
        public DeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
    }
}