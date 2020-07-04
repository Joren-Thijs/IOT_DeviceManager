using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.DeviceClient
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
