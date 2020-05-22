using NUnit.Framework;
using System.Collections.Generic;
using IOT_Thermostat.API.Entity.ThermostatDevice;

namespace IOT_Thermostat.API.Test
{
    [TestFixture]
    public class ThermostatTests
    {

        [Test]
        public void CheckThermostatCanBeCreated_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            Assert.IsNotNull(thermostat);
        }

        [Test]
        public void CheckThermostatMeasurementsListIsInitialized_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();

            Assert.IsTrue(((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Count == 0);
        }

        [Test]
        public void CheckMeasurementsCanBeAdded_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new ThermostatDeviceMeasurement();
            var measurement2 = new ThermostatDeviceMeasurement();

            ((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Add(measurement2);
            Assert.IsTrue(((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Count == 2);
        }

        [Test]
        public void CheckMeasurementsCanBeRemoved_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new ThermostatDeviceMeasurement();
            var measurement2 = new ThermostatDeviceMeasurement();

            ((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Add(measurement2);
            ((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Remove(measurement2);
            Assert.IsTrue(((List<ThermostatDeviceMeasurement>)thermostat.Measurements).Count == 1);
        }
    }
}
