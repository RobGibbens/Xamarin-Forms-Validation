using Xamarin.Forms;

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
	}
}