using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO
{
    public class DeviceMeasurementDto : IDeviceMeasurementDto
    {
        public string Id { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public IDictionary<string, object> Values { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
