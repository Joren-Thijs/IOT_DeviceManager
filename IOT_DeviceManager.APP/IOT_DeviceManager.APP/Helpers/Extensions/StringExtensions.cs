using Newtonsoft.Json;

namespace IOT_DeviceManager.APP.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static T DeserializeJson<T>(this string s) => JsonConvert.DeserializeObject<T>(s);
    }
}
