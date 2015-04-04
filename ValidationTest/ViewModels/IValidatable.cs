using System.ComponentModel;
using FluentValidation;
using System.Collections.Generic;
using FluentValidation.Results;
using PropertyChanged;
using System.Linq;
using System;

namespace ValidationTest
{
	public interface IValidatable
	{
		bool IsValid { get; set; }

		bool Validate (IEnumerable<IValidatable> validatables);

		IEnumerable<ValidationFailure> ValidationErrors { get; set; }
	}
	
}