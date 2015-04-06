using FluentValidation;

namespace ValidationTest
{
	public class InstructorModelValidator : AbstractValidator<InstructorModel>
	{
		public InstructorModelValidator ()
		{
			RuleFor (x => x.FirstName).NotEmpty ().Length(2, 100);
			RuleFor (x => x.LastName).NotEmpty ().Length(2, 100);
		}
	}
}