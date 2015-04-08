using System.Collections.Generic;
using FluentValidation.Results;

namespace ValidationTest
{
	public interface IValidatable
	{
		bool IsValid { get; set; }

		bool Validate (IEnumerable<IValidatable> validatables);

		IEnumerable<ValidationFailure> ValidationErrors { get; set; }
	}
}