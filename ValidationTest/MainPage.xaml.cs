using Xamarin.Forms;

namespace ValidationTest
{
	public partial class MainPage : ContentPage
	{
	    readonly MainViewModel _viewModel;
		public MainPage ()
		{
			var validator = new MainViewModelValidator ();

			var instructor = new InstructorModel (new InstructorModelValidator ());
			var classModel = new ClassModel (new ClassModelValidator ());

			_viewModel = new MainViewModel (validator, instructor, classModel);
			this.BindingContext = _viewModel;
			InitializeComponent ();
		}
	}
}