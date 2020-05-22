using AutoMapper;
using IOT_Thermostat.API.DTO;

namespace IOT_Thermostat.API.Profiles.Device
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Models.Device.Device, DeviceDto>();
        }
    }
}
