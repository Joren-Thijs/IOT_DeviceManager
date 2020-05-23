using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO;
using IOT_DeviceManager.API.Repositories;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public TestController(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> ReturnTestData()
        {
            var measurements = await _deviceRepository.GetMeasurements("2B0RN0T2B");
            var measurementsDto = _mapper.Map<IEnumerable<ThermostatDeviceMeasurementDto>>(measurements);

            return Ok(new JsonResult(measurementsDto));
        }
    }
}
