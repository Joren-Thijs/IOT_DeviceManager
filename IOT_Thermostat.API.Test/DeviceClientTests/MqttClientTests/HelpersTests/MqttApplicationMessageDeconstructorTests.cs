using System;
using System.Collections.Generic;
using System.Text;
using IOT_Thermostat.API.DeviceClient.MqttClient.Helpers;
using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Models.Device;
using IOT_Thermostat.API.Models.ThermostatDevice;
using MQTTnet;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;

namespace IOT_Thermostat.API.Test.DeviceClientTests.MqttClientTests.HelpersTests
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
            var messageTopic = deviceType + "/" +deviceId + "/ms";

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

            var measurement = new ThermostatMeasurement
            {
                Status = new DeviceStatus(),
                DeviceId = "1",
                SetPoint = 22f,
                Temperature = 20f,
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
    }
}
