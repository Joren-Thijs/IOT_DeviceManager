using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<IDevice>> GetDevices();
        Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds);
        Task<IDevice> GetDevice(string deviceId);
        Task<IDevice> AddDevice(IDevice device);
        Task<IDevice> UpdateDevice(IDevice device);
        Task DeleteDevice(IDevice device);
        Task<bool> DeviceExists(string device);

        Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId);
        Task<IDeviceMeasurement> GetMeasurement(string deviceId, string measurementId);
        Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement);
        Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement);
        Task DeleteMeasurement(IDeviceMeasurement measurement);
        Task<bool> MeasurementExists(string deviceId, string measurementId);

        Task<bool> Save();
    }
}
