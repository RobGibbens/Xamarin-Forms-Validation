using Xamarin.Forms;
using FluentValidation;

namespace ValidationTest
{
	public partial class MainPage : ContentPage
	{
	    readonly MainViewModel _viewModel;
		public MainPage ()
		{
			var validator = new MainViewModelValidator ();

			var repository = new Repository (new InstructorModelValidator(), new ClassModelValidator());

			_viewModel = new MainViewModel (validator, repository);
			this.BindingContext = _viewModel;
			InitializeComponent ();
		}
	}
}