using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.Models;
using Xamarin.Forms;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DeviceDetailViewModel : BaseViewModel
    {
        private DeviceDto _device;
        private ObservableCollection<DeviceMeasurementDto> _deviceMeasurements;
        private bool _onStatus;

        public DeviceDto Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }

        public ObservableCollection<DeviceMeasurementDto> DeviceMeasurements
        {
            get => _deviceMeasurements;
            set => SetProperty(ref _deviceMeasurements, value);
        }

        public ICommand ToggleDeviceStatusCommand { get; }

        public DeviceDetailViewModel(DeviceDto device = null)
        {
            Title = device?.DeviceName;
            Device = device;
            DeviceMeasurements = new ObservableCollection<DeviceMeasurementDto>();
            ToggleDeviceStatusCommand = new Command(ExecuteToggleDeviceStatusCommand);
            Task.Run(async () => await LoadDeviceMeasurements());
        }

        private async void ExecuteToggleDeviceStatusCommand(object obj)
        {
            IsBusy = true;
            var status = await WebClient.ToggleDeviceOnStatus(Device.Id);

            if (status.Settings == null)
            {
                Device.Status.OnStatus = false;
                status.Settings = Device.Status.Settings;
            }

            Device.Status = status;
            OnPropertyChanged(nameof(Device));
            IsBusy = false;
        }

        private async Task LoadDeviceMeasurements()
        {
            IsBusy = true;

            var measurements = await WebClient.GetDeviceMeasurementsFromDevice(Device.Id);

            DeviceMeasurements = new ObservableCollection<DeviceMeasurementDto>(measurements);
            IsBusy = false;
        }
    }
}
