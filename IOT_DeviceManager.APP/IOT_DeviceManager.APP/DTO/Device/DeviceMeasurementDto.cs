using System;
using System.Collections.Generic;

namespace IOT_DeviceManager.APP.DTO.Device
{
    public class DeviceMeasurementDto
    {
        public Guid Id { get; set; }
        public DeviceStatusDto Status { get; set; }
        public IDictionary<string, object> Values { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
