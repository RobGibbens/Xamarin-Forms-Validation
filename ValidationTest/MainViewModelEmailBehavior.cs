using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public class MainViewModelEmailBehavior : Behavior<Entry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly ("IsValid", typeof(bool), typeof(MainViewModelEmailBehavior), false);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid {
			get { return (bool)base.GetValue (IsValidProperty); }
			private set { base.SetValue (IsValidPropertyKey, value); }
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
				if (!viewModel.Validate ()) {
					IsValid = false;
					if (viewModel.ValidationErrors.Any (x => x.PropertyName == "EmailAddress")) {
						//Email is invalid
						viewModel.ErrorMessage = viewModel.ValidationErrors.FirstOrDefault(x => x.PropertyName == "EmailAddress").ErrorMessage;
					}
				} else {
					IsValid = true;
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