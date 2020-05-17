using System.Threading.Tasks;

namespace Mqtt.Client.AspNetCore.DeviceClient
{
    public interface IDeviceClient
    {
        Task StartClientAsync();
        Task StopClientAsync();
        Task SetDeviceStatus(string deviceName);
    }
}
