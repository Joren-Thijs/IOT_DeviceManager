using IOT_Thermostat.API.DeviceClient.MQTT.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Extensions.Rpc.Options;
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
            client.UseApplicationMessageReceivedHandler(OnMessage);
            rpcClient = new MqttRpcClient(client, rpcOptions);
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

        public Task SetDeviceStatus()
        {
            throw new System.NotImplementedException();
        }
    }
}
