using NUnit.Framework;
using System;
using IOT_Thermostat.API.Entity.ThermostatDevice;

namespace IOT_Thermostat.API.Test
{
    [TestFixture]
    class MeasurementTests
    {
        [Test]
        public void CheckMeasurementCanBeCreated_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement);
        }

        [Test]
        public void CheckMeasurementOn_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementOnCanBeModified_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            deviceMeasurement.Status.OnStatus = true;
            Assert.IsTrue(deviceMeasurement.Status.OnStatus);
        }

        [Test]
        public void CheckMeasurementTimeStamp_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.TimeStamp);
        }

        [Test]
        public void CheckMeasurementTimeStampIsAcurate_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            TimeSpan diff = DateTime.UtcNow - deviceMeasurement.TimeStamp;
            Assert.IsTrue(diff > TimeSpan.FromSeconds(0));
        }

        [Test]
        public void CheckMeasurementSetPoint_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementSetPointCanBeSet_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            deviceMeasurement.SetPoint = 20f;
            Assert.AreEqual(20f, deviceMeasurement.SetPoint);
        }

        [Test]
        public void CheckMeasurementTemperature_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            Assert.IsNotNull(deviceMeasurement.Temperature);
        }

        [Test]
        public void CheckMeasurementTemperatureCanBeSet_ReturnsTrue()
        {
            ThermostatDeviceMeasurement deviceMeasurement = new ThermostatDeviceMeasurement();
            deviceMeasurement.Temperature = 20f;
            Assert.AreEqual(20f, deviceMeasurement.Temperature);
        }
    }
}
