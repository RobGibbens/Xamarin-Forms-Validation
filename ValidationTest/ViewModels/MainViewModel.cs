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

		public DelegateCommand LoadInstructor { get; private set; }

		public InstructorModel Instructor { get; set; }

		IValidator<InstructorModel> _instructorValidator;

		public MainViewModel (IValidator<MainViewModel> validator, IValidator<InstructorModel> instructorValidator) : base (validator)
		{
			_instructorValidator = instructorValidator;
			this.Save = new DelegateCommand (OnSave, ValidateAll);
			this.LoadInstructor = new DelegateCommand (OnLoadInstructor);

			this.PropertyChanged += (sender, e) => this.Save.RaiseCanExecuteChanged ();

			//OnLoadInstructor ();
		}

		private bool ValidateAll ()
		{
			var isValid = Validate () && ValidateModels ();
			return isValid;
		}

		private bool ValidateModels ()
		{
			return _instructorValidator == null || _instructorValidator.Validate (this.Instructor).IsValid;
		}

		public void OnLoadInstructor ()
		{
			Instructor = new InstructorModel (_instructorValidator);
			Instructor.PropertyChanged += (sender, e) => this.Save.RaiseCanExecuteChanged ();
		}

		public void OnSave ()
		{
			var x = "";
		}

		public int Age { get; set; }

		public bool IsAgreementAccepted { get; set; }

		public string EmailAddress { get; set; }

		public IEnumerable<string> AgeChoices {
			get {
				return Enumerable.Range (1, 100).Select (x => x.ToString ()).ToList ();
			}
		}

	}
}