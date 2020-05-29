using System.ComponentModel.DataAnnotations;
using IOT_DeviceManager.API.DTO.Interfaces;

namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceForUpdateDto : IDeviceDto
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[\w'À-ÿ\.\-\/@][\w' À-ÿ\.\-\/@]*[\w'À-ÿ\.\-\/@]+$")]
        public string DeviceType { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"^[\w'À-ÿ\.\-\/@][\w' À-ÿ\.\-\/@]*[\w'À-ÿ\.\-\/@]+$")]
        public string DeviceName { get; set; }
    }
}
