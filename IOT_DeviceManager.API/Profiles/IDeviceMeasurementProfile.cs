using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;

namespace IOT_DeviceManager.API.Profiles
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
