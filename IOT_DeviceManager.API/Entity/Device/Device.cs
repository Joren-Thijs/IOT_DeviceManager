using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IOT_DeviceManager.API.Entity.Device
{
    public class Device
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[\w'À-ÿ\.\-\/@][\w' À-ÿ\.\-\/@]*[\w'À-ÿ\.\-\/@]+$")]
        public string DeviceType { get; set; } = "device";
        [MaxLength(50)]
        [RegularExpression(@"^[\w'À-ÿ\.\-\/@][\w' À-ÿ\.\-\/@]*[\w'À-ÿ\.\-\/@]+$")]
        public string DeviceName { get; set; }
        public DeviceStatus Status { get; set; }
        public IEnumerable<DeviceMeasurement> Measurements { get; set; } = new List<DeviceMeasurement>();
    }
}
