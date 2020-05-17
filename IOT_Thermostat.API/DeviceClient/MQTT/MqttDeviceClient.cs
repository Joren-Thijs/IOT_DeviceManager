using IOT_Thermostat.API.DeviceClient.MQTT.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Extensions.Rpc.Options;
using MQTTnet.Protocol;
using System;
using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.DeviceClient
{
    public class MqttDeviceClient : IDeviceClient
    {
        private readonly IMqttClientOptions Options;

        private readonly IMqttRpcClientOptions rpcOptions;

        private IMqttClient client;

        private MqttRpcClient rpcClient;

        public MqttDeviceClient()
        {
            Options = MqttDeviceClientOptionsLoader.LoadMqttClientOptions();
            rpcOptions = MqttDeviceClientOptionsLoader.LoadMqttRpcClientOptions();
            client = new MqttFactory().CreateMqttClient();
            rpcClient = new MqttRpcClient(client, rpcOptions);
            SetupClient();
        }

        private void SetupClient()
        {
            client.UseApplicationMessageReceivedHandler(OnMessage);
        }

        public virtual void OnMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            System.Console.WriteLine("A message is received");
            System.Console.WriteLine(eventArgs.ApplicationMessage.Topic);
        }

        public async Task StartClientAsync()
        {
            await client.ConnectAsync(Options);
            System.Console.WriteLine("Client is connected");
            await client.SubscribeAsync("+/ms");
            System.Console.WriteLine("Subscribed on a channel");
            if(!client.IsConnected)
            {
                await client.ReconnectAsync();
            }
        }

        public Task StopClientAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task SetDeviceStatus(string deviceName)
        {
            string topic = deviceName + ".cmd.status";
            string payload = "{\"status\":\"true\"}";
            await rpcClient.ExecuteAsync(TimeSpan.FromSeconds(5), topic, payload, MqttQualityOfServiceLevel.AtLeastOnce);
        }
    }
}
