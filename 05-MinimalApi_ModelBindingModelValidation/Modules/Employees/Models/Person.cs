namespace _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		public static ValueTask<Person?> BindAsync(HttpContext context)
		{
			string idStr = context.Request.Query["id"];
			string nameStr = context.Request.Headers["name"];

			if(int.TryParse(idStr, out var id))
			{
				return new ValueTask<Person?>(new Person { Id = id, Name = nameStr });
			}

			return new ValueTask<Person?>(Task.FromResult<Person?>(null));
		}
	}
}
