using Microsoft.AspNetCore.Mvc;

namespace _08_MvcControllersRouting.Controllers
{
	[Route("/api")]
	public class DepartmentsController
	{
		[HttpGet("departments")]
		public string GetDepartments()
		{
			return "These are the departments";
		}

		[HttpGet]
		[Route("departments/{id}")]
		public string GetDepartmentById(int id)
		{
			return $"Department info: {id}";
		}
	}
}

































































