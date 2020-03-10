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
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement);
        }

        [Test]
        public void CheckMeasurementOn_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement.On);
        }

        [Test]
        public void CheckMeasurementOnCanBeModified_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            measurement.On = true;
            Assert.IsTrue(measurement.On);
        }

        [Test]
        public void CheckMeasurementTimeStamp_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement.TimeStamp);
        }

        [Test]
        public void CheckMeasurementTimeStampIsAcurate_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            TimeSpan diff = DateTime.UtcNow - measurement.TimeStamp;
            Assert.IsTrue(diff > TimeSpan.FromSeconds(0));
        }

        [Test]
        public void CheckMeasurementSetPoint_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementSetPointCanBeSet_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            measurement.SetPoint = 20f;
            Assert.AreEqual(20f, measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementTemperature_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement.Temperature);
        }

        [Test]
        public void CheckMeasurementTemperatureCanBeSet_ReturnsTrue()
        {
            Measurement measurement = new Measurement();
            measurement.Temperature = 20f;
            Assert.AreEqual(20f, measurement.Temperature);
        }
    }
}
