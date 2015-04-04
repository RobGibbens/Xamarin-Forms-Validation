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

		readonly IRepository _repository;
		public MainViewModel (IValidator<MainViewModel> validator, IRepository repository) : base (validator)
		{
			this.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();

			_repository = repository;

			this.Save = new DelegateCommand (OnSave, this.Validate);

			this.Instructor = _repository.GetInstructor ();
			this.Class = _repository.GetClass ();

			Instructor.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();
			Class.PropertyChanged += (s, e) => this.Save.RaiseCanExecuteChanged ();

			//Kick off the inital validation for the screen
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

		public bool Validate ()
		{
			var validators = new List<IValidatable> { Instructor, Class };
			var isValid = base.Validate (validators);
			return isValid;
		}

		public void OnSave ()
		{
			_repository.Save ();
		}
	}
}