namespace IOT_DeviceManager.API.DeviceClient
{
    public class DeviceDisconnectedEventArgs
    {
        public DeviceDisconnectedEventArgs() { }
        public DeviceDisconnectedEventArgs(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; set; }
    }
}
