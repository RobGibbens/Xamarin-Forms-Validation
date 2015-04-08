using System;
using Xamarin.Forms;

namespace ValidationTest
{
	public class PickerBehaviorBase<T> : Behavior<Picker> where T:class
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly ("IsValid", typeof(bool), typeof(T), false);
		static readonly BindablePropertyKey ErrorMessagePropertyKey = BindableProperty.CreateReadOnly ("ErrorMessage", typeof(string), typeof(T), string.Empty);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
		public static readonly BindableProperty ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;

		public event EventHandler HandleSelectedIndexChanged = delegate { };


		public bool IsValid {
			get { return (bool)base.GetValue (IsValidProperty); }
			set { base.SetValue (IsValidPropertyKey, value); }
		}

		public string ErrorMessage {
			get { return (string)base.GetValue (ErrorMessageProperty); }
			set { base.SetValue (ErrorMessagePropertyKey, value); }
		}

		private Picker _picker;

		protected override void OnAttachedTo (Picker bindable)
		{
			_picker = bindable;
			bindable.BindingContextChanged += (sender, e) => BindingContext = _picker.BindingContext;
			bindable.SelectedIndexChanged += HandleSelectedIndexChanged;
		}

		protected override void OnDetachingFrom (Picker bindable)
		{
			bindable.SelectedIndexChanged -= HandleSelectedIndexChanged;
		}
	}

}