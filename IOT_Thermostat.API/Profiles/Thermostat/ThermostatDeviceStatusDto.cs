using AutoMapper;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles.Thermostat
{
    public class ThermostatDeviceStatusDto : Profile
    {
        public ThermostatDeviceStatusDto()
        {
            CreateMap<ThermostatDeviceStatus, ThermostatDeviceStatusDto>();
        }
    }
}
