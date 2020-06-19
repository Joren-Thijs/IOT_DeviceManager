using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Helpers.Exceptions;
using IOT_DeviceManager.API.Helpers.Reflection;
using IOT_DeviceManager.API.Helpers.Web;

namespace IOT_DeviceManager.API.Repositories
{
    public class DeviceInMemoryRepository : IDeviceRepository
    {
        private static readonly List<IDevice> _devices = new List<IDevice>();

        public Task<IEnumerable<IDevice>> GetDevices()
        {
            return Task.FromResult<IEnumerable<IDevice>>(_devices);
        }

        public Task<Paginator<IDevice>> GetDevices(ResourceParameters resourceParameters)
        {
            _ = resourceParameters ?? throw new ArgumentNullException(nameof(resourceParameters));

            var enumerableDevices = _devices as IEnumerable<IDevice>;
            var devices = enumerableDevices.AsQueryable();

            // Filter on search
            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                devices = devices.Where(a => a.DeviceName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            // Order on OrderBy
            if (!string.IsNullOrWhiteSpace(resourceParameters.OrderBy))
            {
                var orderBy = resourceParameters.OrderBy.Trim();
                Expression<Func<IDevice, object>> orderByLambda;
                try
                {
                    orderByLambda = PropertyHelpers.GetPropertySelector<IDevice>(orderBy);
                }
                catch (ArgumentException e)
                {
                    throw new BadInputException(e.Message, $"The property {orderBy} does not exist");
                }

                devices = resourceParameters.SortDirection == "desc" ? devices.OrderByDescending(orderByLambda) : devices.OrderBy(orderByLambda);
            }

            return Task.FromResult(Paginator<IDevice>.Create(devices, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            _ = deviceIds ?? throw new ArgumentNullException(nameof(deviceIds));

            var devicesList = _devices.Where(dev => deviceIds.Any(id => id == dev.Id));
            return Task.FromResult<IEnumerable<IDevice>>(devicesList);
        }

        public Task<IDevice> GetDevice(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(device);
        }

        public Task<IDevice> AddDevice(IDevice device)
        {
            _ = device ?? throw new ArgumentNullException(nameof(device));

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
            _ = device ?? throw new ArgumentNullException(nameof(device));

            var deviceToUpdate = _devices.FirstOrDefault(dev => dev.Id == device.Id);
            _devices.Remove(deviceToUpdate);
            _devices.Add(device);
            return Task.FromResult(device);
        }

        public Task DeleteDevice(IDevice device)
        {
            _ = device ?? throw new ArgumentNullException(nameof(device));

            _devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task<bool> DeviceExists(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var existingDevice = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            return Task.FromResult(existingDevice != null);
        }

        public Task<IDeviceStatus> GetDeviceStatus(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            return Task.FromResult(device.Status);
        }

        public Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            return Task.FromResult(device.Measurements);
        }

        public Task<Paginator<IDeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            _ = resourceParameters ?? throw new ArgumentNullException(nameof(resourceParameters));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurements = device.Measurements.AsQueryable();

            // Filter on search
            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                measurements = measurements.Where(a => a.Values.Any(kvp => kvp.Key.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
            }

            // Order on OrderBy
            if (!string.IsNullOrWhiteSpace(resourceParameters.OrderBy))
            {
                var orderBy = resourceParameters.OrderBy.Trim();
                Expression<Func<IDeviceMeasurement, object>> orderByLambda;
                try
                {
                    orderByLambda = PropertyHelpers.GetPropertySelector<IDeviceMeasurement>(orderBy);
                }
                catch (ArgumentException e)
                {
                    throw new BadInputException(e.Message, $"The property {orderBy} does not exist");
                }

                measurements = resourceParameters.SortDirection == "desc" ? measurements.OrderByDescending(orderByLambda) : measurements.OrderBy(orderByLambda);
            }
             
            return Task.FromResult(Paginator<IDeviceMeasurement>.Create(measurements, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public Task<IDeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (measurementId == Guid.Empty) throw new ArgumentNullException(nameof(measurementId));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();
            var measurement = measurementsList.FirstOrDefault(mes => mes.Id.ToString() == measurementId.ToString());

            return Task.FromResult(measurement);
        }

        public Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (measurement == null) throw new ArgumentNullException(nameof(measurement));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();

            var existingMeasurement = measurementsList.FirstOrDefault(mes => mes.Id.ToString() == measurement.Id.ToString());
            if (existingMeasurement != null)
            {
                throw new ArgumentException($"Measurement already exists with id: {measurement.Id.ToString()}");
            }

            measurement.DeviceId = deviceId;
            measurement.Device = device;
            if (measurement.Id == Guid.Empty)
            {
                measurement.Id = Guid.NewGuid();
            }

            measurementsList.Add(measurement);
            device.Measurements = measurementsList;
            return Task.FromResult(measurement);
        }

        public Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

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
            _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

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

        public Task<bool> MeasurementExists(string deviceId, Guid measurementId)
        {
            if (string.IsNullOrEmpty(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (measurementId == Guid.Empty) throw new ArgumentNullException(nameof(measurementId));

            var device = _devices.FirstOrDefault(dev => dev.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementsList = device.Measurements.ToList();
            var measurement = measurementsList.FirstOrDefault(mes => mes.Id.ToString() == measurementId.ToString());

            return Task.FromResult(measurement != null);
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }
    }
}
