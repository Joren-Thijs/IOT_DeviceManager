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
        private IDictionary<string, object> _values = new Dictionary<string, object>();

        public Guid Id { get; set; }
        public DeviceStatusDto Status { get; set; }

        [JsonConverter(typeof(AnonymousDictionaryDeserializer))]
        public IDictionary<string, object> Values
        {
            get
            {
                Debug.WriteLine($"GET: {_values.SerializeJson()}");
                return _values;
            }
            set
            {
                Debug.WriteLine($"SET: {value.SerializeJson()}");
                _values = value;
            }
        }

        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
