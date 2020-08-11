using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Test.EntityTests
{
    [TestFixture]
    public class DeviceTests
    {

        [Test]
        public void CheckThermostatCanBeCreated_ReturnsTrue()
        {
            var device = new Device();
            Assert.IsNotNull(device);
        }

        [Test]
        public void CheckThermostatMeasurementsListIsInitialized_ReturnsTrue()
        {
            var device = new Device();

            Assert.IsTrue(device.Measurements.ToList().Count == 0);
        }

        [Test]
        public void CheckMeasurementsCanBeAdded_ReturnsTrue()
        {
            var device = new Device();
            var measurement1 = new DeviceMeasurement();
            var measurement2 = new DeviceMeasurement();

            var measurementsList = device.Measurements.ToList();
            measurementsList.Add(measurement1);
            measurementsList.Add(measurement2);
            device.Measurements = measurementsList;
            Assert.IsTrue(device.Measurements.ToList().Count == 2);
        }

        [Test]
        public void CheckMeasurementsCanBeRemoved_ReturnsTrue()
        {
            var device = new Device();
            var measurement1 = new DeviceMeasurement();
            var measurement2 = new DeviceMeasurement();

            var measurementsList = device.Measurements.ToList();
            measurementsList.Add(measurement1);
            measurementsList.Add(measurement2);
            measurementsList.Remove(measurement2);
            device.Measurements = measurementsList;

            Assert.IsTrue(device.Measurements.ToList().Count == 1);
        }
    }
}
