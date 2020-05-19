using System.Text;
using IOT_Thermostat.API.Extensions;
using IOT_Thermostat.API.Models;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_Thermostat.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageDeconstructor
    {
        public static string GetDeviceIdFromMessage(MqttApplicationMessage message)
        {
            var topic = message.Topic;
            var deviceId = topic.GetUntilOrEmpty("/");
            return deviceId;
        }

        public static IDeviceMeasurement GetDeviceMeasurementFromMessage(MqttApplicationMessage message)
        {
            var payload = Encoding.UTF8.GetString(message.Payload);
            ThermostatMeasurement measurement = JsonConvert.DeserializeObject<ThermostatMeasurement>(payload);
            return measurement;
        }
    }
}
