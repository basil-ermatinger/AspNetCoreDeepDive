using _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Handlers;
using _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

// Bind router parameters
// e.g. http://localhost:5074/employees/1
app.MapGet("/employees/{id:int?}", ([FromRoute] int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetEmployeeById(id.Value) : EmployeeRequestHandler.HandleGetEmployee(1);
});

// Bind Headers
// e.g. /employees/ (also specify header with "key = identity" and "value = 1" in Postman)
app.MapGet("/employees/", ([FromHeader(Name = "identity")] int id) =>
{
	return EmployeeRequestHandler.HandleGetEmployeeById(id);
});

// Bind route parameters with self declared name of parameter
// e.g. http://localhost:5074/employees/position/1
app.MapGet("/employees/position/{id}", ([FromRoute(Name = "id")] int? identityNumber) =>
{
	return identityNumber.HasValue ? EmployeeRequestHandler.HandleGetPositionById(identityNumber.Value) : null;
});

// Bind route parameters without using [FromRoute]
// e.g. http://localhost:5074/employees/salary/1
app.MapGet("/employees/salary/{id}", (int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetSalaryById(id.Value) : null;
});

// Bind parameters from query string
// e.g. http://localhost:5074/employees/name?id=1
app.MapGet("/employees/name", (int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetNameById(id.Value) : null;
});

// Bind parameters from query string with self declared name of parameter
// e.g. http://localhost:5074/employees/helloFromEmployee?id=1
app.MapGet("/employees/helloFromEmployee", ([FromQuery(Name = "id")] int? identityNumber) =>
{
	return identityNumber.HasValue ? EmployeeRequestHandler.HandleHelloFromEmployee(identityNumber.Value) : null;
});

// Bind multiple parameters as grouped parameters
// e.g. /employees/get-and-change/1?name=Basil (also specify header with "key = position" and "value = Developer" in Postman)
app.MapGet("/employees/get-and-change/{id:int}", ([AsParameters] GetEmployeeParameters param) =>
{
	Employee? employee = EmployeeRequestHandler.HandleGetEmployeeById(param.Id);

	employee.Name = param.Name;
	employee.Position = param.Position;

	return employee;
});

// Bind arrays to query parameters
// e.g. /employees/byIdsQuery?id=1&id=2
app.MapGet("/employees/byIdsQuery", ([FromQuery(Name = "id")] int[] ids) =>
{
	return EmployeeRequestHandler.HandleGetEmployeesByIds(ids);
});

// Bind arrays to header parameters
// e.g. /employees/byIdsHeader (also specify header with one or more id key/value pairs in postman (e.g. id = 1 / id = 2)
app.MapGet("/employees/byIdsHeader", ([FromHeader(Name = "id")] int[] ids) =>
{
	return EmployeeRequestHandler.HandleGetEmployeesByIds(ids);
});

// Custom binding with BuildAsync method
// e.g. /persons?id=1 (also specify header with key/value pair "name = basil"
app.MapGet("persons", (Person? person) =>
{
	return $"Id is {person?.Id}; Name is {person?.Name}";
});

app.MapGet("/", () =>
{
	return "Hello World";
});

// Bind to HTTP body
// e.g. /employees (also specify body in postman like 
// {
//    "Id": 4,
//    "Name": "Roberto Blanko",
//    "Position": "Alleinunterhalter",
//    "Salary": 100
// }
app.MapPost("/employees", (Employee employee) =>
{
	return EmployeeRequestHandler.HandlePost(employee);
});

app.MapPut("/employees", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandlePut(context);
});

app.MapDelete("/employees/{id}", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandleDelete(context);
});

app.Run();