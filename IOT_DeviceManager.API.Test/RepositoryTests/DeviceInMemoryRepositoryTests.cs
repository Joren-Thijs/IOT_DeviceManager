using IOT_DeviceManager.API.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Helpers.Exceptions;
using IOT_DeviceManager.API.Helpers.Web;


namespace IOT_DeviceManager.API.Test.RepositoryTests
{
    [TestFixture]
    class DeviceInMemoryRepositoryTests
    {
        DeviceInMemoryRepository repo;
        IDevice device;
        IDevice device2;
        IDevice device3;
        private IDeviceMeasurement measurement;
        private IDeviceMeasurement measurement2;
        private IDeviceMeasurement measurement3;

        [SetUp]
        public void Init()
        {
            repo = new DeviceInMemoryRepository();
            device = new Device
            {
                Id = "1",
                DeviceType = "device",
                DeviceName = "Device 1"
            };
            device2 = new Device
            {
                Id = "2",
                DeviceType = "device",
                DeviceName = "Device 2"
            };
            device3 = new Device
            {
                Id = "3",
                DeviceType = "device",
                DeviceName = "Device 3"
            };
            measurement = new DeviceMeasurement
            {
                Id = "1",
                Values = new Dictionary<string, object>
                {
                    {"sensor 1", 21},
                }
            };
            measurement2 = new DeviceMeasurement
            {
                Id = "2",
                Values = new Dictionary<string, object>
                {
                    {"sensor 2", 22},
                }
            };
            measurement3 = new DeviceMeasurement
            {
                Id = "3",
                Values = new Dictionary<string, object>
                {
                    {"sensor 3", 23},
                }
            };
        }

        [TearDown]
        public void CleanUp()
        {
            repo.DeleteMeasurement(measurement);
            repo.DeleteMeasurement(measurement2);
            repo.DeleteMeasurement(measurement3);
            repo.DeleteDevice(device);
            repo.DeleteDevice(device2);
            repo.DeleteDevice(device3);
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
        public async Task CheckDeviceCannotBeAddedTwice_ThrowsArgumentException()
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

            retrievedDevice.Should().BeEquivalentTo(device);
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

            retrievedDevices.First().Should().BeEquivalentTo(device);
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
        public async Task CheckGetDevicesWithResourceParametersNullThrowsException_ThrowsArgumentNullException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            ResourceParameters resourceParameters = null;

            Assert.ThrowsAsync(typeof(ArgumentNullException), async delegate
            {
                await repo.GetDevices(resourceParameters);
            });
        }

        [Test]
        public async Task CheckDevicesCanBeRetrievedWithResourceParametersWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = null,
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            retrievedDevices.First().Should().BeEquivalentTo(device);
            Assert.AreEqual(1, retrievedDevices.Count());
            Assert.AreEqual(1, retrievedDevices.TotalCount);
        }

