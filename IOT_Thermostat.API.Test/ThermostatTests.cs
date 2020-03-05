using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Thermostat.API.Test
{
    public class ThermostatTests
    {

        [Test]
        public void CheckThermostatCanBeCreated_ReturnsFalse()
        {
            Thermostat thermostat = new Thermostat();
            Assert.IsNotNull(thermostat);
        }

        [Test]
        public void CheckThermostatMeasurementsListIsInitialized_ReturnsTrue()
        {
            Thermostat thermostat = new Thermostat();

            Assert.IsTrue(((List<Measurement>)thermostat.Measurements).Count == 0);
        }

        [Test]
        public void CheckMeasurementsCanBeAdded_ReturnsTrue()
        {
            Thermostat thermostat = new Thermostat();
            var measurement1 = new Measurement();
            var measurement2 = new Measurement();

            ((List<Measurement>)thermostat.Measurements).Add(measurement1);
            ((List<Measurement>)thermostat.Measurements).Add(measurement2);
            Assert.IsTrue(((List<Measurement>)thermostat.Measurements).Count == 2);
        }

        [Test]
        public void CheckMeasurementsCanBeRemoved_ReturnsTrue()
        {
            Thermostat thermostat = new Thermostat();
            var measurement1 = new Measurement();
            var measurement2 = new Measurement();

            ((List<Measurement>)thermostat.Measurements).Add(measurement1);
            ((List<Measurement>)thermostat.Measurements).Add(measurement2);
            ((List<Measurement>)thermostat.Measurements).Remove(measurement2);
            Assert.IsTrue(((List<Measurement>)thermostat.Measurements).Count == 1);
        }
    }
}
