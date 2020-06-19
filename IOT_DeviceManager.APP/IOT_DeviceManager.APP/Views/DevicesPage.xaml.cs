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
        readonly DevicesViewModel _viewModel;

        public DevicesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DevicesViewModel();

            _viewModel.WebClient.WebClientErrorEvent += WebClientOnWebClientErrorEvent;
        }

        private void WebClientOnWebClientErrorEvent(object sender, string e)
        {
            DisplayAlert("Error", e, "close");
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var device = (DeviceDto)layout.BindingContext;
            await Navigation.PushAsync(new DeviceDetailPage(new DeviceDetailViewModel(device)));
        }

        void Refresh_Clicked(object sender, EventArgs e)
        {
            _viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Devices.Count == 0)
                _viewModel.IsBusy = true;
        }
    }
}