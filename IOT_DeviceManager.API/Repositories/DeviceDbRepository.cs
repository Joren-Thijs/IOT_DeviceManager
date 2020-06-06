using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Helpers.Web;
using IOT_DeviceManager.API.Repositories.DbContexts;

namespace IOT_DeviceManager.API.Repositories
{
    public class DeviceDbRepository : IDeviceRepository
    {
        private readonly DeviceRepositoryContext _deviceRepositoryContext;

        public DeviceDbRepository(DeviceRepositoryContext deviceRepositoryContext)
        {
            _deviceRepositoryContext = deviceRepositoryContext ?? throw new ArgumentNullException(nameof(deviceRepositoryContext));
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            throw new NotImplementedException();
        }

        public async Task<Paginator<Device>> GetDevices(ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Device>> GetDevices(IEnumerable<string> deviceIds)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> GetDevice(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> AddDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> UpdateDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeviceExists(string device)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceStatus> GetDeviceStatus(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeviceMeasurement>> GetMeasurements(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Paginator<DeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceMeasurement> AddMeasurement(string deviceId, DeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceMeasurement> UpdateMeasurement(DeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMeasurement(DeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MeasurementExists(string deviceId, Guid measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
