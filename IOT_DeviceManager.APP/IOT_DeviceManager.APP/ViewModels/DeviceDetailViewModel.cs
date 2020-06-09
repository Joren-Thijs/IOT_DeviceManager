using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DeviceDetailViewModel : BaseViewModel
    {
        public DeviceDto Device { get; set; }

        public ObservableCollection<DeviceMeasurementDto> DeviceMeasurements { get; set; }

        public DeviceDetailViewModel(DeviceDto device = null)
        {
            Title = device?.DeviceName;
            Device = device;
            DeviceMeasurements = new ObservableCollection<DeviceMeasurementDto>();
            Task.Run(async () => await loadDeviceMeasurements());
        }

        private async Task loadDeviceMeasurements()
        {
            
            IsBusy = true;
            var measurements = await WebClient.GetDeviceMeasurementsFromDevice(Device.Id);
            DeviceMeasurements.Clear();
            foreach (var measurement in measurements)
            {
                DeviceMeasurements.Add(measurement);
            }
            IsBusy = false;
        }
    }
}
