using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageConstructor
    {
        public static string GetSetDeviceStatusRpcTopic(IDevice device)
        {
            var deviceType = GetDeviceTypeStringFromDevice(device);
            var topic = deviceType + "." + device.Id + ".cmd.status";
            return topic;
        }

        public static IDeviceStatus GetDeviceStatusFromRcpAnswer(IDevice device, byte[] rcpAnswer)
        {
            var payload = Encoding.UTF8.GetString(rcpAnswer);
            var deviceType = GetDeviceTypeStringFromDevice(device);
            IDeviceStatus status = deviceType switch
            {
                "thermostat" => JsonConvert.DeserializeObject<ThermostatDeviceStatus>(payload),
                "device" => JsonConvert.DeserializeObject<DeviceStatus>(payload),
                _ => JsonConvert.DeserializeObject<DeviceStatus>(payload),
            };
            return status;
        }

        private static string GetDeviceTypeStringFromDevice(IDevice device)
        {
            var deviceType = "";
            if (device.GetType() == typeof(Device))
            {
                deviceType = "device";
            }
            if (device.GetType() == typeof(ThermostatDevice))
            {
                deviceType = "thermostat";
            }

            return deviceType;
        }
    }
}
