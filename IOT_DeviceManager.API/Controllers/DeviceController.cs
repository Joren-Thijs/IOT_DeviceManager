using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Extensions;
using IOT_DeviceManager.API.Repositories;
using IOT_DeviceManager.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IOT_DeviceManager.API.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceClientService _deviceClientService;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceController(DeviceClientService deviceClientService, IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceClientService = deviceClientService;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _deviceRepository.GetDevices();
            var devicesDto = _mapper.Map<IEnumerable<IDeviceDto>>(devices);

            return Ok(devicesDto.SerializeJson());
        }
    }
}
