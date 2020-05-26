using Newtonsoft.Json;

namespace IOT_DeviceManager.API.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeJson(this object obj, Formatting formatting = Formatting.Indented) => JsonConvert.SerializeObject(obj, formatting);
    }
}
