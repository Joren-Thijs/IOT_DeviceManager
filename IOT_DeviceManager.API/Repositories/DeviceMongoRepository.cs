using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Helpers.Web;

namespace IOT_DeviceManager.API.Repositories
{

    public class DeviceMongoRepository : IDeviceRepository
    {
        public Task<IDevice> AddDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMeasurement(IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeviceExists(string device)
        {
            throw new NotImplementedException();
        }

        public Task<IDevice> GetDevice(string deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDevice>> GetDevices()
        {
            throw new NotImplementedException();
        }

        public Task<Paginator<IDevice>> GetDevices(ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceStatus> GetDeviceStatus(string deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<Paginator<IDeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MeasurementExists(string deviceId, Guid measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<IDevice> UpdateDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}