using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace IOT_Thermostat.API.Profiles
{
    public class ThermostatMeasurementsProfile : Profile
    {
        public ThermostatMeasurementsProfile()
        {
            CreateMap<Models.ThermostatMeasurement, DTO.ThermostatMeasurementDto>();
        }
    }
}
