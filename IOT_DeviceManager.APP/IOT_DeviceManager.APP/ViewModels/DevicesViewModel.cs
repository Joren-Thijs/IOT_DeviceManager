using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using Xamarin.Forms;
using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        private ObservableCollection<DeviceDto> _devices;

        public ObservableCollection<DeviceDto> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }

        public Command LoadItemsCommand { get; set; }

        public DevicesViewModel()
        {
            Title = "My Devices";
            Devices = new ObservableCollection<DeviceDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<EditDeviceViewModel, DeviceDto>(this, "updated device",
                (viewmodel, updatedDevice) =>
                {
                    var currentDevice = Devices.FirstOrDefault(x => x.Id == updatedDevice.Id);
                    Devices.Remove(currentDevice);
                    Devices.Add(updatedDevice);
                    Devices = new ObservableCollection<DeviceDto>(Devices.OrderBy(x => x.DeviceName));
                });
            MessagingCenter.Subscribe<EditDeviceViewModel, string>(this, "deleted device",
                (viewmodel, deletedDeviceId) =>
                {
                    var currentDevice = Devices.FirstOrDefault(x => x.Id == deletedDeviceId);
                    if(currentDevice != null) Devices.Remove(currentDevice);
                });
        }

        async Task ExecuteLoadItemsCommand()
        {
            
            IsBusy = true;
            IEnumerable<DeviceDto> devices = new List<DeviceDto>();

            try
            {
                devices = await WebClient.GetDevices(new ResourceParameters { OrderBy = "DeviceName" });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            Devices = new ObservableCollection<DeviceDto>();

            Devices.Add(new DeviceDto
            {
                Id = "Local-Device",
                DeviceName = "Test Device",
                DeviceType = "Local-Device",
                Status = new DeviceStatusDto
                {
                    OnStatus = false,
                    Settings = new Dictionary<string, object>
                    {
                        {"Sensor 1", 25}
                    }
                }
            });

            foreach (var device in devices)
            {
                if (string.IsNullOrEmpty(device.DeviceName)) device.DeviceName = "New Device";
                Devices.Add(device);
            }

            Devices = new ObservableCollection<DeviceDto>(Devices.OrderBy(x => x.DeviceName));

            IsBusy = false;
        }
    }
}