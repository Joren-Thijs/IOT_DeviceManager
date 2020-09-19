using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Helpers.Exceptions;
using IOT_DeviceManager.API.Helpers.Reflection;
using IOT_DeviceManager.API.Helpers.Web;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IOT_DeviceManager.API.Repositories
{

    public class DeviceMongoRepository : IDeviceRepository
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<IDevice> _deviceCollection;
        private IMongoCollection<IDeviceMeasurement> _measurementCollection;

        public DeviceMongoRepository(IConfiguration configuration)
        {
            _mongoClient = new MongoClient(configuration.GetConnectionString("deviceDb"));
            _database = _mongoClient.GetDatabase(configuration.GetSection("Databases").GetValue<string>("deviceDb"));
            _deviceCollection = _database.GetCollection<IDevice>("Devices");
            _measurementCollection = _database.GetCollection<IDeviceMeasurement>("Measurements");
        }

        public async Task<IDevice> AddDevice(IDevice device)
        {
            _ = device ?? throw new ArgumentNullException(nameof(device));
            try
            {
                await _deviceCollection.InsertOneAsync(device);
            }
            catch (MongoWriteException)
            {
                throw new ArgumentException("This device already exists");
            }
            
            return device;
        }

        public async Task<IDeviceMeasurement> AddMeasurement(string deviceId, IDeviceMeasurement measurement)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

            measurement.DeviceId = deviceId;
            
            try
            {
                await _measurementCollection.InsertOneAsync(measurement);
            }
            catch (MongoWriteException)
            {
                throw new ArgumentException("This measurement already exists");
            }
            return measurement;
        }

        public async Task DeleteDevice(IDevice device)
        {
            _ = device ?? throw new ArgumentNullException(nameof(device));
            var filter = Builders<IDevice>.Filter.Eq("_id", device.Id);
            await _deviceCollection.DeleteOneAsync(filter);
        }

        public async Task DeleteMeasurement(IDeviceMeasurement measurement)
        {
            _ = measurement ?? throw new ArgumentNullException(nameof(measurement));
            var filter = Builders<IDeviceMeasurement>.Filter.Eq("_id", measurement.Id);
            await _measurementCollection.DeleteOneAsync(filter);
        }

        public async Task<bool> DeviceExists(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var count = await _deviceCollection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<IDevice> GetDevice(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            return (await _deviceCollection.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<IEnumerable<IDevice>> GetDevices()
        {
            return (await _deviceCollection.FindAsync(_ => true)).ToList();
        }

        public Task<Paginator<IDevice>> GetDevices(ResourceParameters resourceParameters)
        {
            _ = resourceParameters ?? throw new ArgumentNullException(nameof(resourceParameters));

            var filter = Builders<IDevice>.Filter.Empty;
            IQueryable<IDevice> devices = _deviceCollection.AsQueryable();

            // Filter on search
            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                filter = Builders<IDevice>.Filter.Regex("deviceName", new BsonRegularExpression(searchQuery));

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

                devices = resourceParameters.SortDirection == "desc"
                    ? _deviceCollection.Find(filter).SortByDescending(orderByLambda).ToEnumerable().AsQueryable()
                    : _deviceCollection.Find(filter).SortBy(orderByLambda).ToEnumerable().AsQueryable();
            }

            return Task.FromResult(Paginator<IDevice>.Create(devices, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<IEnumerable<IDevice>> GetDevices(IEnumerable<string> deviceIds)
        {
            _ = deviceIds ?? throw new ArgumentNullException(nameof(deviceIds));

            var devices = (await _deviceCollection.FindAsync(_ => true)).ToEnumerable().Where(dev => deviceIds.Any(id => id == dev.Id)).ToList();

            return devices;
        }

        public async Task<IDeviceStatus> GetDeviceStatus(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var device = (await _deviceCollection.FindAsync(filter)).FirstOrDefault();
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }
            return device.Status;
        }

        public async Task<IDeviceMeasurement> GetMeasurement(string deviceId, Guid measurementId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (measurementId == Guid.Empty) throw new ArgumentNullException(nameof(measurementId));

            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var device = (await _deviceCollection.FindAsync(filter)).FirstOrDefault();

            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementFilter = Builders<IDeviceMeasurement>.Filter.Eq("_id", measurementId);
            var measurement = (await _measurementCollection.FindAsync(measurementFilter)).FirstOrDefault();

            return measurement;
        }

        public async Task<IEnumerable<IDeviceMeasurement>> GetMeasurements(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var device = (await _deviceCollection.FindAsync(filter)).FirstOrDefault();

            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementFilter = Builders<IDeviceMeasurement>.Filter.Eq("DeviceId", deviceId);
            var measurements = (await _measurementCollection.FindAsync(measurementFilter)).ToEnumerable();

            return measurements;
        }

        public async Task<Paginator<IDeviceMeasurement>> GetMeasurements(string deviceId, ResourceParameters resourceParameters)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            _ = resourceParameters ?? throw new ArgumentNullException(nameof(resourceParameters));

            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var device = (await _deviceCollection.FindAsync(filter)).FirstOrDefault();
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementFilter = Builders<IDeviceMeasurement>.Filter.Empty;
            IQueryable<IDeviceMeasurement> measurements = _measurementCollection.AsQueryable();

            // Filter on search
            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                measurementFilter = Builders<IDeviceMeasurement>.Filter.Where(mes => mes.Values.Any(kvp => kvp.Key.Contains(searchQuery)));
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

                measurements = (IMongoQueryable<IDeviceMeasurement>)(resourceParameters.SortDirection == "desc" ? Queryable.OrderByDescending(measurements, orderByLambda) : Queryable.OrderBy(measurements, orderByLambda));
            }

            return await Task.FromResult(Paginator<IDeviceMeasurement>.Create(measurements, resourceParameters.PageNumber, resourceParameters.PageSize));
        }

        public async Task<bool> MeasurementExists(string deviceId, Guid measurementId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            var filter = Builders<IDevice>.Filter.Eq("_id", deviceId);
            var device = (await _deviceCollection.FindAsync(filter)).FirstOrDefault();
            if (device == null)
            {
                throw new ArgumentException($"No device exists with id: {deviceId}");
            }

            var measurementFilter = Builders<IDeviceMeasurement>.Filter.Eq("_id", measurementId);
            var count = await _measurementCollection.CountDocumentsAsync(measurementFilter);
            return count > 0;
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }

        public async Task<IDevice> UpdateDevice(IDevice device)
        {
            _ = device ?? throw new ArgumentNullException(nameof(device));

            var filter = Builders<IDevice>.Filter.Eq("_id", device.Id);
            var result = await _deviceCollection.FindOneAndReplaceAsync(filter, device);
            return result;
        }

        public async Task<IDeviceMeasurement> UpdateMeasurement(IDeviceMeasurement measurement)
        {
            _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

            var measurementFilter = Builders<IDeviceMeasurement>.Filter.Eq("_id", measurement.Id);
            var result = await _measurementCollection.FindOneAndReplaceAsync(measurementFilter, measurement);

            return result;
        }
    }
}