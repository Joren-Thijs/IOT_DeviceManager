using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Models.Device;

namespace IOT_Thermostat.API.Profiles.Device
{
    public class DeviceMeasurementProfile : Profile
    {
        public DeviceMeasurementProfile()
        {
            CreateMap<DeviceMeasurement, DeviceMeasurementDto>();
        }
    }
}
