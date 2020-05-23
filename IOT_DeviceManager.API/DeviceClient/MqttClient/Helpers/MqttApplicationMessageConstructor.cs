using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Helpers
{
    public class MqttApplicationMessageConstructor
    {
        public static string GetSetDeviceStatusRpcTopic(IDevice device)
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
            var topic = deviceType + "." + device.Id + ".cmd.status";
            return topic;
        }
    }
}
