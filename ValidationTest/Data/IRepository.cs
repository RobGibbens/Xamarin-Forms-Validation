namespace ValidationTest
{
	public interface IRepository
	{
		InstructorModel GetInstructor ();
		ClassModel GetClass();
		void Save();
	}
}