using Xamarin.Forms;
using System.Linq;
using System;

namespace ValidationTest
{
	public class LabelBehaviorBase<T> : Behavior<Label> where T:class
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly ("IsValid", typeof(bool), typeof(T), false);
		static readonly BindablePropertyKey ErrorMessagePropertyKey = BindableProperty.CreateReadOnly ("ErrorMessage", typeof(string), typeof(T), string.Empty);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
		public static readonly BindableProperty ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;

		public bool IsValid {
			get { return (bool)base.GetValue (IsValidProperty); }
			set { base.SetValue (IsValidPropertyKey, value); }
		}

		public string ErrorMessage {
			get { return (string)base.GetValue (ErrorMessageProperty); }
			set { base.SetValue (ErrorMessagePropertyKey, value); }
		}

		private Label _label;

		protected override void OnAttachedTo (Label bindable)
		{
			_label = bindable;
			bindable.BindingContextChanged += (sender, e) => BindingContext = _label.BindingContext;
		}

		protected override void OnDetachingFrom (Label bindable)
		{
		}
	}
	
}