using _06_MinimalApi_ModelValidation.Modules.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace _06_ModelValidation.Modules.Employees.Validation
{
	public class EmployeeEnsureSalary : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			Employee? employee = validationContext.ObjectInstance as Employee;

			if(employee is not null && !string.IsNullOrWhiteSpace(employee.Position) && employee.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
			{
				Console.WriteLine("Test");
				if(employee.Salary < 100000)
				{
					return new ValidationResult("A manager's salary has to be greater or equal to $100'000");
				}
			}

			Console.WriteLine("Otherwise");

			return ValidationResult.Success;
		}
	}
}
