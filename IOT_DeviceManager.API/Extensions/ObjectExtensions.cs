using System;
using Newtonsoft.Json;

namespace IOT_DeviceManager.API.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetDefault(this object obj, Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static string SerializeJson(this object obj, Formatting formatting = Formatting.Indented) => JsonConvert.SerializeObject(obj, formatting);
    }
}
