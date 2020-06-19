using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IOT_DeviceManager.APP.Services;
using IOT_DeviceManager.APP.Views;

namespace IOT_DeviceManager.APP
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IWebClient, WebClient>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
