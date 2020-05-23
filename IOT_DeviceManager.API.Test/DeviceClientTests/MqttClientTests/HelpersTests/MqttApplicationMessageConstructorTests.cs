using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.ThermostatDevice;
using IOT_DeviceManager.API.Extensions;
using NUnit.Framework;

namespace IOT_DeviceManager.API.Test.DeviceClientTests.MqttClientTests.HelpersTests
{
    [TestFixture]
    class MqttApplicationMessageConstructorTests
    {
        [Test]
        public void CheckDeviceStatusRpcTopicCanBeConstructedFromDevice_ReturnsTrue()
        {
            var device = new Device
            {
                Id = "1"
            };

            var topic = MqttApplicationMessageConstructor.GetSetDeviceStatusRpcTopic(device);
            var correctTopic = $"device.{device.Id}.cmd.status";
            Assert.AreEqual(topic, correctTopic);
        }

        [Test]
        public void CheckDeviceStatusRpcTopicCanBeConstructedFromThermostatDevice_ReturnsTrue()
        {
            var device = new ThermostatDevice
            {
                Id = "1"
            };

            var topic = MqttApplicationMessageConstructor.GetSetDeviceStatusRpcTopic(device);
            var correctTopic = $"thermostat.{device.Id}.cmd.status";
            Assert.AreEqual(topic, correctTopic);
        }

        [Test]
        public void CheckDeviceStatusCanBeConstructedFromRpcAnswer_ReturnsTrue()
        {
            var device = new Device
            {
                Id = "1"
            };
            var status = new DeviceStatus
            {
                OnStatus = true
            };
            var statusString = status.SerializeJson();
            var statusStringBytes = Encoding.ASCII.GetBytes(statusString);
            var newStatus = MqttApplicationMessageConstructor.GetDeviceStatusFromRcpAnswer(device, statusStringBytes);
            newStatus.Should().BeEquivalentTo(status);
        }
    }
}
