using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using MQTTnet;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageConstructor
    {
        public static string GetSetDeviceStatusRpcTopic(IDevice device)
        {
            var topic = device.DeviceType + "." + device.Id + ".cmd.status";
            return topic;
        }

        public static IDeviceStatus GetDeviceStatusFromRcpAnswer(IDevice device, byte[] rcpAnswer)
        {
            var payload = Encoding.UTF8.GetString(rcpAnswer);
            IDeviceStatus status = JsonConvert.DeserializeObject<DeviceStatus>(payload);
            
            return status;
        }
    }
}
