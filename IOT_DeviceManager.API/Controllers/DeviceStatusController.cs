using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;
using IOT_DeviceManager.API.Extensions;
using IOT_DeviceManager.API.Repositories;
using IOT_DeviceManager.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IOT_DeviceManager.API.Controllers
{
    [ApiController]
    [Route("api/devices/{deviceId}/status")]
    public class DeviceStatusController : ControllerBase
    {
        private readonly DeviceClientService _deviceClientService;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceStatusController(DeviceClientService deviceClientService, IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceClientService = deviceClientService;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SetDeviceStatus([FromRoute] string deviceId, [FromBody] string deviceStatus)
        {
            var device = await _deviceRepository.GetDevice(deviceId);
            if (device == null)
            {
                return NotFound();
            }

            var newStatus = deviceStatus.DeserializeJson<ThermostatDeviceStatus>();
            var statusAnswer = await _deviceClientService.SetDeviceStatusAsync(device, newStatus);

            return Ok(statusAnswer.SerializeJson());
        }

        [HttpPost("onstatus/toggle")]
        public async Task<IActionResult> ToggleDeviceOnStatus([FromRoute] string deviceId)
        {
            var device = await _deviceRepository.GetDevice(deviceId);
            if (device == null)
            {
                return NotFound();
            }

            var newStatus = device.Status;
            newStatus.OnStatus = !device.Status.OnStatus;

            var statusAnswer = await _deviceClientService.SetDeviceStatusAsync(device, newStatus);

            return Ok(statusAnswer.SerializeJson());
        }
    }
}
