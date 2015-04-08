using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public class MainViewModelEmailBehavior : EntryBehaviorBase<MainViewModelEmailBehavior>
	{
		public MainViewModelEmailBehavior () : base ()
		{
			this.HandleTextChanged += Validate;
		}

		public void Validate (object sender, TextChangedEventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
		    if (viewModel != null)
		    {
		        if (!viewModel.IsValid && viewModel.ValidationErrors.Any(x => x.PropertyName == "EmailAddress"))
		        {
		            //Email is invalid
		            this.ErrorMessage =
		                viewModel.ValidationErrors.FirstOrDefault(x => x.PropertyName == "EmailAddress").ErrorMessage;
		            this.IsValid = false;
		        }
		        else
		        {
		            this.IsValid = true;
		            this.ErrorMessage = string.Empty;
		        }
		    }
		    else
		    {
		        this.IsValid = true;
		    }

            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.White;
            ((Entry)sender).BackgroundColor = IsValid ? Color.Default : Color.Red;
        }
	}
}