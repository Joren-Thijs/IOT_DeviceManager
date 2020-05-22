using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles
{
    public class ThermostatDeviceStatusDto : Profile
    {
        public ThermostatDeviceStatusDto()
        {
            CreateMap<ThermostatDeviceStatus, ThermostatDeviceStatusDto>();
        }
    }
}
