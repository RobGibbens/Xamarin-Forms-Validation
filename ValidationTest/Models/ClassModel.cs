using FluentValidation;
using PropertyChanged;
using System;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class ClassModel : ViewModelBase<ClassModel>
	{
		public ClassModel (IValidator<ClassModel> validator) : base (validator) {}
		public string Title { get; set; }
		public DateTime ScheduledDate { get; set; }
	}
}