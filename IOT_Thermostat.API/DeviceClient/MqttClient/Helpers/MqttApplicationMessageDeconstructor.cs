using System.Text;
using IOT_Thermostat.API.Extensions;
using IOT_Thermostat.API.Models;
using IOT_Thermostat.API.Models.ThermostatDevice;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_Thermostat.API.DeviceClient.MqttClient.Helpers
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
                "thermostat" => JsonConvert.DeserializeObject<ThermostatMeasurement>(payload),
                _ => null,
            };
            return measurement;
        }
    }
}
