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
			this.HandleTextChanged += Validate;
		}

		public void Validate (object sender, TextChangedEventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
		    if (viewModel != null)
		    {
		        if (!viewModel.IsValid && viewModel.ValidationErrors.Any(x => x.PropertyName == _propertyName))
		        {
		            //Email is invalid
		            this.IsValid = false;
		        }
		        else
		        {
		            this.IsValid = true;
		        }
		    }
		    else
		    {
		        this.IsValid = true;
		    }
		}
	}
}