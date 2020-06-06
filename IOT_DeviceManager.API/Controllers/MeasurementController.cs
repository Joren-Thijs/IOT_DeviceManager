using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.Helpers.Extensions;
using IOT_DeviceManager.API.Helpers.Web;
using IOT_DeviceManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IOT_DeviceManager.API.Controllers
{
    [ApiController]
    [Route("api/devices/{deviceId}/measurements")]
    public class MeasurementController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public MeasurementController(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetDeviceMeasurementsFromDevice")]
        [HttpHead]
        public async Task<IActionResult> GetDeviceMeasurementsFromDevice([FromRoute] string deviceId, [FromQuery] ResourceParameters resourceParameters)
        {
            var deviceExists = await _deviceRepository.DeviceExists(deviceId);
            if (!deviceExists) return NotFound();

            var measurements = await _deviceRepository.GetMeasurements(deviceId, resourceParameters);

            this.SetXPaginationResponseHeaders("GetDeviceMeasurementsFromDevice", measurements, resourceParameters);

            var measurementsDto = _mapper.Map<IEnumerable<DeviceMeasurementDto>>(measurements);

            return Ok(measurementsDto.SerializeJson());
        }

        [HttpOptions]
        public IActionResult GetDeviceMeasurementOptions()
        {
            Response.Headers.Add("Allow", "GET,HEAD,OPTIONS");
            return Ok();
        }
    }
}
