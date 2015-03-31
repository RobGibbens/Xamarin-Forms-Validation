using System.ComponentModel;
using FluentValidation;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Windows.Input;
//using PropertyChanged;
using System.Linq.Expressions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ValidationTest
{
	//[ImplementPropertyChanged]
	public class ViewModelBase<T> : INotifyPropertyChanged where T:class
	{
		readonly IValidator<T> _validator;

		public ViewModelBase (IValidator<T> validator)
		{
			_validator = validator;
		}

		public IEnumerable<ValidationFailure> ValidationErrors { get; private set; }

		string _errorMessage;
		public string ErrorMessage {
			get {
				return _errorMessage;
			}
			set {
				if (_errorMessage != value) {
					_errorMessage = value;
					RaisePropertyChanged ();
				}
			}
		}

		public bool Validate ()
		{
			var validationResult = _validator.Validate (this);
			this.ValidationErrors = validationResult.Errors;
			this.IsValid = validationResult.IsValid;
			return validationResult.IsValid;
		}

		bool _isValid;
		public bool IsValid {
			get {
				return _isValid;
			}
			set {
				if (_isValid != value) {
					_isValid = value;
					RaisePropertyChanged ();
				}
			}
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

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propExpr)
		{
			var prop = (PropertyInfo)((MemberExpression)propExpr.Body).Member;
			this.RaisePropertyChanged(prop.Name);
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName= "")
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
}