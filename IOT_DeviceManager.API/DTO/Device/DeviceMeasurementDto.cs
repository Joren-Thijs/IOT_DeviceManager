using System;
using System.Collections.Generic;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceMeasurementDto
    {
        public Guid Id { get; set; }
        public DeviceStatusDto Status { get; set; }
        public IEnumerable<MeasurementValueDto> Values { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DeviceId { get; set; }
    }
}
