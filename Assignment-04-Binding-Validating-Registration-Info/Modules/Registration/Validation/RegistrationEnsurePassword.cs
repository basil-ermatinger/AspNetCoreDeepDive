using Assignment_04_Binding_Validating_Registration_Info.Modules.Employees.Models;
using Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Validation
{
	public class RegistrationEnsurePassword : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			RegistrationWithValidationClasses? loginDetails = validationContext.ObjectInstance as RegistrationWithValidationClasses;
			string? password = loginDetails?.Password;

			if(password is not null && password.Length < 6)
			{
				return new ValidationResult("Password must be at least 6 characters long");
			}

			return ValidationResult.Success;
		}
	}
}
