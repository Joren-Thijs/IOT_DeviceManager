using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class MeasurementValueDto
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
