using FluentValidation;

namespace ValidationTest
{
	public class Repository : IRepository
	{
		readonly IValidator<InstructorModel> _instructorValidator;
		readonly IValidator<ClassModel> _classValidator;

		public Repository (IValidator<InstructorModel> instructorValidator, IValidator<ClassModel> classValidator)
		{
			_classValidator = classValidator;
			_instructorValidator = instructorValidator;
		}

		public InstructorModel GetInstructor ()
		{
			return new InstructorModel (_instructorValidator);
		}

		public ClassModel GetClass ()
		{
			return new ClassModel (_classValidator);
		}

		public void Save()
		{
			//Save Instructor
			//Save Class
		}
	}	
}