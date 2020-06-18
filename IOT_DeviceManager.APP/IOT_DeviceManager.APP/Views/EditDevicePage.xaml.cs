using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT_DeviceManager.APP.DTO.Device;
using IOT_DeviceManager.APP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IOT_DeviceManager.APP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditDevicePage : ContentPage
    {
        private EditDeviceViewModel _viewModel;

		public EditDevicePage (EditDeviceViewModel viewmodel)
		{
			InitializeComponent ();
            this._viewModel = viewmodel;
            BindingContext = this._viewModel;
        }

        private async void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            if (await _viewModel.DeleteDeviceAsync())
            {
                await Navigation.PopToRootAsync(true);
            }
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            if (await _viewModel.UpdateDeviceAsync())
            {
                await Navigation.PopAsync(true);
            }
        }
    }
}