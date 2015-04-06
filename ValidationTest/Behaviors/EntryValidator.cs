using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public class EntryValidator : EntryBehaviorBase<EntryValidator>
	{
		readonly string _propertyName;

		public EntryValidator (string propertyName) : base ()
		{
			_propertyName = propertyName;
			this.HandleTextChanged += OnTextChanged;
		}

		void OnTextChanged (object sender, TextChangedEventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
			if (viewModel != null) {
				if (!viewModel.Validate () && viewModel.ValidationErrors.Any (x => x.PropertyName == _propertyName)) {
					//Email is invalid
					IsValid = false;
				} else {
					IsValid = true;
				}
			}
		}
	}
}