using IOT_DeviceManager.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
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
            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {

                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                }
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(20, result);
        }

        [Test]
        public void CalculatingAverageWithTwo_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 21,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithThreeAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 21,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithFourAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 24,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 26,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(23, result);
        }

        [Test]
        public void CalculatingAverageWithFiveAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 24,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 26,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 23,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(23, result);
        }

        [Test]
        public void CalculatingAverageWithSixAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 20,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 22,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 24,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 26,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    SetPoint = 22,
                    Temperature = 23,
                    TimeStamp = DateTime.UtcNow
                },
                new ThermostatDeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    SetPoint = 22,
                    Temperature = 23,
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(23, result);
        }
    }
}
