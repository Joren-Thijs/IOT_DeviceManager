using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.Helpers.Reflection
{
    public class PropertyHelpers
    {
        public static Expression<Func<T, object>> GetPropertySelector<T>(string propertyName)
        {
            var arg = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(object));
            var exp = Expression.Lambda<Func<T, object>>(conv, arg);
            return exp;
        }
    }
}
