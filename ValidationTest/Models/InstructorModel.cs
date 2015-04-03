using FluentValidation;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class InstructorModel : ViewModelBase<InstructorModel>
	{
		public InstructorModel (IValidator<InstructorModel> validator) : base (validator)
		{
			this.Classes = new ObservableCollection<ClassModel> ();
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ObservableCollection<ClassModel> Classes { get; set; }
	}
}