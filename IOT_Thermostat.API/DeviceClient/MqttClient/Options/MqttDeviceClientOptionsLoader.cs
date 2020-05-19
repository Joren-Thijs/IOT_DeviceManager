using System;
using IOT_Thermostat.API.DeviceClient.MqttClient.MqttRpcClientDeviceTopicGenerationStrategies;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.Rpc.Options;
using MQTTnet.Extensions.Rpc.Options.TopicGeneration;

namespace IOT_Thermostat.API.DeviceClient.MqttClient.Options
{
    public class MqttDeviceClientOptionsLoader
    {
        public static IMqttClientOptions LoadMqttClientOptions()
        {
            string clientId = Environment.GetEnvironmentVariable("MQTT_CLIENT_ID");
            string clientUsername = Environment.GetEnvironmentVariable("MQTT_CLIENT_USERNAME");
            string clientPassword = Environment.GetEnvironmentVariable("MQTT_CLIENT_PASSWORD");

            string brokerServer = Environment.GetEnvironmentVariable("MQTT_BROKER_SERVER");
            int brokerPort = Convert.ToInt32(Environment.GetEnvironmentVariable("MQTT_BROKER_PORT"));

            return new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithCredentials(clientUsername, clientPassword)
                .WithTcpServer(brokerServer, brokerPort)
                .Build();
        }

        public static IMqttRpcClientOptions LoadMqttRpcClientOptions()
        {
            IMqttRpcClientTopicGenerationStrategy strategy = new MqttRpcClientDeviceTopicGenerationStrategy();

            return new MqttRpcClientOptionsBuilder()
                .WithTopicGenerationStrategy(strategy)
                .Build();
        }
    }
}
