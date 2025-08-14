using _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Handlers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.MapPost("/employees", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandlePost(context);
});

// e.g. http://localhost:5074/employees/1
app.MapGet("/employees/{id:int?}", ([FromRoute] int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetEmployeeById(id.Value) : EmployeeRequestHandler.HandleGetEmployees(1);
});

// e.g. http://localhost:5074/employees/position/1
app.MapGet("/employees/position/{id}", ([FromRoute(Name = "id")] int? identityNumber) =>
{
	return identityNumber.HasValue ? EmployeeRequestHandler.HandleGetPositionById(identityNumber.Value) : null;
});

// e.g. http://localhost:5074/employees/salary/1
app.MapGet("/employees/salary/{id}", (int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetSalaryById(id.Value) : null;
});

// e.g. http://localhost:5074/employees/name?id=1
app.MapGet("/employees/name", (int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetNameById(id.Value) : null;
});

// e.g. http://localhost:5074/employees/helloFromEmployee?id=1
app.MapGet("/employees/helloFromEmployee", ([FromQuery(Name = "id")] int? identityNumber) =>
{
	return identityNumber.HasValue ? EmployeeRequestHandler.HandleHelloFromEmployee(identityNumber.Value) : null;
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