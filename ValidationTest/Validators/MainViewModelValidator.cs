using FluentValidation;

namespace ValidationTest
{
	public class MainViewModelValidator : AbstractValidator<MainViewModel>
	{
		public MainViewModelValidator ()
		{
			RuleFor (x => x.EmailAddress).EmailAddress ();
			RuleFor (x => x.IsAgreementAccepted).Equal (true).When (x => x.Age >= 18).WithMessage("Users over the age of 18 must accept the agreement");
		}
	}
}