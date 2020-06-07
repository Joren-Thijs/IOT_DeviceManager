using System;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DeviceDetailViewModel : BaseViewModel
    {
        public DeviceDto Device { get; set; }
        public DeviceDetailViewModel(DeviceDto device = null)
        {
            Title = device?.DeviceName;
            Device = device;
        }
    }
}
