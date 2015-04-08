using System;
using System.Linq;

namespace ValidationTest
{
	public class MainViewModelAgeAcceptanceBehavior : PickerBehaviorBase<MainViewModelAgeAcceptanceBehavior>
	{
		public MainViewModelAgeAcceptanceBehavior () : base()
		{
		    //this.IsValid = true;
			this.HandleSelectedIndexChanged += Validate;
		}

		public void Validate (object sender, EventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
		    if (viewModel != null)
		    {
		        if (!viewModel.IsValid && viewModel.ValidationErrors.Any(x => x.PropertyName == "IsAgreementAccepted"))
		        {
		            //IsAgreementAccepted is invalid
		            this.ErrorMessage = viewModel
                                            .ValidationErrors
                                            .FirstOrDefault(x => x.PropertyName == "IsAgreementAccepted")
                                            .ErrorMessage;
		            IsValid = false;
		        }
		        else
		        {
		            IsValid = true;
		            this.ErrorMessage = string.Empty;
		        }
		    }
		    else
		    {
		        this.IsValid = true;
		    }
		}
	}
}