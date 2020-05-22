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
    public class IDeviceMeasurementProfile : Profile
    {
        public IDeviceMeasurementProfile()
        {
            CreateMap<IDeviceMeasurement, IDeviceMeasurementDto>()
                .Include<DeviceMeasurement, DeviceMeasurementDto>()
                .Include<ThermostatDeviceMeasurement, ThermostatDeviceMeasurementDto>();

            CreateMap<DeviceMeasurement, DeviceMeasurementDto>();
            CreateMap<ThermostatDeviceMeasurement, ThermostatDeviceMeasurementDto>();
        }
    }
}
