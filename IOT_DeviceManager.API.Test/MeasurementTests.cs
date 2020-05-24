using NUnit.Framework;
using System;
using System.Collections.Generic;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Test
{
    [TestFixture]
    class MeasurementTests
    {
        [Test]
        public void CheckMeasurementCanBeCreated_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement);
        }

        [Test]
        public void CheckMeasurementOn_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementOnCanBeModified_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            deviceMeasurement.Status.OnStatus = true;
            Assert.IsTrue(deviceMeasurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementTimeStamp_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.TimeStamp);
        }

        [Test]
        public void CheckMeasurementTimeStampIsAcurate_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            TimeSpan diff = DateTime.UtcNow - deviceMeasurement.TimeStamp;
            Assert.IsTrue(diff > TimeSpan.FromSeconds(0));
        }

        [Test]
        public void CheckMeasurementValues_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.Values);
        }

        [Test]
        public void CheckMeasurementValuesCanBeSet_ReturnsTrue()
        {
            DeviceMeasurement deviceMeasurement = new DeviceMeasurement();
            deviceMeasurement.Values = new Dictionary<string, object>
            {
                {"setpoint", 20}
            };
            Assert.AreEqual(20, deviceMeasurement.Values["setpoint"]);
        }
    }
}
