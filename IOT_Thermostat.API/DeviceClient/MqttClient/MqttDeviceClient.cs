using System;
using System.Threading.Tasks;
using IOT_Thermostat.API.DeviceClient.MqttClient.Helpers;
using IOT_Thermostat.API.DeviceClient.MqttClient.Options;
using IOT_Thermostat.API.Entity.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Extensions.Rpc.Options;
using MQTTnet.Protocol;

namespace IOT_Thermostat.API.DeviceClient.MqttClient
{
    public class MqttDeviceClient : IDeviceClient
    {
        private readonly IMqttClientOptions _mqttClientOptions;

        private readonly IMqttRpcClientOptions _mqttRpcClientOptions;

        private IMqttClient _mqttClient;

        private MqttRpcClient _mqttRpcClient;

        public event EventHandler<DeviceMeasurementEventArgs> DeviceMeasurementReceived;

        public MqttDeviceClient()
        {
            _mqttClientOptions = MqttDeviceClientOptionsLoader.LoadMqttClientOptions();
            _mqttRpcClientOptions = MqttDeviceClientOptionsLoader.LoadMqttRpcClientOptions();
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttRpcClient = new MqttRpcClient(_mqttClient, _mqttRpcClientOptions);
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
            System.Console.WriteLine("Client is connected");
            await _mqttClient.SubscribeAsync("+/+/ms");
            await _mqttClient.SubscribeAsync("+/+/ping");
            if(!_mqttClient.IsConnected)
            {
                await _mqttClient.ReconnectAsync();
            }
        }

        public Task StopClientAsync()
        {
            return Task.CompletedTask;
        }

        public async Task SetDeviceStatus(string deviceName)
        {
            string topic = deviceName + ".cmd.status";
            string payload = "{\"status\":\"true\"}";
            await _mqttRpcClient.ExecuteAsync(TimeSpan.FromSeconds(5), topic, payload,
                MqttQualityOfServiceLevel.AtLeastOnce);
        }

        public void OnDeviceMeasurementReceived(DeviceMeasurementEventArgs eventArgs)
        {
            DeviceMeasurementReceived?.Invoke(this, eventArgs);
        }
    }
}
