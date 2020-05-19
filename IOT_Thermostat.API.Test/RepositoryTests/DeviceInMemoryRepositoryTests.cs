using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Test.RepositoryTests
{
    [TestFixture]
    class DeviceInMemoryRepositoryTests
    {
        DeviceInMemoryRepository repo;
        IDevice device;
        IDevice device2;
        private IDeviceMeasurement measurement;
        private IDeviceMeasurement measurement2;

        [SetUp]
        public void Init()
        {
            repo = new DeviceInMemoryRepository();
            device = new ThermostatDevice
            {
                Id = "1"
            };
            device2 = new ThermostatDevice
            {
                Id = "2"
            };
            measurement = new ThermostatMeasurement
            {
                Id = "1"
            };
            measurement2 = new ThermostatMeasurement
            {
                Id = "2"
            };
        }

        [Test]
        public void CheckDeviceInMemoryRepositoryCanBeCreated_ReturnsTrue()
        {
            Assert.IsNotNull(repo);
        }

        [Test]
        public async Task CheckChangesCanBeSaved_ReturnsTrue()
        {
            await repo.Save();
        }

        [Test]
        public async Task CheckDeviceCanBeAdded_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
        }

        [Test]
        public async Task CheckDeviceCannotBeAddedTwice_ReturnsArgumentException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            Assert.ThrowsAsync(typeof(ArgumentException), async delegate
            {
                await repo.AddDevice(device); await repo.Save();
            });
        }

        [Test]
        public async Task CheckDeviceCanBeRetrieved_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            var retrievedDevice = await repo.GetDevice(device.Id);
            Assert.AreEqual(device, retrievedDevice);
        }

        [Test]
        public async Task CheckDeviceCanBeDeleted_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            await repo.DeleteDevice(device);
            await repo.Save();
            var retrievedDevice = await repo.GetDevice(device.Id);
            Assert.IsNull(retrievedDevice);
        }

        [Test]
        public async Task CheckDevicesCanBeRetrievedWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var retrievedDevices = await repo.GetDevices();

            Assert.AreEqual(device, retrievedDevices.First());
            Assert.AreEqual(1, retrievedDevices.Count());
        }

        [Test]
        public async Task CheckDevicesCanBeRetrievedWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.Save();

            var retrievedDevices = await repo.GetDevices();

            Assert.AreEqual(2, retrievedDevices.Count());
        }

        [Test]
        public async Task CheckDeviceCanBeUpdated_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var unChangedName = device.DeviceName;
            device.DeviceName = "newName";

            await repo.UpdateDevice(device);
            await repo.Save();

            var updatedDevice = await repo.GetDevice(device.Id);
            await repo.Save();

            Assert.AreNotEqual(unChangedName, updatedDevice.DeviceName);
        }

        [Test]
        public async Task CheckDeviceExists_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            var result = await repo.DeviceExists(device.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckDeviceDoesntExists_ReturnsFalseAsync()
        {
            var result = await repo.DeviceExists(device.Id);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CheckMeasurementCanBeAdded_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
        }

        [Test]
        public async Task CheckMeasurementCannotBeAddedTwice_ReturnsArgumentException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            Assert.ThrowsAsync(typeof(ArgumentException), async delegate
            {
                await repo.AddMeasurement(device.Id, measurement); await repo.Save();
            });
        }

        [Test]
        public async Task CheckMeasurementCannotBeAddedToNonExistingDevice_ReturnsArgumentException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            Assert.ThrowsAsync(typeof(ArgumentException), async delegate
            {
                await repo.AddMeasurement(device2.Id, measurement); await repo.Save();
            });
        }

        [Test]
        public async Task CheckMeasurementCanBeRetrieved_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var retrievedMeasurement = await repo.GetMeasurement(device.Id, measurement.Id);
            Assert.AreEqual(measurement, retrievedMeasurement);
        }

        [Test]
        public async Task CheckMeasurementCannotBeRetrievedFromNonExistingDevice_ReturnsArgumentException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            Assert.ThrowsAsync(typeof(ArgumentException), async delegate
            {
                await repo.GetMeasurement(device2.Id, measurement.Id);
                await repo.Save();
            });
        }

        [Test]
        public async Task CheckMeasurementCanBeRetrievedAndHasCorrectDeviceId_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var retrievedMeasurement = await repo.GetMeasurement(device.Id, measurement.Id);
            Assert.AreEqual(device.Id, retrievedMeasurement.DeviceId);
            Assert.AreEqual(device, retrievedMeasurement.Device);
        }

        [Test]
        public async Task CheckMeasurementCanBeDeleted_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            await repo.DeleteMeasurement(measurement);
            await repo.Save();
            var retrievedMeasurement = await repo.GetMeasurement(device.Id, measurement.Id);
            Assert.IsNull(retrievedMeasurement);
        }

        [Test]
        public async Task CheckMeasurementsCanBeRetrievedWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device2.Id, measurement2);
            await repo.Save();

            var retrievedMeasurements = await repo.GetMeasurements(device.Id);
            var retrievedMeasurements2 = await repo.GetMeasurements(device2.Id);

            Assert.AreEqual(measurement, retrievedMeasurements.First());
            Assert.AreEqual(1, retrievedMeasurements.Count());
            Assert.AreEqual(measurement2, retrievedMeasurements2.First());
            Assert.AreEqual(1, retrievedMeasurements2.Count());
        }

        [Test]
        public async Task CheckMeasurementsCanBeRetrievedWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();

            var retrievedMeasurements = await repo.GetMeasurements(device.Id);

            Assert.AreEqual(2, retrievedMeasurements.Count());
        }

        [Test]
        public async Task CheckMeasurementCanBeUpdated_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var unChangedStatus = measurement.Status;
            measurement.Status = new DeviceStatus(true);

            await repo.UpdateMeasurement(measurement);
            await repo.Save();

            var updatedMeasurement = await repo.GetMeasurement(device.Id, measurement.Id);
            await repo.Save();

            Assert.AreNotEqual(unChangedStatus.OnStatus, updatedMeasurement.Status.OnStatus);
        }

        [Test]
        public async Task CheckMeasurementExists_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var result = await repo.MeasurementExists(device.Id, measurement.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckMeasurementDoesntExists_ReturnsFalseAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var result = await repo.MeasurementExists(device.Id, measurement.Id);
            Assert.IsFalse(result);
        }

    }
}
