using System.ComponentModel;
using FluentValidation;
using System.Collections.Generic;
using FluentValidation.Results;
using PropertyChanged;
using System.Linq;
using System;

namespace ValidationTest
{
	[ImplementPropertyChanged]
	public class ViewModelBase<T> : INotifyPropertyChanged, IValidatable where T:class
	{
		readonly IValidator<T> _validator;

		public ViewModelBase (IValidator<T> validator)
		{
			_validator = validator;
		}

		public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
		public string ErrorMessage { get; set; }
		public bool IsValid { get; set; }

		public virtual bool Validate (IEnumerable<IValidatable> validatables)
		{
			var validationResult = _validator.Validate (this);
			this.ValidationErrors = validationResult.Errors;
			var isValid = validationResult.IsValid;

			if (validatables != null && validatables.Any ()) {
				foreach (var validatable in validatables) {
					validatable.Validate (null);
					isValid = isValid && validatable.IsValid;

					if (validatable.ValidationErrors != null) {
						this.ValidationErrors = this.ValidationErrors.Union (validatable.ValidationErrors);
					}
				}
			}

			this.IsValid = IsValid;

			return isValid;
		}

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