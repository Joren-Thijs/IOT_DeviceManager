using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using IOT_DeviceManager.API.Entity.Device;
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
            var device = new Device
            {
                Id = "1",
                DeviceType = "thermostat"
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

        [Test]
        public void CheckDeviceStatusConstructedFromRpcAnswerHasCorrectClass_ReturnsTrue()
        {
            var device = new Device
            {
                Id = "1"
            };
            var deviceStatus = new DeviceStatus
            {
                OnStatus = true
            };

            var deviceStatusString = deviceStatus.SerializeJson();
            var deviceStatusStringBytes = Encoding.ASCII.GetBytes(deviceStatusString);
            var newDeviceStatus = MqttApplicationMessageConstructor.GetDeviceStatusFromRcpAnswer(device, deviceStatusStringBytes);
            newDeviceStatus.Should().BeOfType<DeviceStatus>();

            var thermostatDevice = new Device()
            {
                Id = "2",
                DeviceType = "thermostat"
            };
            var thermostatDevicestatus = new DeviceStatus()
            {
                OnStatus = true
            };

            var thermostatDeviceStatusString = deviceStatus.SerializeJson();
            var thermostatDeviceStatusStringBytes = Encoding.ASCII.GetBytes(deviceStatusString);
            var newThermostatDeviceStatus = MqttApplicationMessageConstructor.GetDeviceStatusFromRcpAnswer(thermostatDevice, deviceStatusStringBytes);
            newThermostatDeviceStatus.Should().BeOfType<DeviceStatus>();
        }
    }
}
