using AutoMapper;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles.Thermostat
{
    public class ThermostatDeviceProfile : Profile
    {
        public ThermostatDeviceProfile()
        {
            CreateMap<ThermostatDevice, DTO.ThermostatDeviceDto>();
        }
    }
}
