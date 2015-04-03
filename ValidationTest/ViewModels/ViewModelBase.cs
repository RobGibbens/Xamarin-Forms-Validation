using System.ComponentModel;
using FluentValidation;
using System.Collections.Generic;
using FluentValidation.Results;
using PropertyChanged;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class ViewModelBase<T> : INotifyPropertyChanged where T:class
	{
		readonly IValidator<T> _validator;

		public ViewModelBase (IValidator<T> validator)
		{
			_validator = validator;
		}

		public IEnumerable<ValidationFailure> ValidationErrors { get; private set; }

		public string ErrorMessage { get; set; }

		public bool Validate ()
		{
			var validationResult = _validator.Validate (this);
			this.ValidationErrors = validationResult.Errors;
			this.IsValid = validationResult.IsValid;
			return validationResult.IsValid;
		}

		public bool IsValid { get; set; }

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void OnPropertyChanged (string propertyName, object before, object after)
		{
			//Perform property validation
			var propertyChanged = PropertyChanged;
			if (propertyChanged != null && propertyName != "ValidationErrors") {
				propertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
	
}