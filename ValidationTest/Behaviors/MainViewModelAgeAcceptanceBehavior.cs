using Xamarin.Forms;
using System.Linq;
using System;

namespace ValidationTest
{
	public class MainViewModelAgeAcceptanceBehavior : PickerBehaviorBase<MainViewModelAgeAcceptanceBehavior>
	{
		public MainViewModelAgeAcceptanceBehavior () : base() {
			this.HandleSelectedIndexChanged += OnSelectedIndexChanged;
		}

		void OnSelectedIndexChanged (object sender, EventArgs e)
		{
			var viewModel = this.BindingContext as MainViewModel;
			if (viewModel != null) {
				if (!viewModel.Validate () && viewModel.ValidationErrors.Any (x => x.PropertyName == "IsAgreementAccepted")) {
					//IsAgreementAccepted is invalid
					this.ErrorMessage = viewModel.ValidationErrors.FirstOrDefault (x => x.PropertyName == "IsAgreementAccepted").ErrorMessage;
					IsValid = false;
				} else {
					IsValid = true;
					this.ErrorMessage = string.Empty;
				}
			}
		}
	}
}