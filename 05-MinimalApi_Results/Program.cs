using _05_MinimalApi_Results.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/employees", () =>
{
	List<Employee> employees = EmployeesRepository.GetEmployees();

	return TypedResults.Ok(employees);
});

app.MapPost("/employees", (Employee employee) =>
{
	if(employee.Name == null) 
	{
		return Results.ValidationProblem(new Dictionary<string, string[]>
		{
			{ "position", new[] { "Name is not set" } }
		});
	}

		EmployeesRepository.AddEmployee(employee);
	return TypedResults.Created($"/employees/{employee.Id}", employee);
}).WithParameterValidation();

app.Run();
