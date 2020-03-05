using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Thermostat.API.Test.CalculationTests
{
    [TestFixture]
    class CalculateOnTimeTests
    {
        [Test]
        public void CalculatingOnTimeWithOne_ReturnsBadArgumentException()
        {
            CalculationService calculationService = new CalculationService();

            Measurement measurement = new Measurement()
            {
                On = true,
                SetPoint = 22f,
                Temperature = 20f,
                TimeStamp = DateTime.UtcNow
            };

            List<Measurement> measurements = new List<Measurement>
            {
                measurement
            };

            Assert.Throws(typeof(ArgumentException), delegate { calculationService.CalculateTotalOnTime(measurements); });

        }
    }
}
