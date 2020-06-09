using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Calculations;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Helpers.Extensions;
using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.Services
{
    public class WebClient : IWebClient
    {
        private const string BaseUrl = "http://127.0.0.1:53967/api/";
        private readonly HttpClient _httpClient;

        public WebClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<DeviceDto>> GetDevices(ResourceParameters resourceParameters = null)
        {
            var uriString = BaseUrl + "devices";
            var uri = new Uri(uriString);
            
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<IEnumerable<DeviceDto>>();
        }

        public async Task<DeviceDto> GetDevice(string deviceId)
        {
            var uriString = BaseUrl + "devices/" + deviceId;
            var uri = new Uri(uriString);
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<DeviceDto>();
        }

        public async Task<DeviceDto> UpdateDevice(string deviceId, DeviceForUpdateDto deviceForUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceDto> UpdateDeviceName(string deviceId, string deviceName)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceDto> UpdateDeviceType(string deviceId, string deviceType)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDevice(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeviceMeasurementDto>> GetDeviceMeasurementsFromDevice(string deviceId, ResourceParameters resourceParameters = null)
        {
            var uriString = BaseUrl + "devices/" + deviceId + "/measurements";
            var uri = new Uri(uriString);
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<DeviceMeasurementDto>();
            }

            if (!response.IsSuccessStatusCode)
            {
                return new List<DeviceMeasurementDto>();
            }
            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<IEnumerable<DeviceMeasurementDto>>();
        }

        public async Task<DeviceStatusDto> GetDeviceStatus(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceStatusDto> SetDeviceStatus(string deviceId, DeviceStatusDto deviceStatusDto)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceStatusDto> ToggleDeviceOnStatus(string deviceId, DeviceStatusDto deviceStatusDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CalculationResultDto> GetTotalOnTime(string deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<CalculationResultDto> GetAverage(string deviceId, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
