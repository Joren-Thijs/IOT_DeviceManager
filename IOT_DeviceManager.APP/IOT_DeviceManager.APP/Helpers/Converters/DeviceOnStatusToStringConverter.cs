using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IOT_DeviceManager.APP.Helpers.Converters
{
    public class DeviceOnStatusToStringConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsString = value.ToString();
            switch (valueAsString)
            {
                case ("True"):
                {
                    return "On";
                }
                case ("False"):
                {
                    return "Off";
                }
                default:
                {
                    return "Off";
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
