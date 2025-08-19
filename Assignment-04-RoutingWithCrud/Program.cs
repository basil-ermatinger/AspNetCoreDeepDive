
using Assignment_04_RoutingWithCrud.Modules.Employees.Handlers;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.MapPost("/employees", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandlePost(context);
});

app.MapGet("/employees", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandleGetEmployees(context);
});

app.MapGet("/employees/{id:int}", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandleGetEmployeeById(context);
});

app.MapGet("/employees/position/{id}", async (HttpContext context) =>
{
	await EmployeeRequestHandler.HandleGetPositionById(context);
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