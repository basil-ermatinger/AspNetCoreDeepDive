using System.ComponentModel.DataAnnotations;
using Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Validation;
using Microsoft.Extensions.Primitives;

namespace Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Models
{
	public class RegistrationWithValidationClasses
	{
		[Required]
		[RegistrationEnsureEmail]
		public required string Email { get; set; }

		[Required]
		[RegistrationEnsurePassword]
		public required string Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[RegistrationEnsureConfirmPassword]
		public required string ConfirmPassword { get; set; }
	}
}
