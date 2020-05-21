using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System.Collections.Generic;
using IOT_Thermostat.API.Models.ThermostatDevice;

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

            Assert.IsTrue(((List<ThermostatMeasurement>)thermostat.Measurements).Count == 0);
        }

        [Test]
        public void CheckMeasurementsCanBeAdded_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new ThermostatMeasurement();
            var measurement2 = new ThermostatMeasurement();

            ((List<ThermostatMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<ThermostatMeasurement>)thermostat.Measurements).Add(measurement2);
            Assert.IsTrue(((List<ThermostatMeasurement>)thermostat.Measurements).Count == 2);
        }

        [Test]
        public void CheckMeasurementsCanBeRemoved_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new ThermostatMeasurement();
            var measurement2 = new ThermostatMeasurement();

            ((List<ThermostatMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<ThermostatMeasurement>)thermostat.Measurements).Add(measurement2);
            ((List<ThermostatMeasurement>)thermostat.Measurements).Remove(measurement2);
            Assert.IsTrue(((List<ThermostatMeasurement>)thermostat.Measurements).Count == 1);
        }
    }
}
