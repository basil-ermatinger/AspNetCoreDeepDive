using Microsoft.AspNetCore.Mvc;

namespace _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Models
{
	public class GetEmployeeParameters
	{
		[FromRoute]
		public int Id { get; set; }

		[FromQuery]
		public string Name { get; set; }

		[FromHeader]
		public string Position { get; set; }
	}
}
