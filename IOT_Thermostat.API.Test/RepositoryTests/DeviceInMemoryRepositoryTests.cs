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
        }

        [Test]
        public void CheckDeviceInMemoryRepositoryCanBeCreated_ReturnsTrue()
        {
            Assert.IsNotNull(repo);
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

            Assert.ThrowsAsync(typeof(ArgumentException), async delegate { await repo.AddDevice(device); await repo.Save(); });
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

            var updatedDevice = await repo.UpdateDevice(device);
            await repo.Save();

            Assert.AreNotEqual(unChangedName, updatedDevice.DeviceName);
        }

        [Test]
        public async Task CheckMeasurementCanBeAdded_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement);
        }

    }
}
