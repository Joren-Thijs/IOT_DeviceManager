using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Helpers.Extensions;
using Xamarin.Forms;

using IOT_DeviceManager.APP.Models;
using IOT_DeviceManager.APP.Views;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        public ObservableCollection<DeviceDto> Devices { get; set; }
        public Command LoadItemsCommand { get; set; }

        public DevicesViewModel()
        {
            Title = "My Devices";
            Devices = new ObservableCollection<DeviceDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Devices.Clear();
                Devices.Add(new DeviceDto
                {
                    Id = "Mock-device",
                    DeviceName = "Test Device",
                    DeviceType = "Mock-Device",
                    Status = new DeviceStatusDto
                    {
                        OnStatus = false,
                        Settings = new Dictionary<string, object>
                        {
                            {"Sensor 1", 25}
                        }
                    }
                });

                var devices = await WebClient.GetDevices();

                foreach (var device in devices)
                {
                    if (string.IsNullOrEmpty(device.DeviceName)) device.DeviceName = "New Device";
                    Devices.Add(device);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}