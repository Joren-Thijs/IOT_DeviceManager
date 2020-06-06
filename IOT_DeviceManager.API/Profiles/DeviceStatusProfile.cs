using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Profiles
{
    public class DeviceStatusProfile : Profile
    {
        public DeviceStatusProfile()
        {
            CreateMap<DeviceStatus, DeviceStatusDto>();
            CreateMap<DeviceSetting, DeviceSettingDto>();
        }
    }
}
