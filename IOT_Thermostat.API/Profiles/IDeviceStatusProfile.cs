using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.DTO.Interfaces;
using IOT_Thermostat.API.Entity.Device;
using IOT_Thermostat.API.Entity.Interfaces;
using IOT_Thermostat.API.Entity.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles
{
    public class IDeviceStatusProfile : Profile
    {
        public IDeviceStatusProfile()
        {
            CreateMap<IDeviceStatus, IDeviceStatusDto>()
                .Include<DeviceStatus, DeviceStatusDto>()
                .Include<ThermostatDeviceStatus, ThermostatDeviceStatusDto>();

            CreateMap<ThermostatDeviceStatus, ThermostatDeviceStatusDto>();
            CreateMap<DeviceStatus, DeviceStatusDto>();
        }
    }
}
