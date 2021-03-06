﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Entity.Device;
using IOT_DeviceManager.API.Helpers.Extensions;
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
            _deviceClientService = deviceClientService ?? throw new ArgumentNullException(nameof(deviceClientService));
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetDeviceStatus([FromRoute] string deviceId)
        {
            var deviceExists = await _deviceRepository.DeviceExists(deviceId);
            if (!deviceExists) return NotFound();

            var deviceStatus = await _deviceRepository.GetDeviceStatus(deviceId);

            var deviceStatusDto = _mapper.Map<IDeviceStatusDto>(deviceStatus);

            return Ok(deviceStatusDto.SerializeJson());
        }

        [HttpPost]
        public async Task<IActionResult> SetDeviceStatus([FromRoute] string deviceId, [FromBody] DeviceStatus newStatus)
        {
            var device = await _deviceRepository.GetDevice(deviceId);
            if (device == null)
            {
                return NotFound();
            }

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

        [HttpOptions]
        public IActionResult GetDeviceStatusOptions()
        {
            Response.Headers.Add("Allow", "GET,HEAD,POST,OPTIONS");
            return Ok();
        }
    }
}
