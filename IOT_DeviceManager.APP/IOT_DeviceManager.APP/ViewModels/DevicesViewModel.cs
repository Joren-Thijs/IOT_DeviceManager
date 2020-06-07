using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
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
                    DeviceName = "Test Device",
                    DeviceType = "Mock-Device"
                });
                
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