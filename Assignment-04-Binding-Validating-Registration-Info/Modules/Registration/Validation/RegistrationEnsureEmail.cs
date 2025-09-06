using Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Validation
{
	public class RegistrationEnsureEmail : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			RegistrationWithValidationClasses? registration = validationContext.ObjectInstance as RegistrationWithValidationClasses;
			string? email = registration?.Email;

			if(email is not null)
			{
				string trimmedEmail = email.Trim();

				if(trimmedEmail.EndsWith("."))
				{
					return new ValidationResult("Email address is not valid");
				}

				try
				{
					_ = new MailAddress(email);
					return ValidationResult.Success;
				}
				catch
				{
					return new ValidationResult("Email address is not valid");
				}
			}

			return new ValidationResult("Email address is not set");
		}
	}
}
