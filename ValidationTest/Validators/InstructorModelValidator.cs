using FluentValidation;

namespace ValidationTest
{
	public class InstructorModelValidator : AbstractValidator<InstructorModel>
	{
		public InstructorModelValidator ()
		{
			RuleFor (x => x.FirstName).NotEmpty ();
			RuleFor (x => x.LastName).NotEmpty ();
		}
	}
	
}