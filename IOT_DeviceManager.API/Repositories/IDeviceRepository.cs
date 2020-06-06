using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Helpers.Web;

namespace IOT_DeviceManager.API.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetDevices();
        Task<Paginator<Device>> GetDevices(ResourceParameters resourceParameters);
        Task<IEnumerable<Device>> GetDevices(IEnumerable<string> deviceIds);
        Task<Device> GetDevice(string deviceId);
        Task<Device> AddDevice(Device device);
        Task<Device> UpdateDevice(Device device);
        Task DeleteDevice(Device device);
        Task<bool> DeviceExists(string device);

        Task<DeviceStatus> GetDeviceStatus(string deviceId);

        Task<IEnumerable<DeviceMeasurement>> GetMeasurements(string deviceId);
        Task<Paginator<DeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters);
        Task<DeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId);
        Task<DeviceMeasurement> AddMeasurement(string deviceId, DeviceMeasurement measurement);
        Task<DeviceMeasurement> UpdateMeasurement(DeviceMeasurement measurement);
        Task DeleteMeasurement(DeviceMeasurement measurement);
        Task<bool> MeasurementExists(string deviceId, Guid measurementId);

        Task<bool> Save();
    }
}
