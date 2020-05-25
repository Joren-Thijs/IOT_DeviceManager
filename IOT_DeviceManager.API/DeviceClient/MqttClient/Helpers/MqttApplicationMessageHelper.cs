using System;
using System.Text;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Requests;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Requests.DeviceId;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Extensions;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageHelper
    {
        public static string GetDeviceTypeFromMessage(MqttApplicationMessage message)
        {
            var topic = message.Topic;
            var deviceType = topic.GetUntilOrEmpty("/");

            return deviceType;
        }

        public static string GetDeviceIdFromMessage(MqttApplicationMessage message)
        {
            var topic = message.Topic;
            var deviceType = GetDeviceTypeFromMessage(message);
            var deviceId = topic.Substring(deviceType.Length + 1).GetUntilOrEmpty("/");

            return deviceId;
        }

        public static IDeviceMeasurement GetDeviceMeasurementFromMessage(MqttApplicationMessage message)
        {
            var topic = message.Topic;
            var payload = Encoding.UTF8.GetString(message.Payload);
            IDeviceMeasurement measurement = JsonConvert.DeserializeObject<DeviceMeasurement>(payload);
            
            return measurement;
        }

        public static DeviceIdRequestResponseDto GetDeviceIdRequestResponseFromMessage(MqttApplicationMessage message)
        {
            var topic = message.Topic;
            var payload = Encoding.UTF8.GetString(message.Payload);
            DeviceIdRequestResponseDto responseDto = JsonConvert.DeserializeObject<DeviceIdRequestResponseDto>(payload);
            responseDto.DeviceId = Guid.NewGuid().ToString();
            return responseDto;
        }

        public static string GetSetDeviceStatusRpcTopicFromDevice(IDevice device)
        {
            var topic = device.DeviceType + "." + device.Id + ".cmd.status";
            return topic;
        }

        public static IDeviceStatus GetDeviceStatusFromRcpAnswer(IDevice device, byte[] rcpAnswer)
        {
            var payload = Encoding.UTF8.GetString(rcpAnswer);
            IDeviceStatus status = JsonConvert.DeserializeObject<DeviceStatus>(payload);

            return status;
        }

        public static string GetDeviceIdRequestResponseTopicFromMessage(MqttApplicationMessage message)
        {
            return message.Topic + "/response";
        }
    }
}
