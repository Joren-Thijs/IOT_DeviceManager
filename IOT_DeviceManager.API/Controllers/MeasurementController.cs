using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Helpers.Extensions;
using IOT_DeviceManager.API.Helpers.Web;
using IOT_DeviceManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet(Name = "GetMeasurements")]
        public async Task<IActionResult> GetMeasurements([FromRoute] string deviceId, [FromQuery] ResourceParameters resourceParameters)
        {
            var deviceExists = await _deviceRepository.DeviceExists(deviceId);
            if (!deviceExists) return NotFound();

            var measurements = await _deviceRepository.GetMeasurements(deviceId, resourceParameters);

            this.SetXPaginationResponseHeaders("GetMeasurements", measurements, resourceParameters);

            var measurementsDto = _mapper.Map<IEnumerable<IDeviceMeasurementDto>>(measurements);

            return Ok(measurementsDto.SerializeJson());
        }
    }
}
