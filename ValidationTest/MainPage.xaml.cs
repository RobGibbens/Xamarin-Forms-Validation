using Xamarin.Forms;

namespace ValidationTest
{
	public partial class MainPage : ContentPage
	{
		MainViewModel _viewModel;
		public MainPage ()
		{
			var validator = new MainViewModelValidator ();
			_viewModel = new MainViewModel (validator);
			this.BindingContext = _viewModel;
			InitializeComponent ();
		}
	}
}