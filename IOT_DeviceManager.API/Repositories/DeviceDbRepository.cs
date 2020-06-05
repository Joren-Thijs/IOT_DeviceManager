using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;
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

        public async Task<IEnumerable<IDevice>> GetDevices()
        {
            throw new NotImplementedException();
        }

        public async Task<Paginator<IDevice>> GetDevices(ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IDevice> GetDevice(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDevice> AddDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public async Task<IDevice> UpdateDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeviceExists(string device)
        {
            throw new NotImplementedException();
        }

        public async Task<IDeviceStatus> GetDeviceStatus(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Paginator<IDeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IDeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMeasurement(IDeviceMeasurement measurement)
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
