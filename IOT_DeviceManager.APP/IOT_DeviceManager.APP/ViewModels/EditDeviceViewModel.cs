using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using Xamarin.Forms;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class EditDeviceViewModel : BaseViewModel
    {
        private string deviceName;

        public string DeviceName
        {
            get { return deviceName; }
            set { SetProperty(ref deviceName, value); }
        }

        private string deviceType;

        public string DeviceType
        {
            get { return deviceType; }
            set { SetProperty(ref deviceType, value); }
        }


        public DeviceDto Device { get; set; }

        public EditDeviceViewModel(DeviceDto device)
        {
            Device = device;
            DeviceName = Device.DeviceName;
            DeviceType = Device.DeviceType;
        }

        public async Task<bool> UpdateDeviceAsync()
        {
            IsBusy = true;
            var updatedDevice = await WebClient.UpdateDevice(Device.Id, new DeviceForUpdateDto{DeviceName = DeviceName, DeviceType = DeviceType});
            if (updatedDevice == null)
            {
                IsBusy = false;
                return false;
            }
            Device = updatedDevice;
            MessagingCenter.Send(this, "updated device", Device);
            IsBusy = false;
            return true;
        }

        public async Task<bool> DeleteDeviceAsync()
        {
            IsBusy = true;
            var deleted = await WebClient.DeleteDevice(Device.Id);
            if (deleted) MessagingCenter.Send(this, "deleted device", Device.Id);
            IsBusy = false;
            return deleted;
        }
    }
}
