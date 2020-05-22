using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Models.Device;

namespace IOT_Thermostat.API.Profiles
{
    public class DeviceMeasurementProfile : Profile
    {
        public DeviceMeasurementProfile()
        {
            CreateMap<DeviceMeasurement, DeviceMeasurementDto>();
        }
    }
}
