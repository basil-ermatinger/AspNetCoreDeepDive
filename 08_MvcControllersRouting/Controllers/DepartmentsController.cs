namespace _08_MvcControllersRouting.Controllers
{
	public class DepartmentsController
	{
		public string GetDepartments()
		{
			return "These are the departments";
		}

		public string GetDepartmentById(int id)
		{
			return $"Department info: {id}";
		}
	}
}
