using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.DeviceClient
{
    public class DeviceMeasurementEventArgs
    {
        public DeviceMeasurementEventArgs() { }
        public DeviceMeasurementEventArgs(string deviceType, string deviceId, IDeviceMeasurement deviceMeasurement)
        {
            DeviceType = deviceType;
            DeviceId = deviceId;
            DeviceMeasurement = deviceMeasurement;
        }

        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        public IDeviceMeasurement DeviceMeasurement { get; set; }
    }
}
