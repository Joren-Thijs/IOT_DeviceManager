using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDto>().ReverseMap();
            CreateMap<Device, DeviceForUpdateDto>().ReverseMap();
        }
    }
}
