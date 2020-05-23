using IOT_DeviceManager.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;

namespace IOT_DeviceManager.API.Test.CalculationTests
{
    [TestFixture]
    class CalculateOnTimeTests
    {
        [Test]
        public void CalculatingOnTimeWithOne_ReturnsBadArgumentException()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = DateTime.UtcNow
                },
            };

            Assert.Throws(typeof(ArgumentException), delegate { calculationService.CalculateTotalOnTime(measurements); });
        }

        [Test]
        public void CalculatingOnTimeWithTwo_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(1, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(2, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithFour_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 03:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(3, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithFive_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 03:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 06:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(6, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithThree_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(1, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithFour_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(1, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithFive_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 05:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(2, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithSix_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 05:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 06:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(3, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithSeven_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 05:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 06:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 07:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(3, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithEight_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 05:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 06:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 07:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 08:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(3, onTime.Hours);
        }

        [Test]
        public void CalculatingOnTimeWithOnOffWithNine_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();

            IEnumerable<IDeviceMeasurement> measurements = new List<IDeviceMeasurement>
            {
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 00:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 01:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 02:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 04:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 05:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 06:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(),
                    TimeStamp = Convert.ToDateTime("2020-01-01 07:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 08:00:00")
                },
                new DeviceMeasurement
                {
                    Status = new DeviceStatus(true),
                    TimeStamp = Convert.ToDateTime("2020-01-01 09:00:00")
                },
            };

            var onTime = calculationService.CalculateTotalOnTime(measurements);

            Assert.AreEqual(4, onTime.Hours);
        }
    }
}
