using Xamarin.Forms;
using System.Linq;

namespace ValidationTest
{
	public partial class MainPage : ContentPage
	{
	    readonly MainViewModel _viewModel;
		public MainPage ()
		{
			var viewModelValidator = new MainViewModelValidator ();

			//This really should be an Dependency Injection container
			var repository = new Repository (new InstructorModelValidator(), new ClassModelValidator());

			_viewModel = new MainViewModel (viewModelValidator, repository);

			this.BindingContext = _viewModel;
			InitializeComponent ();
		}

		void OnShowErrors (object sender, System.EventArgs e)
		{
			var message = "No errors";
			_viewModel.Validate ();
			if (_viewModel.ValidationErrors.Any ()) {
				message = _viewModel
								.ValidationErrors
								.Select (x => x.ErrorMessage)
								.Aggregate ((current, next) => current + ", " + next);
			}
			this.DisplayAlert("Errors", message, "OK");
		}
	}
}