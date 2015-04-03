using FluentValidation;
using PropertyChanged;
using System.Linq;
using System.Collections.Generic;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class MainViewModel : ViewModelBase<MainViewModel>
	{
		public DelegateCommand Save { get; private set; }

		public MainViewModel (IValidator<MainViewModel> validator, IRepository repository) : base (validator)
		{
			this.Save = new DelegateCommand (OnSave, ValidateAll);

			this.Instructor = repository.GetInstructor ();
			this.Class = repository.GetClass ();

			this.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();
			Instructor.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();
			Class.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();

			this.Save.RaiseCanExecuteChanged ();
		}

		public InstructorModel Instructor { get; private set; }
		public ClassModel Class { get; private set; }

		public int Age { get; set; }
		public bool IsAgreementAccepted { get; set; }
		public string EmailAddress { get; set; }

		public IEnumerable<string> AgeChoices {
			get {
				return Enumerable.Range (1, 100).Select (x => x.ToString ()).ToList ();
			}
		}

		private bool ValidateAll ()
		{
			var isViewModelValid = this.Validate ();
			var areModelsValid = ValidateModels ();

			var isValid = isViewModelValid && areModelsValid;
			return isValid;
		}

		private bool ValidateModels ()
		{
			return Instructor.Validate () && Class.Validate ();
		}

		public void OnSave ()
		{
			var x = "";
		}
	}
}