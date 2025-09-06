using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Primitives;

namespace Assignment_04_Binding_Validating_Registration_Info.Modules.Employees.Models
{
	public class Registration
	{
		[Required]
		[EmailAddress (ErrorMessage = "Invalid email address")]
		public required string Email { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long!")]
		public required string Password { get; set; }

		[Required (ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "Password do not match!")]
		public required string ConfirmPassword { get; set; }

		public static ValueTask<Registration?> BindAsync(HttpContext context)
		{
			StringValues email = context.Request.Query["email"];
			StringValues password = context.Request.Query["pwd1"];
			StringValues confirmPassword = context.Request.Query["pwd2"];

			return new ValueTask<Registration?>(new Registration { Email = email, Password = password, ConfirmPassword = confirmPassword });
		}
	}
}
