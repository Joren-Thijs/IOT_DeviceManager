using System.Collections.Generic;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Helpers.Web;

namespace IOT_DeviceManager.API.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<IDevice>> GetDevices();
        Task<Paginator<IDevice>> GetDevices(ResourceParameters resourceParameters);
        Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds);
        Task<IDevice> GetDevice(string deviceId);
        Task<IDevice> AddDevice(IDevice device);
        Task<IDevice> UpdateDevice(IDevice device);
        Task DeleteDevice(IDevice device);
        Task<bool> DeviceExists(string device);

        Task<IDeviceStatus> GetDeviceStatus(string deviceId);

        Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId);
        Task<Paginator<IDeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters);
        Task<IDeviceMeasurement> GetMeasurement(string deviceId, string measurementId);
        Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement);
        Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement);
        Task DeleteMeasurement(IDeviceMeasurement measurement);
        Task<bool> MeasurementExists(string deviceId, string measurementId);

        Task<bool> Save();
    }
}
