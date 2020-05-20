using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_Thermostat.API.DTO;
using IOT_Thermostat.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Thermostat.API.Controllers
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
            var measurementsDto = _mapper.Map<IEnumerable<ThermostatMeasurementDto>>(measurements);

            return Ok(new JsonResult(measurementsDto));
        }
    }
}
