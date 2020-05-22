using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles.Thermostat
{
    public class ThermostatDeviceMeasurementProfile : Profile
    {
        public ThermostatDeviceMeasurementProfile()
        {
            CreateMap<ThermostatDeviceMeasurement, ThermostatDeviceMeasurementDto>();
        }
    }
}
