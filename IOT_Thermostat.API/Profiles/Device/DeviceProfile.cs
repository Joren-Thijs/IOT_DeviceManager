using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Models.Device;

namespace IOT_Thermostat.API.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDto>();
        }
    }
}
