using Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Validation
{
	public class RegistrationEnsureConfirmPassword : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			RegistrationWithValidationClasses? registration = validationContext.ObjectInstance as RegistrationWithValidationClasses;
			string? password = registration?.Password;
			string? confirmPassword = registration?.ConfirmPassword;

			if(confirmPassword is not null && confirmPassword != password)
			{
				return new ValidationResult("Confirm password does not match password!");
			}

			return ValidationResult.Success;
		}
	}
}
