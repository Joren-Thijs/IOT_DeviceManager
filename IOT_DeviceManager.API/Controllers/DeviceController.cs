using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IOT_DeviceManager.API.DTO.Device;
using IOT_DeviceManager.API.DTO.Interfaces;
using IOT_DeviceManager.API.Helpers.Extensions;
using IOT_DeviceManager.API.Helpers.Web;
using IOT_DeviceManager.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

        [HttpGet(Name = "GetDevices")]
        public async Task<IActionResult> GetDevices([FromQuery] ResourceParameters resourceParameters)
        {
            var devices = await _deviceRepository.GetDevices(resourceParameters);

            this.SetXPaginationResponseHeaders("GetDevices", devices, resourceParameters);

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

        [HttpPut("{deviceId}")]
        public async Task<IActionResult> UpdateDevice([FromRoute] string deviceId, [FromBody] DeviceForUpdateDto deviceUpdateDto)
        {
            var deviceFromRepo = await _deviceRepository.GetDevice(deviceId);
            if (deviceFromRepo == null)
            {
                return NotFound();
            }

            // map the IDevice entity to a deviceForUpdateDto
            // apply the updated field values to that dto
            // map the deviceForUpdateDto back to an IDevice Entity
            _mapper.Map(deviceUpdateDto, deviceFromRepo);

            var updatedDevice = await _deviceRepository.UpdateDevice(deviceFromRepo);
            await _deviceRepository.Save();

            var updatedDeviceDto = _mapper.Map<IDeviceDto>(updatedDevice);

            return Ok(updatedDeviceDto.SerializeJson());
        }

        [HttpPatch("{deviceId}")]
        public async Task<IActionResult> PartiallyUpdateDevice([FromRoute] string deviceId, [FromBody] JsonPatchDocument<DeviceForUpdateDto> patchDocument)
        {
            var deviceFromRepo = await _deviceRepository.GetDevice(deviceId);
            if (deviceFromRepo == null)
            {
                return NotFound();
            }

            var deviceToPatch = _mapper.Map<DeviceForUpdateDto>(deviceFromRepo);

            // Add validation
            patchDocument.ApplyTo(deviceToPatch, ModelState);
            if (!TryValidateModel(deviceToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // map the IDevice entity to a deviceForUpdateDto
            // apply the updated field values to that dto
            // map the deviceForUpdateDto back to an IDevice Entity
            _mapper.Map(deviceToPatch, deviceFromRepo);

            var updatedDevice = await _deviceRepository.UpdateDevice(deviceFromRepo);
            await _deviceRepository.Save();

            var updatedDeviceDto = _mapper.Map<IDeviceDto>(updatedDevice);

            return Ok(updatedDeviceDto.SerializeJson());
        }

        // Override default method to make sure we get a code 422 on invalid models when patching
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
