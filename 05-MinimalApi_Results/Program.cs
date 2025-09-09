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
	if(employee is null)
	{
		return Results.BadRequest("Employee is not provided or is not valid.");
	}

	EmployeesRepository.AddEmployee(employee);
	return TypedResults.Created($"/employees/{employee.Id}", employee);
}).WithParameterValidation();

app.Run();
