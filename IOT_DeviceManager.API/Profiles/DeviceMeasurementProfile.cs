using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Profiles
{
    public class DeviceMeasurementProfile : Profile
    {
        public DeviceMeasurementProfile()
        {
            CreateMap<DeviceMeasurement, DeviceMeasurementDto>();
            CreateMap<MeasurementValue, MeasurementValueDto>();
        }
    }
}
