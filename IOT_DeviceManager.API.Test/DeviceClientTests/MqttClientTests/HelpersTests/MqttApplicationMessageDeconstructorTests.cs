using System;
using System.Collections.Generic;
using System.Text;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using MQTTnet;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Test.DeviceClientTests.MqttClientTests.HelpersTests
{
    [TestFixture]
    class MqttApplicationMessageDeconstructorTests
    {
        [Test]
        public void CheckDeviceTypeCanBeDeconstructedFromMessage_ReturnsTrue()
        {
            var deviceType = "thermostat";
            var deviceId = "ABCD";
            var messageTopic = deviceType + "/" + deviceId + "/ms";

            var message = new MqttApplicationMessage();
            message.Topic = messageTopic;

            var deconstructedDeviceType = MqttApplicationMessageDeconstructor.GetDeviceTypeFromMessage(message);
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

            var deconstructedDeviceId = MqttApplicationMessageDeconstructor.GetDeviceIdFromMessage(message);
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

            var deconstructedMeasurement = MqttApplicationMessageDeconstructor.GetDeviceMeasurementFromMessage(message);
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

            var deconstructedMeasurement = MqttApplicationMessageDeconstructor.GetDeviceMeasurementFromMessage(message);
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

            var deconstructedMeasurement = MqttApplicationMessageDeconstructor.GetDeviceMeasurementFromMessage(message);
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

            var deconstructedMeasurement = MqttApplicationMessageDeconstructor.GetDeviceMeasurementFromMessage(message);
            deconstructedMeasurement.Should().BeOfType<DeviceMeasurement>();
        }
    }
}
