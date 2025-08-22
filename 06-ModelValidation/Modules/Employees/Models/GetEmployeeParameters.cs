using Microsoft.AspNetCore.Mvc;

namespace _06_MinimalApi_ModelValidation.Modules.Employees.Models
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
