using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public class MainViewModelEmailBehavior : EntryBehaviorBase<MainViewModelEmailBehavior>
	{
		public MainViewModelEmailBehavior () : base ()
		{
			this.HandleTextChanged += OnTextChanged;
		}

		void OnTextChanged (object sender, TextChangedEventArgs e)
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
	}
}