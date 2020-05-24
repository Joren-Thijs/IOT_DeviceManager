using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.DTO.Interfaces
{
    public interface IDeviceStatusDto
    {
        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
    }
}
