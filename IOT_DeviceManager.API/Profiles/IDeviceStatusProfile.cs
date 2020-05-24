using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Profiles
{
    public class IDeviceStatusProfile : Profile
    {
        public IDeviceStatusProfile()
        {
            CreateMap<IDeviceStatus, IDeviceStatusDto>()
                .Include<DeviceStatus, DeviceStatusDto>();

            CreateMap<DeviceStatus, DeviceStatusDto>();
        }
    }
}
