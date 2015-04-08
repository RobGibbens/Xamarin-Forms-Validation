using NUnit.Framework;
using ValidationTest;
using Moq;
using Should;
using Ploeh.AutoFixture;

namespace ValidationTests.Tests.Unit
{
	[TestFixture ()]
	public class MainViewModelTests
	{
		[Test]
		public void ViewModel_must_be_invalid_when_created()
		{
			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();
			var vm = new MainViewModel (mainViewModelValidator, repository.Object);

			vm.IsValid.ShouldBeFalse ();
		}

		[Test]
		public void Agreement_can_be_false_when_age_is_less_than_18 ()
		{
			var fixture = new Fixture();

			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();
			var vm = new MainViewModel (mainViewModelValidator, repository.Object);

			fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 17));

			vm.Age = fixture.Create<int> ();
			vm.IsAgreementAccepted = false;
			vm.EmailAddress = fixture.Create<string>() + "@foobar.com";

			vm.IsValid.ShouldBeTrue ();
		}

		[Test]
		public void Agreement_must_be_true_when_age_is_greater_than_18 ()
		{
			var fixture = new Fixture();

			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();
			var vm = new MainViewModel (mainViewModelValidator, repository.Object);

			fixture.Customizations.Add(new RandomNumericSequenceGenerator(18, 100));

			vm.Age = fixture.Create<int> ();
			vm.IsAgreementAccepted = false;
			vm.EmailAddress = fixture.Create<string>() + "@foobar.com";

			vm.IsValid.ShouldBeFalse ();

			vm.IsAgreementAccepted = true;

			vm.IsValid.ShouldBeTrue ();
		}

		[Test]
		public void Email_must_be_a_valid_address_format ()
		{
			var fixture = new Fixture();

			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();
			var vm = new MainViewModel (mainViewModelValidator, repository.Object);

			fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 17));

			vm.Age = fixture.Create<int> ();
			vm.IsAgreementAccepted = false;
			vm.EmailAddress = fixture.Create<string>();

			vm.IsValid.ShouldBeFalse ();
		}

		[Test]
		public void ViewModel_must_save_models_when_valid()
		{
			var fixture = new Fixture();

			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();

			var vm = new MainViewModel (mainViewModelValidator, repository.Object);
			fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 17));

			vm.Age = fixture.Create<int> ();
			vm.IsAgreementAccepted = true;
			vm.EmailAddress = fixture.Create<string>() + "@foobar.com";

			vm.Save.Execute (null);

			repository.Verify (x => x.Save (), Times.Once);
		}

		[Test]
		public void ViewModel_must_not_save_models_when_invalid()
		{
			var mainViewModelValidator = new MainViewModelValidator ();

			var repository = new Mock<IRepository> ();

			var vm = new MainViewModel (mainViewModelValidator, repository.Object);

			vm.Save.Execute (null);

			repository.Verify (x => x.Save (), Times.Never);
		}
	}


}