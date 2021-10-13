using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Sub_6.Converters
{
    class NullBindingConverter : IValueConverter
    {
        //basically. If the thing that a binding wants to retrieve doesn't exist (isn't loaded yet) and the value is null,
        //in stead of throwing an exception it simply unsets the value of the binding (temporarily).
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // According to https://msdn.microsoft.com/en-us/library/system.windows.data.ivalueconverter.convertback(v=vs.110).aspx#Anchor_1
            // if you do not support a conversion back you should return a Binding.DoNothing or a DependencyProperty.UnsetValue
            return Binding.DoNothing;
            // Original code:
            // throw new NotImplementedException();

            // (since we have onpropertychanged we don't need conversion back, it already updates)
        }
    }
}
