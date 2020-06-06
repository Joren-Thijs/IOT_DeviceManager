using System;

using IOT_DeviceManager.APP.Models;

namespace IOT_DeviceManager.APP.ViewModels
{
    public class DeviceDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public DeviceDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
