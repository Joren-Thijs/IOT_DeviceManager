using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Extensions;
using IOT_DeviceManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IOT_DeviceManager.API.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _deviceRepository.GetDevices();
            var devicesDto = _mapper.Map<IEnumerable<IDeviceDto>>(devices);

            return Ok(devicesDto.SerializeJson());
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetDevice([FromRoute] string deviceId)
        {
            var device = await _deviceRepository.GetDevice(deviceId);
            if (device == null)
            {
                return NotFound();
            }

            var deviceDto = _mapper.Map<IDeviceDto>(device);

            return Ok(deviceDto.SerializeJson());
        }
    }
}
