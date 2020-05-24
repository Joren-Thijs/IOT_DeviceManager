using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.DTO.Interfaces
{
    public interface IDeviceDto
    {
        public string Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public IDeviceStatusDto Status { get; set; }
    }
}
