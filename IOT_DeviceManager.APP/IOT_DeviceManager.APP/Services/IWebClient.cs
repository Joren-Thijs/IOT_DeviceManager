using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Calculations;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.Services
{
    public interface IWebClient
    {
        event EventHandler<string> WebClientErrorEvent;
        Task<IEnumerable<DeviceDto>> GetDevices(ResourceParameters resourceParameters = null);
        Task<DeviceDto> GetDevice(string deviceId);
        Task<DeviceDto> UpdateDevice(string deviceId, DeviceForUpdateDto deviceForUpdateDto);
        Task<bool> DeleteDevice(string deviceId);

        Task<IEnumerable<DeviceMeasurementDto>> GetDeviceMeasurementsFromDevice(string deviceId, ResourceParameters resourceParameters = null);

        Task<DeviceStatusDto> GetDeviceStatus(string deviceId);

        Task<DeviceStatusDto> SetDeviceStatus(string deviceId, DeviceStatusDto deviceStatusDto);
        Task<DeviceStatusDto> ToggleDeviceOnStatus(string deviceId);

        Task<CalculationResultDto> GetTotalOnTime(string deviceId);
        Task<CalculationResultDto> GetAverage(string deviceId, string propertyName);
    }
}
