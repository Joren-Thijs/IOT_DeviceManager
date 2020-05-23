using IOT_DeviceManager.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.ThermostatDevice;

namespace IOT_DeviceManager.API.Test.CalculationTests
{
    [TestFixture]
    class CalculateAverageTests
    {

        [Test]
        public void CalculatingAverageWithOne_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();
            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {

                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 26f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
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

            List<ThermostatDeviceMeasurement> measurements = new List<ThermostatDeviceMeasurement>()
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 20f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 22f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22f,
                    Temperature = 24f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 26f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22f,
                    Temperature = 23f,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
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
