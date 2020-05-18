using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System;

namespace IOT_Thermostat.API.Test
{
    class MeasurementTests
    {
        [Test]
        public void CheckMeasurementCanBeCreated_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            Assert.IsNotNull(measurement);
        }

        [Test]
        public void CheckMeasurementOn_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            Assert.IsNotNull(measurement.On);
        }

        [Test]
        public void CheckMeasurementOnCanBeModified_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            measurement.On = true;
            Assert.IsTrue(measurement.On);
        }

        [Test]
        public void CheckMeasurementTimeStamp_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            Assert.IsNotNull(measurement.TimeStamp);
        }

        [Test]
        public void CheckMeasurementTimeStampIsAcurate_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            TimeSpan diff = DateTime.UtcNow - measurement.TimeStamp;
            Assert.IsTrue(diff > TimeSpan.FromSeconds(0));
        }

        [Test]
        public void CheckMeasurementSetPoint_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            Assert.IsNotNull(measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementSetPointCanBeSet_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            measurement.SetPoint = 20f;
            Assert.AreEqual(20f, measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementTemperature_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            Assert.IsNotNull(measurement.Temperature);
        }

        [Test]
        public void CheckMeasurementTemperatureCanBeSet_ReturnsTrue()
        {
            DeviceMeasurement measurement = new DeviceMeasurement();
            measurement.Temperature = 20f;
            Assert.AreEqual(20f, measurement.Temperature);
        }
    }
}
