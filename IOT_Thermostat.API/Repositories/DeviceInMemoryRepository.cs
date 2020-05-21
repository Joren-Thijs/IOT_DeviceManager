using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Models.Interfaces;

namespace IOT_Thermostat.API.Repositories
{
    public class DeviceInMemoryRepository : IDeviceRepository
    {
        private static readonly List<IDevice> _devices = new List<IDevice>();

        public Task<IEnumerable<IDevice>> GetDevices()
        {
            return Task.FromResult<IEnumerable<IDevice>>(_devices);
        }

        public Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            var devicesList = (List<IDevice>)_devices.Where(dev => deviceIds.Any(id => id == dev.Id));
            return Task.FromResult<IEnumerable<IDevice>>(devicesList);
        }

        public Task<IDevice> GetDevice(string deviceId)
        {
            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(device);
        }

        public Task<IDevice> AddDevice(IDevice device)
        {
            var existingDevice = _devices.FirstOrDefault(dev => dev.Id == device.Id);
            if (existingDevice != null)
            {
                throw new ArgumentException("Device already exists");
            }
            _devices.Add(device);
            return Task.FromResult(device);
        }

        public Task<IDevice> UpdateDevice(IDevice device)
        {
            var deviceToUpdate = _devices.FirstOrDefault(dev => dev.Id == device.Id);
            _devices.Remove(deviceToUpdate);
            _devices.Add(device);
            return Task.FromResult(device);
        }

        public Task DeleteDevice(IDevice device)
        {
            _devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task<bool> DeviceExists(string deviceId)
        {
            var existingDevice = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(existingDevice != null);
        }

        public Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            return Task.FromResult(device.Measurements);
        }

        public Task<IDeviceMeasurement> GetMeasurement(string deviceId, string measurementId)
        {
            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();
            var measurement = measurementsList.FirstOrDefault(mes => mes.Id == measurementId);

            return Task.FromResult(measurement);
        }

        public Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();

            var existingMeasurement = measurementsList.FirstOrDefault(mes => mes.Id == measurement.Id);
            if (existingMeasurement != null)
            {
                throw new ArgumentException("Measurement already exists");
            }

            measurement.DeviceId = deviceId;
            measurement.Device = device;
            if (measurement.Id == null)
            {
                measurement.Id = Convert.ToString(measurementsList.Count + 1);
            }

            measurementsList.Add(measurement);
            device.Measurements = measurementsList;
            return Task.FromResult(measurement);
        }

        public Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            _devices.ForEach(
                device =>
                {
                    var measurementsList = device.Measurements.ToList();
                    var existingMeasurement = measurementsList.FirstOrDefault(mes => mes.Id == measurement.Id);
                    measurementsList.Remove(existingMeasurement);
                    measurementsList.Add(measurement);
                    device.Measurements = measurementsList;
                });
            return Task.FromResult(measurement);
        }

        public Task DeleteMeasurement(IDeviceMeasurement measurement)
        {
            _devices.ForEach(
                device =>
                {
                    var measurementsList = device.Measurements.ToList();
                    var existingMeasurement = measurementsList.FirstOrDefault(mes => mes.Id == measurement.Id);
                    measurementsList.Remove(existingMeasurement);
                    device.Measurements = measurementsList;
                });
            return Task.CompletedTask;
        }

        public Task<bool> MeasurementExists(string deviceId, string measurementId)
        {
            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();
            var measurement = measurementsList.FirstOrDefault(mes => mes.Id == measurementId);

            return Task.FromResult(measurement != null);
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }
    }
}
