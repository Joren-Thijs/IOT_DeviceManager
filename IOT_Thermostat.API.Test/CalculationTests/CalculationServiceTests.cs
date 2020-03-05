using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace IOT_Thermostat.API.Test.CalculationTests
{
    class CalculationServiceTests
    {
        [Test]
        public void CheckCalculationServiceCanBeCreated_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();
            Assert.IsNotNull(calculationService);
        }
    }
}
