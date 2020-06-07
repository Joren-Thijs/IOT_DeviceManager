using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IOT_DeviceManager.APP.Models;
using IOT_DeviceManager.APP.Views;
using IOT_DeviceManager.APP.ViewModels;

namespace IOT_DeviceManager.APP.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DevicesPage : ContentPage
    {
        DevicesViewModel viewModel;

        public DevicesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DevicesViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var device = (DeviceDto)layout.BindingContext;
            await Navigation.PushAsync(new DeviceDetailPage(new DeviceDetailViewModel(device)));
        }

        void AddItem_Clicked(object sender, EventArgs e)
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Devices.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}