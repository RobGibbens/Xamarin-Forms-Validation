using FluentValidation;
using System;

namespace ValidationTest
{
	public class ClassModelValidator : AbstractValidator<ClassModel>
	{
		public ClassModelValidator ()
		{
			RuleFor (x => x.Title).NotEmpty ();
			//RuleFor (x => x.ScheduledDate).GreaterThan(DateTime.Now.AddMinutes(-30));
		}
	}
}