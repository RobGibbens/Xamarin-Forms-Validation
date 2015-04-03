using Xamarin.Forms;
using FluentValidation;

namespace ValidationTest
{
	public interface IRepository
	{
		InstructorModel GetInstructor ();
		ClassModel GetClass();
	}
	
}