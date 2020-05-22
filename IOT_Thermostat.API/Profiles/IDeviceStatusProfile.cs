using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.DTO.Interfaces;
using IOT_Thermostat.API.Models.Device;
using IOT_Thermostat.API.Models.Interfaces;
using IOT_Thermostat.API.Models.ThermostatDevice;

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
