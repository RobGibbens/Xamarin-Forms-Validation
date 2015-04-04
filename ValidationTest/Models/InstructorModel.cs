using FluentValidation;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class InstructorModel : ViewModelBase<InstructorModel>
	{
		public InstructorModel (IValidator<InstructorModel> validator) : base (validator) {}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ObservableCollection<ClassModel> Classes { get; set; } = new ObservableCollection<ClassModel> ();
	}
}