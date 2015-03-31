using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace ValidationTest
{
    /// <summary>
    /// This simple bindable property allows the Picker collection choices
    /// to be data bound. The current implementation of Picker does not use
    /// a Bindable Property, and therefore cannot be data bound without this.
    /// </summary>
    public class PickerCollection
    {
        public static BindableProperty ItemsProperty = 
            BindableProperty.CreateAttached<PickerCollection, IEnumerable<string>>(
                bindable => PickerCollection.GetItems(bindable),
                null, /* default value */
                BindingMode.OneWay,
                null,
                PickerCollection.ItemsChanged,
                null,
                null);

        public static IEnumerable<string> GetItems(BindableObject bo)
        {
            return (IEnumerable<string>)bo.GetValue(PickerCollection.ItemsProperty);
        }

        public static void SetItems(BindableObject bo, IEnumerable<string> value)
        {
            bo.SetValue(PickerCollection.ItemsProperty, value);
        }

        public static void ItemsChanged(BindableObject bindableObject, IEnumerable<string> oldValue, IEnumerable<string> newValue)
		{
			var picker = bindableObject as Picker;

			if (picker == null)
				return;
				
			try {
				picker.Items.Clear ();

				if (newValue == null)
					return;

				foreach (var item in newValue) {
					picker.Items.Add (item);
				}
			} catch (Exception ex) {
				Debug.WriteLine ("https://bugzilla.xamarin.com/show_bug.cgi?id=26528");
				Debug.WriteLine (ex.StackTrace);
			}
		}
    }
}

