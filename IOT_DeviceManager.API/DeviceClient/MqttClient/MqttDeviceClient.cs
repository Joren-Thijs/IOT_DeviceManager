using System;
using System.Threading.Tasks;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers;
using IOT_DeviceManager.API.DeviceClient.MqttClient.Options;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Extensions;
using MQTTnet;
using MQTTnet.Client;
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
            _mqttClient.UseApplicationMessageReceivedHandler(OnMessageAsync);
        }

        public virtual async Task OnMessageAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string topic = eventArgs.ApplicationMessage.Topic;
            System.Console.WriteLine("A message is received");
            System.Console.WriteLine(topic);
            if (eventArgs.ApplicationMessage.Topic.EndsWith("/ping"))
            {
                await RespondToPing(topic);
            }

            if (eventArgs.ApplicationMessage.Topic.EndsWith("/ms"))
            {
                HandleMeasurement(eventArgs.ApplicationMessage);
            }
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
            var deviceType = MqttApplicationMessageDeconstructor.GetDeviceTypeFromMessage(message);
            var deviceId = MqttApplicationMessageDeconstructor.GetDeviceIdFromMessage(message);
            IDeviceMeasurement measurement = MqttApplicationMessageDeconstructor.GetDeviceMeasurementFromMessage(message);
            DeviceMeasurementEventArgs eventArgs = new DeviceMeasurementEventArgs(deviceType, deviceId, measurement);
            OnDeviceMeasurementReceived(eventArgs);
        }

        public async Task StartClientAsync()
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions);
            System.Console.WriteLine("MQTT Client is connected");
            await _mqttClient.SubscribeAsync("+/+/ms");
            await _mqttClient.SubscribeAsync("+/+/ping");
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ReconnectAsync();
            }
            await _mqttRpcClient.ConnectAsync(_mqttRpcClientOptions);
            System.Console.WriteLine("MQTT Rpc Client is connected");
        }

        public Task StopClientAsync()
        {
            return Task.CompletedTask;
        }

        public async Task SetDeviceStatus(IDevice device, IDeviceStatus status)
        {
            var topic = MqttApplicationMessageConstructor.GetSetDeviceStatusRpcTopic(device);
            var payload = status.SerializeJson();
            await _mqttRpcClientRpc.ExecuteAsync(TimeSpan.FromSeconds(5), topic, payload,
                MqttQualityOfServiceLevel.AtLeastOnce);
        }

        public void OnDeviceMeasurementReceived(DeviceMeasurementEventArgs eventArgs)
        {
            DeviceMeasurementReceived?.Invoke(this, eventArgs);
        }
    }
}
