using _06_MinimalApi_ModelValidation.Modules.Employees.Models;
using _06_MinimalApi_ModelValidation.Modules.Employees.Repositories;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Model validation
// e.g. /employees and define body with at least Name and Salary in Range 50000 to 200000:
// {
//		"Name": "Roberto Blanko",
//    "Salary": 50000
// }
// if position is "Manager" the "Salary" has to be greater or equal to 100000:
// {
//  	"Id": 11,
//    "Name": "Roberto Blanko",
//    "Position": "Manager",
//    "Salary": 150000
// }
app.MapPost("/employees", (Employee employee) =>
{
	EmployeeRepository.AddEmployee(employee);
	return "Employee is added successfully.";
}).WithParameterValidation();

app.Run();
