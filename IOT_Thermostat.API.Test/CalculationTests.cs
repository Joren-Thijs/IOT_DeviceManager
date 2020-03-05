using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Thermostat.API.Test
{
    class CalculationTests
    {
        [Test]
        public void CheckCalculationServiceCanBeCreated_ReturnsFalse()
        {
            CalculationService calculationService = new CalculationService();
            Assert.IsNotNull(calculationService);
        }

        [Test]
        public void CheckCalculatingAverageWithOne_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            Measurement measurement = new Measurement()
            {
                On = true,
                SetPoint = 22f,
                Temperature = 20f,
                TimeStamp = DateTime.UtcNow
            };

            List<Measurement> measurements = new List<Measurement>();

            measurements.Add(measurement);

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(20f, result);
        }

        [Test]
        public void CheckCalculatingAverageWithTwo_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            Measurement measurement1 = new Measurement()
            {
                On = true,
                SetPoint = 22f,
                Temperature = 20f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement2 = new Measurement()
            {
                On = true,
                SetPoint = 25f,
                Temperature = 22f,
                TimeStamp = DateTime.UtcNow
            };

            List<Measurement> measurements = new List<Measurement>();

            measurements.Add(measurement1);
            measurements.Add(measurement2);

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(21f, result);
        }

        [Test]
        public void CheckCalculatingAverageWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            Measurement measurement1 = new Measurement()
            {
                On = true,
                SetPoint = 22f,
                Temperature = 20f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement2 = new Measurement()
            {
                On = true,
                SetPoint = 25f,
                Temperature = 22f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement3 = new Measurement()
            {
                On = true,
                SetPoint = 25f,
                Temperature = 21f,
                TimeStamp = DateTime.UtcNow
            };

            List<Measurement> measurements = new List<Measurement>();

            measurements.Add(measurement1);
            measurements.Add(measurement2);
            measurements.Add(measurement3);

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(21f, result);
        }
    }
}
