using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public class MainViewModelEmailBehavior : Behavior<Entry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly ("IsValid", typeof(bool), typeof(MainViewModelEmailBehavior), false);
		static readonly BindablePropertyKey ErrorMessagePropertyKey = BindableProperty.CreateReadOnly ("ErrorMessage", typeof(string), typeof(MainViewModelEmailBehavior), string.Empty);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
		public static readonly BindableProperty ErrorMessageProperty = ErrorMessagePropertyKey.BindableProperty;

		public bool IsValid {
			get { return (bool)base.GetValue (IsValidProperty); }
			private set { base.SetValue (IsValidPropertyKey, value); }
		}

		public string ErrorMessage {
			get { return (string)base.GetValue (ErrorMessageProperty); }
			private set { base.SetValue (ErrorMessagePropertyKey, value); }
		}

		private Entry _entry;

		protected override void OnAttachedTo (Entry bindable)
		{
			_entry = bindable;
			bindable.BindingContextChanged += (sender, e) => BindingContext = _entry.BindingContext;
			bindable.TextChanged += HandleTextChanged;
		}

		void HandleTextChanged (object sender, TextChangedEventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
			if (viewModel != null) {
				if (!viewModel.Validate () && viewModel.ValidationErrors.Any (x => x.PropertyName == "EmailAddress")) {
					//Email is invalid
					this.ErrorMessage = viewModel.ValidationErrors.FirstOrDefault (x => x.PropertyName == "EmailAddress").ErrorMessage;
					IsValid = false;
				} else {
					IsValid = true;
					this.ErrorMessage = string.Empty;
				}
		
				((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
			}
		}

		protected override void OnDetachingFrom (Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
		}
	}
}