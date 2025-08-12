using _05_MinimalApi_ModelBindingModelValidation.Modules.Employees.Handlers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.MapPost("/employees", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandlePost(context);
});

//app.MapGet("/employees", async (HttpContext context) =>
//{
//	await EmployeeRequestHandler.HandleGetEmployees(context);
//});

app.MapGet("/employees/{id:int?}", async ([FromRoute] int? id) =>
{
	return id.HasValue ? EmployeeRequestHandler.HandleGetEmployeeById(id.Value) : null;
});

app.MapGet("/employees/position/{id}", async ([FromRoute (Name = "id")] int identityNumber) =>
{
	return EmployeeRequestHandler.HandleGetPositionById(identityNumber);
});

app.MapGet("/employees/salary/{id}", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandleGetSalaryById(context);
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