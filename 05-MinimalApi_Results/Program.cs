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
	if(employee is null || employee.Id <= 0)
	{
		return TypedResults.BadRequest("Employee is not provided or is not valid.");
	}

	EmployeesRepository.AddEmployee(employee);
	return Results.Ok("Employee added successfully.");
}).WithParameterValidation();

app.Run();
