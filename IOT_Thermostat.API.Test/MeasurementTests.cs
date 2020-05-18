using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System;

namespace IOT_Thermostat.API.Test
{
    [TestFixture]
    class MeasurementTests
    {
        [Test]
        public void CheckMeasurementCanBeCreated_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            Assert.IsNotNull(measurement);
        }

        [Test]
        public void CheckMeasurementOn_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            Assert.IsNotNull(measurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementOnCanBeModified_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            measurement.Status.OnStatus = true;
            Assert.IsTrue(measurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementTimeStamp_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            Assert.IsNotNull(measurement.TimeStamp);
        }

        [Test]
        public void CheckMeasurementTimeStampIsAcurate_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            TimeSpan diff = DateTime.UtcNow - measurement.TimeStamp;
            Assert.IsTrue(diff > TimeSpan.FromSeconds(0));
        }

        [Test]
        public void CheckMeasurementSetPoint_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            Assert.IsNotNull(measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementSetPointCanBeSet_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            measurement.SetPoint = 20f;
            Assert.AreEqual(20f, measurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementTemperature_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            Assert.IsNotNull(measurement.Temperature);
        }

        [Test]
        public void CheckMeasurementTemperatureCanBeSet_ReturnsTrue()
        {
            ThermostatMeasurement measurement = new ThermostatMeasurement();
            measurement.Temperature = 20f;
            Assert.AreEqual(20f, measurement.Temperature);
        }
    }
}
