using IOT_DeviceManager.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;

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

                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                }
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(20, result);
        }

        [Test]
        public void CalculatingAverageWithTwo_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                }
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithPropertyNameWithMissmatchingCase_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();
            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {

                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                }
            };

            var result = calculationService.CalculateAverage(measurements, "Temperature");

            Assert.AreEqual(20, result);
        }

        [Test]
        public void CalculatingAverageWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 21}
                    },
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithThreeAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 21}
                    },
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(21, result);
        }

        [Test]
        public void CalculatingAverageWithFourAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 24}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 26}
                    },
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(23, result);
        }

        [Test]
        public void CalculatingAverageWithFiveAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 24}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 26}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 23}
                    },
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(23, result);
        }

        [Test]
        public void CalculatingAverageWithSixAndDifferingOnStatus_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 20}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 22}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 24}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 26}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 23}
                    },
                    TimeStamp = DateTime.UtcNow
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    Values = new Dictionary<string, object>
                    {
                        {"temperature", 23}
                    },
                    TimeStamp = DateTime.UtcNow
                },
            };

            var result = calculationService.CalculateAverage(measurements, "temperature");

            Assert.AreEqual(23, result);
        }
    }
}
