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

            List<Measurement> measurements = new List<Measurement>()
            { 
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                }
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(20f, result);
        }

        [Test]
        public void CalculatingAverageWithTwo_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(21f, result);
        }

        [Test]
        public void CalculatingAverageWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 21f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(21f, result);
        }

        [Test]
        public void CalculatingAverageWithThreeAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 21f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(21f, result);
        }

        [Test]
        public void CalculatingAverageWithFourAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 26f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(23f, result);
        }

        [Test]
        public void CalculatingAverageWithFiveAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 26f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 23f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(23f, result);
        }

        [Test]
        public void CalculatingAverageWithSixAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            List<Measurement> measurements = new List<Measurement>()
            {
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 26f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = false,
                    SetPoint = 22f,
                    Temperature = 23f,
                    TimeStamp = DateTime.UtcNow
                },
                new Measurement
                {
                    On = true,
                    SetPoint = 22f,
                    Temperature = 23f,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverageTemp(measurements);

            Assert.AreEqual(23f, result);
        }
    }
}
