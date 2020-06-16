using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using IOT_DeviceManager.APP.Helpers;
using IOT_DeviceManager.APP.Helpers.Extensions;
using Newtonsoft.Json;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceMeasurementDto
    {
        public Guid Id { get; set; }
        public DeviceStatusDto Status { get; set; }

        [JsonConverter(typeof(AnonymousDictionaryDeserializer))]
        public IDictionary<string, object> Values { get; set; }

        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
