using FluentValidation;
using PropertyChanged;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class InstructorModel : ViewModelBase<InstructorModel>
	{
		public InstructorModel (IValidator<InstructorModel> validator) : base (validator)
		{
			
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
	
}