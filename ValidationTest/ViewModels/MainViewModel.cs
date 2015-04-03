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

		public MainViewModel (IValidator<MainViewModel> validator, 
					InstructorModel instructorModel,
					ClassModel classModel) : base (validator)
		{
			this.Instructor = instructorModel;
			this.Class = classModel;

			this.Save = new DelegateCommand (OnSave, ValidateAll);

			this.PropertyChanged += (s,e) => this.Save.RaiseCanExecuteChanged ();

			OnLoadInstructor ();
		}

		public InstructorModel Instructor { get; set; }
		public ClassModel Class {get;set;}

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
			var isValid = Validate () && ValidateModels ();
			return isValid;
		}

		private bool ValidateModels ()
		{
			return Instructor.Validate() && Class.Validate();
		}

		public void OnLoadInstructor ()
		{
			Instructor.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();
			Class.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();
		}

		public void OnSave ()
		{
			var x = "";
		}
	}
}