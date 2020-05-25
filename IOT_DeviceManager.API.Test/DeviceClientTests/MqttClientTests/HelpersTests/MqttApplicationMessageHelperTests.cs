using System;
using System.Collections.Generic;
using System.Text;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using MQTTnet;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Extensions;

namespace IOT_DeviceManager.API.Test.DeviceClientTests.MqttClientTests.HelpersTests
{
    [TestFixture]
    class MqttApplicationMessageHelperTests
    {
        [Test]
        public void CheckDeviceTypeCanBeDeconstructedFromMessage_ReturnsTrue()
        {
            var deviceType = "thermostat";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;

            var deconstructedDeviceType = MqttApplicationMessageHelper.GetDeviceTypeFromMessage(message);
            Assert.AreEqual(deviceType, deconstructedDeviceType);
        }

        [Test]
        public void CheckDeviceIdCanBeDeconstructedFromMessage_ReturnsTrue()
        {
            var deviceType = "thermostat";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;

            var deconstructedDeviceId = MqttApplicationMessageHelper.GetDeviceIdFromMessage(message);
            Assert.AreEqual(deviceId, deconstructedDeviceId);
        }

        [Test]
        public void CheckDeviceMeasurementCanBeDeconstructedFromMessage_ReturnsTrue()
        {
            var deviceType = "thermostat";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var measurement = new DeviceMeasurement
            {
                Status = new DeviceStatus
                {
                    OnStatus = false,
                    Settings = new Dictionary<string, object>
                    {
                        {"setpoint", 22}
                    }
                },
                DeviceId = "1",
                Values = new Dictionary<string, object>
                {
                    {"temperature", 20}
                },
                TimeStamp = DateTime.Now
            };

            var measurementString = JsonConvert.SerializeObject(measurement);
            var messagePayload = Encoding.ASCII.GetBytes(measurementString);

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;
            message.Payload = messagePayload;

            var deconstructedMeasurement = MqttApplicationMessageHelper.GetDeviceMeasurementFromMessage(message);
            deconstructedMeasurement.Should().BeEquivalentTo(measurement);
        }

        [Test]
        public void CheckDeviceMeasurementDeconstructedFromMessageHasCorrectClassDevice_ReturnsTrue()
        {
            var deviceType = "device";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var measurement = new DeviceMeasurement
            {
                Status = new DeviceStatus
                {
                    OnStatus = false,
                },
                DeviceId = "1",
                TimeStamp = DateTime.Now
            };

            var measurementString = JsonConvert.SerializeObject(measurement);
            var messagePayload = Encoding.ASCII.GetBytes(measurementString);

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;
            message.Payload = messagePayload;

            var deconstructedMeasurement = MqttApplicationMessageHelper.GetDeviceMeasurementFromMessage(message);
            deconstructedMeasurement.Should().BeOfType<DeviceMeasurement>();
        }

        [Test]
        public void CheckDeviceMeasurementDeconstructedFromMessageHasCorrectClassThermostat_ReturnsTrue()
        {
            var deviceType = "thermostat";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var measurement = new DeviceMeasurement
            {
                Status = new DeviceStatus
                {
                    OnStatus = false,
                    Settings = new Dictionary<string, object>
                    {
                        {"setpoint", 22}
                    }
                },
                DeviceId = "1",
                Values = new Dictionary<string, object>
                {
                    {"temperature", 20}
                },
                TimeStamp = DateTime.Now
            };

            var measurementString = JsonConvert.SerializeObject(measurement);
            var messagePayload = Encoding.ASCII.GetBytes(measurementString);

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;
            message.Payload = messagePayload;

            var deconstructedMeasurement = MqttApplicationMessageHelper.GetDeviceMeasurementFromMessage(message);
            deconstructedMeasurement.Should().BeOfType<DeviceMeasurement>();
        }

        [Test]
        public void CheckDeviceMeasurementDeconstructedFromMessageHasCorrectClassDeviceByDefault_ReturnsTrue()
        {
            var deviceType = "";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var measurement = new DeviceMeasurement
            {
                Status = new DeviceStatus(),
                DeviceId = "1",
                TimeStamp = DateTime.Now
            };

            var measurementString = JsonConvert.SerializeObject(measurement);
            var messagePayload = Encoding.ASCII.GetBytes(measurementString);

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;
            message.Payload = messagePayload;

            var deconstructedMeasurement = MqttApplicationMessageHelper.GetDeviceMeasurementFromMessage(message);
            deconstructedMeasurement.Should().BeOfType<DeviceMeasurement>();
        }

        [Test]
        public void CheckDeviceStatusRpcTopicCanBeConstructedFromDevice_ReturnsTrue()
        {
            var device = new Device
            {
                Id = "1"
            };

            var topic = MqttApplicationMessageHelper.GetSetDeviceStatusRpcTopicFromDevice(device);
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

            var topic = MqttApplicationMessageHelper.GetSetDeviceStatusRpcTopicFromDevice(device);
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
            var newStatus = MqttApplicationMessageHelper.GetDeviceStatusFromRcpAnswer(device, statusStringBytes);
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
            var newDeviceStatus = MqttApplicationMessageHelper.GetDeviceStatusFromRcpAnswer(device, deviceStatusStringBytes);
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
            var newThermostatDeviceStatus = MqttApplicationMessageHelper.GetDeviceStatusFromRcpAnswer(thermostatDevice, deviceStatusStringBytes);
            newThermostatDeviceStatus.Should().BeOfType<DeviceStatus>();
        }

        [Test]
        public void CheckDeviceRequestIdRpcTopicCanBeConstructedFromMessage_ReturnsTrue()
        {
            var messageTopic = "device/deviceId/request/id";
            var message = new MqttApplicationMessage
            {
                Topic = messageTopic
            };

            var topic = MqttApplicationMessageHelper.GetDeviceIdRequestResponseTopicFromMessage(message);
            var correctTopic = (messageTopic + "/response");
            Assert.AreEqual(topic, correctTopic);
        }
    }
}
