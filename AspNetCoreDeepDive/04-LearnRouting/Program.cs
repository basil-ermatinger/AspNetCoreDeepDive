var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/employees", async (HttpContext context) =>
{
	await context.Response.WriteAsync("Get employees");
});

app.MapPost("/employees", async (HttpContext context) =>
{
	await context.Response.WriteAsync("Create an employee");
});

app.MapPut("/employees", async (HttpContext context) =>
{
	await context.Response.WriteAsync("Update an employee");
});

app.MapDelete("/employees", async (HttpContext context) =>
{
	await context.Response.WriteAsync("Delete an employee");
});

app.Run();
