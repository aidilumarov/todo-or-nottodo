using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ToDoOrNotToDo.Converters
{
    public class ItemStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value
                ? (Color)Application.Current.Resources["ItemCompletedColor"]
                : (Color)Application.Current.Resources["ItemActiveColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
