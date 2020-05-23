using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.DTO.Interfaces
{
    public interface IDeviceMeasurementDto
    {
        public string Id { get; set; }
        public IDeviceStatusDto Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
