using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class DeviceSetting
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
