using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Profiles
{
    public class IDeviceProfile : Profile
    {
        public IDeviceProfile()
        {
            CreateMap<IDevice, IDeviceDto>()
                .Include<Device, DeviceDto>()
                .Include<Device, DeviceForUpdateDto>().ReverseMap();

            CreateMap<Device, DeviceDto>().ReverseMap();
            CreateMap<Device, DeviceForUpdateDto>().ReverseMap();
        }
    }
}
