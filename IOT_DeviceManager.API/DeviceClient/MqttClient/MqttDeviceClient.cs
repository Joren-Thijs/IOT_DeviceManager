using System;
using System.Threading.Tasks;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Options;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Extensions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Extensions.Rpc.Options;
using MQTTnet.Protocol;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient
{
    public class MqttDeviceClient : IDeviceClient
    {
        private readonly IMqttClientOptions _mqttClientOptions;

        private IMqttClientOptions _mqttRpcClientOptions;

        private readonly IMqttRpcClientOptions _mqttRpcClientOptionsRpc;

        private IMqttClient _mqttClient;

        private IMqttClient _mqttRpcClient;

        private MqttRpcClient _mqttRpcClientRpc;
        

        public event EventHandler<DeviceMeasurementEventArgs> DeviceMeasurementReceived;

        public MqttDeviceClient()
        {
            _mqttClientOptions = MqttDeviceClientOptionsLoader.LoadMqttClientOptions();
            _mqttRpcClientOptions = MqttDeviceClientOptionsLoader.LoadMqttRpcClientOptions();
            _mqttRpcClientOptionsRpc = MqttDeviceClientOptionsLoader.LoadMqttRpcClientRpcOptions();
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttRpcClient = new MqttFactory().CreateMqttClient();
            _mqttRpcClientRpc = new MqttRpcClient(_mqttRpcClient, _mqttRpcClientOptionsRpc);
            SetupClient();
        }

        private void SetupClient()
        {
            _mqttClient.UseConnectedHandler(OnMqttClientConnectedAsync);
            _mqttRpcClient.UseConnectedHandler(OnMqttRpcClientConnectedAsync);
            _mqttClient.UseApplicationMessageReceivedHandler(OnMqttClientMessageReceivedAsync);
        }

        private  Task OnMqttClientConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("MQTT Client is connected");
            return Task.CompletedTask;
        }
        private Task OnMqttRpcClientConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("MQTT RPC Client is connected");
            return Task.CompletedTask;
        }

        public virtual async Task OnMqttClientMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string topic = eventArgs.ApplicationMessage.Topic;
            System.Console.WriteLine("A message is received");
            System.Console.WriteLine(topic);

            if (eventArgs.ApplicationMessage.Topic.EndsWith("/request/id"))
            {
                await RespondToRequestId(eventArgs.ApplicationMessage);
            }

            if (eventArgs.ApplicationMessage.Topic.EndsWith("/request/ping"))
            {
                await RespondToPing(eventArgs.ApplicationMessage.Topic);
            }

            if (eventArgs.ApplicationMessage.Topic.EndsWith("/ms"))
            {
                HandleMeasurement(eventArgs.ApplicationMessage);
            }
        }

        private async Task RespondToRequestId(MqttApplicationMessage message)
        {
            var requestResponseDto = MqttApplicationMessageHelper.GetDeviceIdRequestResponseFromMessage(message);
            var topic = MqttApplicationMessageHelper.GetDeviceIdRequestResponseTopicFromMessage(message);
            var payload = requestResponseDto.SerializeJson();
            var responseMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .Build();
            await _mqttClient.PublishAsync(responseMessage);
        }

        private async Task RespondToPing(string topic)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic + "/response")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();
            await _mqttClient.PublishAsync(message);
        }


        private void HandleMeasurement(MqttApplicationMessage message)
        {
            var deviceType = MqttApplicationMessageHelper.GetDeviceTypeFromMessage(message);
            var deviceId = MqttApplicationMessageHelper.GetDeviceIdFromMessage(message);
            IDeviceMeasurement measurement = MqttApplicationMessageHelper.GetDeviceMeasurementFromMessage(message);
            DeviceMeasurementEventArgs eventArgs = new DeviceMeasurementEventArgs(deviceType, deviceId, measurement);
            OnDeviceMeasurementReceived(eventArgs);
        }

        public async Task StartClientAsync()
        {
            await ConnectToMqttBroker();
            
            await _mqttClient.SubscribeAsync("+/+/request/+");
            await _mqttClient.SubscribeAsync("+/+/ms");
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ReconnectAsync();
            }
            
        }

        private async Task ConnectToMqttBroker()
        {
            while (!_mqttClient.IsConnected)
            {
                try
                {
                    await _mqttClient.ConnectAsync(_mqttClientOptions);
                    await _mqttRpcClient.ConnectAsync(_mqttRpcClientOptions);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Connection to MQTT Broker failed");
                    Console.WriteLine("Attempting reconnection...");
                }
            }
        }

        public Task StopClientAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IDeviceStatus> SetDeviceStatus(IDevice device, IDeviceStatus status)
        {
            var topic = MqttApplicationMessageHelper.GetSetDeviceStatusRpcTopicFromDevice(device);
            var payload = status.SerializeJson();
            var rcpAnswer = await _mqttRpcClientRpc.ExecuteAsync(TimeSpan.FromSeconds(5), topic, payload,
                MqttQualityOfServiceLevel.AtLeastOnce);
            var newStatus = MqttApplicationMessageHelper.GetDeviceStatusFromRcpAnswer(device, rcpAnswer);
            return newStatus;
        }

        public void OnDeviceMeasurementReceived(DeviceMeasurementEventArgs eventArgs)
        {
            DeviceMeasurementReceived?.Invoke(this, eventArgs);
        }
    }
}
