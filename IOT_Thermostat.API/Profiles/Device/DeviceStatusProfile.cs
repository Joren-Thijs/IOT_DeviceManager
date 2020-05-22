using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Models.Device;

namespace IOT_Thermostat.API.Profiles.Device
{
    public class DeviceStatusProfile : Profile
    {
        public DeviceStatusProfile()
        {
            CreateMap<DeviceStatus, DeviceStatusDto>();
        }
    }
}