        [Test]
        public async Task CheckDevicesCanBeRetrievedWithResourceParametersWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = null,
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(2, retrievedDevices.Count());
            Assert.AreEqual(2, retrievedDevices.TotalCount);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithWrongSearchQueryWithOneReturnsEmptyPaginator_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "z",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(0, retrievedDevices.TotalCount);
            Assert.AreEqual(0, retrievedDevices.Count());
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithCorrectSearchQueryWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device 1",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(1, retrievedDevices.TotalCount);
            Assert.AreEqual(1, retrievedDevices.Count);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithCorrectSearchQueryWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device 1",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(1, retrievedDevices.TotalCount);
            Assert.AreEqual(1, retrievedDevices.Count);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithCorrectSearchQueryWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(3, retrievedDevices.Count);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithOrderByWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceName",
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(3, retrievedDevices.Count);
            retrievedDevices.FirstOrDefault().Should().BeEquivalentTo(device);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithWrongOrderBye_ThrowsBadInputException()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceNamee",
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            Assert.ThrowsAsync(typeof(BadInputException), async delegate
            {
                await repo.GetDevices(resourceParameters);
            });
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithOrderByDescendingWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceName",
                SortDirection = "desc",
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(3, retrievedDevices.Count);
            retrievedDevices.FirstOrDefault().Should().BeEquivalentTo(device3);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithPageSizeOfOneWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceName",
                SortDirection = "asc",
                PageNumber = 1,
                PageSize = 1
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(1, retrievedDevices.Count);
            retrievedDevices.FirstOrDefault().Should().BeEquivalentTo(device);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithPageSizeOfOneWithOrderByDescendingWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceName",
                SortDirection = "desc",
                PageNumber = 1,
                PageSize = 1
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(1, retrievedDevices.Count);
            retrievedDevices.FirstOrDefault().Should().BeEquivalentTo(device3);
        }

        [Test]
        public async Task CheckGetDevicesWithResourceParametersWithPageSizeOfTenWithPageTwoWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "device",
                OrderBy = "DeviceName",
                SortDirection = "desc",
                PageNumber = 2,
                PageSize = 10
            };

            var retrievedDevices = await repo.GetDevices(resourceParameters);

            Assert.AreEqual(3, retrievedDevices.TotalCount);
            Assert.AreEqual(0, retrievedDevices.Count);
        }

        [Test]
        public async Task CheckDevicesRetrievedHasCorrectClass_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            var retrievedDevice1 = await repo.GetDevice(device.Id);
            var retrievedDevice2 = await repo.GetDevice(device2.Id);
            var retrievedDevice3 = await repo.GetDevice(device3.Id);
            retrievedDevice1.Should().BeOfType<Device>();
            retrievedDevice2.Should().BeOfType<Device>();
            retrievedDevice3.Should().BeOfType<Device>();
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
        public async Task CheckMeasurementCannotBeAddedTwice_ThrowsArgumentException()
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
        public async Task CheckMeasurementCannotBeAddedToNonExistingDevice_ThrowsArgumentException()
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
            retrievedMeasurement.Should().BeEquivalentTo(measurement);
        }

        [Test]
        public async Task CheckMeasurementsRetrievedHaveCorrectClass_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.AddDevice(device2);
            await repo.AddDevice(device3);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.AddMeasurement(device2.Id, measurement2);
            await repo.AddMeasurement(device3.Id, measurement3);
            await repo.Save();

            var retrievedMeasurement = await repo.GetMeasurement(device.Id, measurement.Id);
            var retrievedMeasurement2 = await repo.GetMeasurement(device2.Id, measurement2.Id);
            var retrievedMeasurement3 = await repo.GetMeasurement(device3.Id, measurement3.Id);

            retrievedMeasurement.Should().BeOfType<DeviceMeasurement>();
            retrievedMeasurement2.Should().BeOfType<DeviceMeasurement>();
            retrievedMeasurement3.Should().BeOfType<DeviceMeasurement>();
        }

        [Test]
        public async Task CheckMeasurementCannotBeRetrievedFromNonExistingDevice_ThrowsArgumentException()
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
            retrievedMeasurement.Device.Should().BeEquivalentTo(device);
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

            retrievedMeasurements.First().Should().BeEquivalentTo(measurement);
            Assert.AreEqual(1, retrievedMeasurements.Count());

            retrievedMeasurements2.First().Should().BeEquivalentTo(measurement2);
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
        public async Task CheckGetMeasurementsWithResourceParametersNullThrowsException_ThrowsArgumentNullException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            ResourceParameters resourceParameters = null;

            Assert.ThrowsAsync(typeof(ArgumentNullException), async delegate
            {
                await repo.GetMeasurements(device.Id, resourceParameters);
            });
        }

        [Test]
        public async Task CheckMeasurementsCanBeRetrievedWithResourceParametersWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = null,
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            retrievedMeasurements.First().Should().BeEquivalentTo(measurement);
            Assert.AreEqual(1, retrievedMeasurements.Count());
            Assert.AreEqual(1, retrievedMeasurements.TotalCount);
        }

        [Test]
        public async Task CheckMeasurementsCanBeRetrievedWithResourceParametersWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = null,
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(2, retrievedMeasurements.Count());
            Assert.AreEqual(2, retrievedMeasurements.TotalCount);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithWrongSearchQueryWithOneReturnsEmptyPaginator_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "z",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(0, retrievedMeasurements.TotalCount);
            Assert.AreEqual(0, retrievedMeasurements.Count());
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithCorrectSearchQueryWithOne_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor 1",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(1, retrievedMeasurements.TotalCount);
            Assert.AreEqual(1, retrievedMeasurements.Count);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithCorrectSearchQueryWithTwo_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor 1",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(1, retrievedMeasurements.Count);
            Assert.AreEqual(1, retrievedMeasurements.TotalCount);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithCorrectSearchQueryWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = null,
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(3, retrievedMeasurements.Count);
            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithOrderByWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStamp",
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
            Assert.AreEqual(3, retrievedMeasurements.Count);
            retrievedMeasurements.FirstOrDefault().Should().BeEquivalentTo(measurement);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithWrongOrderBye_ThrowsBadInputException()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStampp",
                SortDirection = null,
                PageNumber = 1,
                PageSize = 10
            };

            Assert.ThrowsAsync(typeof(BadInputException), async delegate
            {
                await repo.GetMeasurements(device.Id, resourceParameters);
            });
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithOrderByDescendingWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStamp",
                SortDirection = "desc",
                PageNumber = 1,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(3, retrievedMeasurements.Count);
            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
            retrievedMeasurements.FirstOrDefault().Should().BeEquivalentTo(measurement3);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithPageSizeOfOneWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStamp",
                SortDirection = "asc",
                PageNumber = 1,
                PageSize = 1
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(1, retrievedMeasurements.Count);
            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
            retrievedMeasurements.FirstOrDefault().Should().BeEquivalentTo(measurement);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithPageSizeOfOneWithOrderByDescendingWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStamp",
                SortDirection = "desc",
                PageNumber = 1,
                PageSize = 1
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(1, retrievedMeasurements.Count);
            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
            retrievedMeasurements.FirstOrDefault().Should().BeEquivalentTo(measurement3);
        }

        [Test]
        public async Task CheckGetMeasurementsWithResourceParametersWithPageSizeOfTenWithPageTwoWithThree_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();

            await repo.AddMeasurement(device.Id, measurement);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement2);
            await repo.Save();
            await repo.AddMeasurement(device.Id, measurement3);
            await repo.Save();

            var resourceParameters = new ResourceParameters
            {
                SearchQuery = "sensor",
                OrderBy = "TimeStamp",
                SortDirection = "asc",
                PageNumber = 2,
                PageSize = 10
            };

            var retrievedMeasurements = await repo.GetMeasurements(device.Id, resourceParameters);

            Assert.AreEqual(3, retrievedMeasurements.TotalCount);
            Assert.AreEqual(0, retrievedMeasurements.Count);
        }

        [Test]
        public async Task CheckMeasurementCanBeAddedAndGetsAnId_ReturnsTrueAsync()
        {
            await repo.AddDevice(device);
            await repo.Save();
            await repo.AddMeasurement(device.Id, new DeviceMeasurement());
            await repo.Save();

            var retrievedMeasurements = await repo.GetMeasurements(device.Id);
            var retrievedMeasurement = retrievedMeasurements.FirstOrDefault();
            Assert.IsNotNull(retrievedMeasurement);
            Assert.AreNotEqual(retrievedMeasurement.Id, null);
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
