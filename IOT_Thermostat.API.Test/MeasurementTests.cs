using IOT_Thermostat.API.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace IOT_Thermostat.API.Test
{
    public class MeasurementTests
    {

        [Test]
        public void CheckMeasurementCanBeCreated_ReturnsFalse()
        {
            Measurement measurement = new Measurement();
            Assert.IsNotNull(measurement);
        }


    }
}