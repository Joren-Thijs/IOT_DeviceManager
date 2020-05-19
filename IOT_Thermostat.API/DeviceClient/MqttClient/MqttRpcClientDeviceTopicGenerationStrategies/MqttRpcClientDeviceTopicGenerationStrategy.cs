using MQTTnet.Extensions.Rpc.Options;
using MQTTnet.Extensions.Rpc.Options.TopicGeneration;

namespace IOT_Thermostat.API.DeviceClient.MqttClient.MqttRpcClientDeviceTopicGenerationStrategies
{
    public class MqttRpcClientDeviceTopicGenerationStrategy : IMqttRpcClientTopicGenerationStrategy
    {
        public MqttRpcTopicPair CreateRpcTopics(TopicGenerationContext context)
        {
            var requestTopic = context.MethodName.Replace(".", "/");
            var responseTopic = requestTopic + "/response";

            return new MqttRpcTopicPair
            {
                RequestTopic = requestTopic,
                ResponseTopic = responseTopic
            };
        }
    }
}
