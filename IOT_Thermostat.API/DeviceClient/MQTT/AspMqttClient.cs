﻿using IOT_Thermostat.API.DeviceClient.MQTT.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.DeviceClient
{
    public class AspMqttClient : IDeviceClient
    {
        private readonly IMqttClientOptions Options;

        private IMqttClient client;

        public AspMqttClient()
        {
            Options = MqttDeviceClientOptionsLoader.LoadMqttClientOptions();
            client = new MqttFactory().CreateMqttClient();
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
            await client.SubscribeAsync("#");
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
