using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Services
{
    public class MQTTCLientService : IDeviceClient
    {
        public IMqttClient Client { get; private set; } = new MqttFactory().CreateMqttClient();
        private readonly IMqttClientOptions clientOptions = new MqttClientOptionsBuilder()
        .WithClientId("api")
        .WithTcpServer("127.0.0.1")
        .Build();

        public MQTTCLientService()
        {
            Console.WriteLine("Constructor called");
        }

        public async Task StartDeviceClientAsync(CancellationToken stoppingToken)
        {
            //await SetupSubscriptionsAsync();
            Client.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine($"{DateTime.Now.ToShortTimeString()} disconnected");
            });
            Client.UseConnectedHandler(async e =>
            {
                Console.WriteLine(e.AuthenticateResult.ResultCode);
                Console.WriteLine($"{DateTime.Now.ToShortTimeString()} connected.");
                await Client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("#/ms").Build());
            });
            Client.UseApplicationMessageReceivedHandler(async e =>
            {
                Console.WriteLine("Meassage received");
                Console.WriteLine(e.ApplicationMessage.Topic);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            });
            await Client.ConnectAsync(clientOptions, stoppingToken);
            while(true) { Thread.Sleep(50); }
        }

        private async Task SetupSubscriptionsAsync()
        {
            await Client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("#/ms").Build());
        }

        private Task Client_OnMessage(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("Meassage received");
            Console.WriteLine(arg.ApplicationMessage.Topic);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(arg.ApplicationMessage.Payload));
            return Task.CompletedTask;
        }

        private async Task PublishMessageAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithAtLeastOnceQoS()
                .WithRetainFlag()
                .Build();

            await Client.PublishAsync(message, CancellationToken.None);
        }
    }
}
