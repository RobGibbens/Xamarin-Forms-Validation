using Xamarin.Forms;

namespace ValidationTest
{
	public partial class MainPage : ContentPage
	{
	    readonly MainViewModel _viewModel;
		public MainPage ()
		{
			var validator = new MainViewModelValidator ();
			var instructorModelValidator = new InstructorModelValidator ();

			_viewModel = new MainViewModel (validator, instructorModelValidator);
			this.BindingContext = _viewModel;
			InitializeComponent ();
		}
	}
}