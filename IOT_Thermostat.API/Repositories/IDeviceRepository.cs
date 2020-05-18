using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Repositories
{
    public interface IDeviceRepository
    {
        IEnumerable<IDevice> GetDevices();
        IEnumerable<IDevice> GetDevices(IEnumerable<string> deviceIds);
        IDevice GetDevice(string deviceId);
        IDevice AddDevice(IDevice device);
        IDevice UpdateDevice(IDevice device);
        void DeleteDevice(IDevice device);
        bool DeviceExists(string device);

        IEnumerable<DeviceMeasurement> GetMeasurements(string deviceId);
        DeviceMeasurement GetMeasurement(string deviceId, string measurementId);
        DeviceMeasurement AddMeasurement(string deviceId, DeviceMeasurement measurement);
        DeviceMeasurement UpdateMeasurement(DeviceMeasurement measurement);
        void DeleteMeasurement(DeviceMeasurement measurement);
        bool MeasurementExists(string deviceId, string measurementId);

        bool Save();
    }
}
