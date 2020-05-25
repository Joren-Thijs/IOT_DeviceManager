using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.DeviceClient.MqttClient.Requests.DeviceId
{
    public class DeviceIdRequestResponseDto
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string TransactionKey { get; set; }
    }
}
