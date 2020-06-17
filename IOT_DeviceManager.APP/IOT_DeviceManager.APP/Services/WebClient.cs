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
using Flurl;
using Flurl.Http;

namespace IOT_DeviceManager.APP.Services
{
    public class WebClient : IWebClient
    {
        public event EventHandler<string> WebClientErrorEvent;

        private readonly Url _baseUrl = new Url("http://127.0.0.1:53967/api/");

        public async Task<IEnumerable<DeviceDto>> GetDevices(ResourceParameters resourceParameters = null)
        {
            var url = _baseUrl.AppendPathSegment("devices").SetQueryParams(resourceParameters);

            var response = await url.GetStringAsync();
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem loading the devices");
                return new List<DeviceDto>();
            }

            return response.DeserializeJson<IEnumerable<DeviceDto>>();
        }

        public async Task<DeviceDto> GetDevice(string deviceId)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId);

            var response = await GetResourcesAsync(url);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem loading the device details");
                return new DeviceDto();
            }

            return response.DeserializeJson<DeviceDto>();
        }

        public async Task<DeviceDto> UpdateDevice(string deviceId, DeviceForUpdateDto deviceForUpdateDto)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId);
            var stringContent = new StringContent(deviceForUpdateDto.SerializeJson(), Encoding.UTF8);

            var response = await PutResourcesAsync(url, stringContent);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem updating the device");
                return new DeviceDto();
            }

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<DeviceDto>();
        }

        public async Task<bool> DeleteDevice(string deviceId)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId);
            return await DeleteResourcesAsync(url);

        }

        public async Task<IEnumerable<DeviceMeasurementDto>> GetDeviceMeasurementsFromDevice(string deviceId, ResourceParameters resourceParameters = null)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "measurements").SetQueryParams(resourceParameters);

            var response = await GetResourcesAsync(url);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem loading the device measurements");
                return new List<DeviceMeasurementDto>();
            }

            return response.DeserializeJson<IEnumerable<DeviceMeasurementDto>>();
        }

        public async Task<DeviceStatusDto> GetDeviceStatus(string deviceId)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "status");

            var response = await GetResourcesAsync(url);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem loading the device status");
                return new DeviceStatusDto();
            }

            return response.DeserializeJson<DeviceStatusDto>();
        }

        public async Task<DeviceStatusDto> SetDeviceStatus(string deviceId, DeviceStatusDto deviceStatusDto)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "status");
            var stringContent = new StringContent(deviceStatusDto.SerializeJson(), Encoding.UTF8);

            var response = await PostResourcesAsync(url, stringContent);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem setting the device status");
                return new DeviceStatusDto();
            }

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<DeviceStatusDto>();
        }

        public async Task<DeviceStatusDto> ToggleDeviceOnStatus(string deviceId)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "status", "onstatus", "toggle");
            var stringContent = new StringContent("", Encoding.UTF8);

            var response = await PostResourcesAsync(url, stringContent);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem setting the device status");
                return new DeviceStatusDto();
            }

            var content = await response.Content.ReadAsStringAsync();
            return content.DeserializeJson<DeviceStatusDto>();
        }

        public async Task<CalculationResultDto> GetTotalOnTime(string deviceId)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "measurements", "calculations", "totalontime");

            var response = await GetResourcesAsync(url);
            if (response == null)
            {
                InvokeWebClientErrorEvent("There was a problem loading the total ON time");
                return new CalculationResultDto();
            }

            return response.DeserializeJson<CalculationResultDto>();
        }

        public async Task<CalculationResultDto> GetAverage(string deviceId, string propertyName)
        {
            var url = _baseUrl.AppendPathSegments("devices", deviceId, "measurements", "calculations", "average", propertyName);

            var response = await GetResourcesAsync(url);
            if (response == null)
            {
                InvokeWebClientErrorEvent($"There was a problem loading the average {propertyName}");
                return new CalculationResultDto();
            }

            return response.DeserializeJson<CalculationResultDto>();
        }

        private void InvokeWebClientErrorEvent(string message)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => WebClientErrorEvent?.Invoke(this, message));
        }

        private async Task<string> GetResourcesAsync(Url url)
        {
            try
            {
                var response = await url.GetStringAsync();
                return response;
            }
            catch (FlurlHttpException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> PostResourcesAsync(Url url, HttpContent content)
        {
            try
            {
                var response = await url.PostAsync(content);
                if (!response.IsSuccessStatusCode) return null;
                return response;
            }
            catch (FlurlHttpException)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> PutResourcesAsync(Url url, HttpContent content)
        {
            try
            {
                var response = await url.PutAsync(content);
                if (!response.IsSuccessStatusCode) return null;
                return response;
            }
            catch (FlurlHttpException)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<bool> DeleteResourcesAsync(Url url)
        {
            try
            {
                var response = await url.DeleteAsync();
                if (!response.IsSuccessStatusCode) return false;
                return true;
            }
            catch (FlurlHttpException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
