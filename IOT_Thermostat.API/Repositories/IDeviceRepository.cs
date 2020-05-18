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

        IEnumerable<IDeviceMeasurement> GetMeasurements(string deviceId);
        IDeviceMeasurement GetMeasurement(string deviceId, string measurementId);
        IDeviceMeasurement AddMeasurement(string deviceId, IDeviceMeasurement measurement);
        IDeviceMeasurement UpdateMeasurement(IDeviceMeasurement measurement);
        void DeleteMeasurement(IDeviceMeasurement measurement);
        bool MeasurementExists(string deviceId, string measurementId);

        bool Save();
    }
}
