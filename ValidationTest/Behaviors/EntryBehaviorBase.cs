using Xamarin.Forms;
using System.Linq;
using System;

namespace ValidationTest
{
	public class EntryBehaviorBase<T> : Behavior<Entry> where T:class
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly ("IsValid", typeof(bool), typeof(T), false);
		static readonly BindablePropertyKey ErrorMessagePropertyKey = BindableProperty.CreateReadOnly ("ErrorMessage", typeof(string), typeof(T), string.Empty);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
		public static readonly BindableProperty ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;

		public event EventHandler<TextChangedEventArgs> HandleTextChanged = delegate { };

		public bool IsValid {
			get { return (bool)base.GetValue (IsValidProperty); }
			set { base.SetValue (IsValidPropertyKey, value); }
		}

		public string ErrorMessage {
			get { return (string)base.GetValue (ErrorMessageProperty); }
			set { base.SetValue (ErrorMessagePropertyKey, value); }
		}

		private Entry _entry;

		protected override void OnAttachedTo (Entry bindable)
		{
			_entry = bindable;
			bindable.BindingContextChanged += (sender, e) => BindingContext = _entry.BindingContext;
			bindable.TextChanged += HandleTextChanged;
		}

		protected override void OnDetachingFrom (Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
		}
	}
}