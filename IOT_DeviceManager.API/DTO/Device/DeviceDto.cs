namespace IOT_DeviceManager.API.DTO.Device
{
    public class DeviceDto
    {
        public string Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public DeviceStatusDto Status { get; set; }
    }
}
