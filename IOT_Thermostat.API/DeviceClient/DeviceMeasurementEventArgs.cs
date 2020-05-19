using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models;

namespace IOT_Thermostat.API.DeviceClient
{
    public class DeviceMeasurementEventArgs
    {
        public DeviceMeasurementEventArgs() { }
        public DeviceMeasurementEventArgs(string deviceId, IDeviceMeasurement deviceMeasurement)
        {
            DeviceId = deviceId;
            DeviceMeasurement = deviceMeasurement;
        }

        public string DeviceId { get; set; }
        public IDeviceMeasurement DeviceMeasurement { get; set; }
    }
}
