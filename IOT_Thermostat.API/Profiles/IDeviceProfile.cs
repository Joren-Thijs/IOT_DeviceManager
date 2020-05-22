using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.DTO.Interfaces;
using IOT_Thermostat.API.Models.Interfaces;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Profiles
{
    public class IDeviceProfile : Profile
    {
        public IDeviceProfile()
        {
            CreateMap<IDevice, IDeviceDto>()
                .Include<Models.Device.Device, DeviceDto>()
                .Include<ThermostatDevice, ThermostatDeviceDto>();

            CreateMap<Models.Device.Device, DeviceDto>();
            CreateMap<ThermostatDevice, ThermostatDeviceDto>();
        }
    }
}
