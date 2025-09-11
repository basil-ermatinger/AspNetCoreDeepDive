using _05_MinimalApi_Results.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.MapGet("/", () => "Hello World!");

app.MapGet("/employees", () =>
{
	List<Employee> employees = EmployeesRepository.GetEmployees();

	return TypedResults.Ok(employees);
});

app.MapPost("/employees", (Employee employee) =>
{
	if(employee is null || employee.Id < 0) 
	{
		return Results.ValidationProblem(new Dictionary<string, string[]>
		{
			{ "id", new[] { "Employee is not provided or is not valid." } }
		});
	}

		EmployeesRepository.AddEmployee(employee);
	return TypedResults.Created($"/employees/{employee.Id}", employee);
}).WithParameterValidation();

app.Run();
