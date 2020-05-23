using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Extensions;
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
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ReturnTestData([FromRoute] string deviceId)
        {
            var measurements = await _deviceRepository.GetMeasurements(deviceId);
            var measurementsDto = _mapper.Map<IEnumerable<IDeviceMeasurementDto>>(measurements);

            return Ok(measurementsDto.SerializeJson());
        }
    }
}
