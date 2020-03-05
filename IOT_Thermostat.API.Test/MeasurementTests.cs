using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
