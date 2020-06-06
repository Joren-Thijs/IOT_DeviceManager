using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.DeviceClient
{
    public class DeviceMeasurementEventArgs
    {
        public DeviceMeasurementEventArgs() { }
        public DeviceMeasurementEventArgs(string deviceType, string deviceId, DeviceMeasurement deviceMeasurement)
        {
            DeviceType = deviceType;
            DeviceId = deviceId;
            DeviceMeasurement = deviceMeasurement;
        }

        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        public DeviceMeasurement DeviceMeasurement { get; set; }
    }
}
