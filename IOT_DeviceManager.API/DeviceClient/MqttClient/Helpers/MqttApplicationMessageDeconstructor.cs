using System.Text;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;
using IOT_DeviceManager.API.Extensions;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageDeconstructor
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
            var deviceType = topic.GetUntilOrEmpty("/");

            var payload = Encoding.UTF8.GetString(message.Payload);
            IDeviceMeasurement measurement = deviceType switch
            {
                "thermostat" => JsonConvert.DeserializeObject<ThermostatDeviceMeasurement>(payload),
                "device" => JsonConvert.DeserializeObject<DeviceMeasurement>(payload),
                _ => JsonConvert.DeserializeObject<DeviceMeasurement>(payload),
            };
            return measurement;
        }
    }
}
