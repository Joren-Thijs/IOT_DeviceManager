using System;
using System.ComponentModel;
using IOT_DeviceManager.APP.DTO.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IOT_DeviceManager.APP.Models;
using IOT_DeviceManager.APP.ViewModels;

namespace IOT_DeviceManager.APP.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DeviceDetailPage : ContentPage
    {
        DeviceDetailViewModel viewModel;

        public DeviceDetailPage(DeviceDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            viewModel.WebClient.WebClientErrorEvent += WebClientOnErrorEvent;
        }

        private void WebClientOnErrorEvent(object sender, string e)
        {
            DisplayAlert("Error", e, "close");
        }

        public DeviceDetailPage()
        {
            InitializeComponent();

            var device = new DeviceDto
            {
                DeviceName = "Device"
            };

            viewModel = new DeviceDetailViewModel(device);
            BindingContext = viewModel;
        }

        private void DeviceStatusButton_OnClicked(object sender, EventArgs e)
        {
            viewModel.ToggleDeviceStatusCommand.Execute(null);
        }

        private void ExpandDeviceMeasurementsButton_OnClicked(object sender, EventArgs e)
        {
            DeviceMeasurementsCollectionView.IsVisible = !DeviceMeasurementsCollectionView.IsVisible;
        }

        private void EditDeviceButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditDevicePage(new EditDeviceViewModel(viewModel.Device)));
        }
    }
}