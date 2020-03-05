using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace IOT_Thermostat.API.Test.CalculationTests
{
    class CalculateAverageTests
    {

        [Test]
        public void CalculatingAverageWithOne_ReturnsTrue()
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
        public void CalculatingAverageWithTwo_ReturnsTrue()
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
        public void CalculatingAverageWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            Measurement measurement1 = new Measurement()
            {
                On = false,
                SetPoint = 22f,
                Temperature = 20f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement2 = new Measurement()
            {
                On = false,
                SetPoint = 25f,
                Temperature = 22f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement3 = new Measurement()
            {
                On = false,
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

        [Test]
        public void CalculatingAverageWithThreeAndDifferingOnStatus_ReturnsTrue()
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
                On = false,
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

        [Test]
        public void CalculatingAverageWithFourAndDifferingOnStatus_ReturnsTrue()
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
                On = false,
                SetPoint = 25f,
                Temperature = 22f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement3 = new Measurement()
            {
                On = true,
                SetPoint = 30f,
                Temperature = 24f,
                TimeStamp = DateTime.UtcNow
            };

            Measurement measurement4 = new Measurement()
            {
                On = false,
                SetPoint = 15f,
                Temperature = 26f,
                TimeStamp = DateTime.UtcNow
            };

            List<Measurement> measurements = new List<Measurement>();

            measurements.Add(measurement1);
            measurements.Add(measurement2);
            measurements.Add(measurement3);
            measurements.Add(measurement4);

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(23f, result);
        }
    }
}
