using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Repositories
{
    public class DeviceInMemoryRepository : IDeviceRepository
    {
        private List<IDevice> devices = new List<IDevice>();

        public Task<IEnumerable<IDevice>> GetDevices()
        {
            return Task.FromResult<IEnumerable<IDevice>>(devices);
        }

        public Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            var devicesList = (List<IDevice>)devices.Where(dev => deviceIds.Any(id => id == dev.Id));
            return Task.FromResult<IEnumerable<IDevice>>(devicesList);
        }

        public Task<IDevice> GetDevice(string deviceId)
        {
            var device = devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(device);
        }

        public Task<IDevice> AddDevice(IDevice device)
        {
            var existingDevice = devices.FirstOrDefault(dev => dev.Id == device.Id);
            if (existingDevice != null)
            {
                throw new ArgumentException("Device already exists");
            }
            devices.Add(device);
            return Task.FromResult(device);
        }

        public Task<IDevice> UpdateDevice(IDevice device)
        {
            var deviceToUpdate = devices.FirstOrDefault(dev => dev.Id == device.Id);
            devices.Remove(deviceToUpdate);
            devices.Add(device);
            return Task.FromResult(device);
        }

        public Task DeleteDevice(IDevice device)
        {
            devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task<bool> DeviceExists(string deviceId)
        {
            var existingDevice = devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(existingDevice != null);
        }

        public Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            var device = (ThermostatDevice)devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }
            device.Measurements.ToList().Add(measurement);
            return Task.FromResult(measurement);
        }

        public Task DeleteMeasurement(IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceMeasurement> GetMeasurement(string deviceId, string measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MeasurementExists(string deviceId, string measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }
    }
}
