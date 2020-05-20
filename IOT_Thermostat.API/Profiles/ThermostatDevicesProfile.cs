using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace IOT_Thermostat.API.Profiles
{
    public class ThermostatDevicesProfile : Profile
    {
        public ThermostatDevicesProfile()
        {
            CreateMap<Models.ThermostatDevice, DTO.ThermostatDeviceDto>();
        }
    }
}
