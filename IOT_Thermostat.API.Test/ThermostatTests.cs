using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace IOT_Thermostat.API.Test
{
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

            Assert.IsTrue(((List<DeviceMeasurement>)thermostat.Measurements).Count == 0);
        }

        [Test]
        public void CheckMeasurementsCanBeAdded_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new DeviceMeasurement();
            var measurement2 = new DeviceMeasurement();

            ((List<DeviceMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<DeviceMeasurement>)thermostat.Measurements).Add(measurement2);
            Assert.IsTrue(((List<DeviceMeasurement>)thermostat.Measurements).Count == 2);
        }

        [Test]
        public void CheckMeasurementsCanBeRemoved_ReturnsTrue()
        {
            ThermostatDevice thermostat = new ThermostatDevice();
            var measurement1 = new DeviceMeasurement();
            var measurement2 = new DeviceMeasurement();

            ((List<DeviceMeasurement>)thermostat.Measurements).Add(measurement1);
            ((List<DeviceMeasurement>)thermostat.Measurements).Add(measurement2);
            ((List<DeviceMeasurement>)thermostat.Measurements).Remove(measurement2);
            Assert.IsTrue(((List<DeviceMeasurement>)thermostat.Measurements).Count == 1);
        }
    }
}
