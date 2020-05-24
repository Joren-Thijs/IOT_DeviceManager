using System.Collections.Generic;

namespace IOT_DeviceManager.API.Entity.Interfaces
{
    public interface IDeviceStatus
    {
        public bool OnStatus { get; set; }
        public IDictionary<string, object> Settings { get; set; }
    }
}
