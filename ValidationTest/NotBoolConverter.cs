using System;
using Xamarin.Forms;

namespace ValidationTest
{
	public class NotBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool && (bool)value)
				return false;
			else
				return true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool && (bool)value)
				return false;
			else
				return true;
		}
	}
}