using _06_MinimalApi_ModelValidation.Modules.Employees.Models;
using _06_MinimalApi_ModelValidation.Modules.Employees.Repositories;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/employees", (Employee employee) =>
{
	EmployeeRepository.AddEmployee(employee);
	return "Employee is added successfully.";
}).WithParameterValidation();

app.Run();
