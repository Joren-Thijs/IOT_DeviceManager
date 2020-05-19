using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Thermostat.API.Repositories;
using Newtonsoft.Json;

namespace IOT_Thermostat.API.Controllers
{   
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public TestController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ReturnTestData()
        {
            var measurements = await _deviceRepository.GetMeasurements("2B0RN0T2B");
            var measurementsString = JsonConvert.SerializeObject(measurements);
            return Ok(measurements);
        }
    }
}
