using System;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeJson(this object obj, Formatting formatting = Formatting.Indented) => JsonConvert.SerializeObject(obj, formatting);
    }
}
