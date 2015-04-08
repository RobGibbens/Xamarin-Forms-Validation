using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using PropertyChanged;

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

		public IEnumerable<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();

		public bool IsValid { get; set; }

		public virtual bool Validate (IEnumerable<IValidatable> validatables)
		{
			var isValid = true;
			if (_validator != null) {
				var validationResult = _validator.Validate (this);
				this.ValidationErrors = validationResult.Errors;
				isValid = validationResult.IsValid;
			}


			//Validate any child objects
			if (validatables != null && validatables.Any ()) {
				foreach (var validatable in validatables) {
					if (validatable != null) {
						validatable.Validate (null);
						isValid = isValid && validatable.IsValid;

						if (validatable.ValidationErrors != null) {
							this.ValidationErrors = this.ValidationErrors.Union (validatable.ValidationErrors);
						}
					}
				}
			}

			this.IsValid = isValid;
			return isValid;
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void OnPropertyChanged (string propertyName, object before, object after)
		{
			//Perform property validation
			var propertyChanged = PropertyChanged;
			if (propertyChanged != null && propertyName != "ValidationErrors") {
				propertyChanged (this, new PropertyChangedEventArgs (propertyName));

				if (propertyName != "IsValid") {
					Validate (null);
				}
			}
		}
	}
	
}