var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Just for debugging
app.Use(async (context, next) =>
{
	await next(context); // context.Endpoint is null
});

app.UseRouting();

// Just for debugging
app.Use(async (context, next) =>
{
	await next(context); // context.Endpoint is set
});

app.UseEndpoints(endpoints =>
{
	endpoints.MapGet("/employees", async (HttpContext context) =>
	{
		await context.Response.WriteAsync("Get employees");
	});

	endpoints.MapGet("/employees/{id:int:range(1,5)}", async (HttpContext context) =>
	{
		await context.Response.WriteAsync($"Get employee by id smaller than 6: {context.Request.RouteValues["id"]}");
	});

	endpoints.MapGet("/employees/{name:alpha:minlength(1):maxlength(4)}", async (HttpContext context) =>
	{
		await context.Response.WriteAsync($"Get employee by string with 1 to 4 chars: {context.Request.RouteValues["name"]}");
	});

	endpoints.MapGet("/employees/{name:minlength(5)}", async (HttpContext context) =>
	{
		await context.Response.WriteAsync($"Get employee by string with 5 or more chars: {context.Request.RouteValues["name"]}");
	});

	endpoints.MapPost("/employees", async (HttpContext context) =>
	{
		await context.Response.WriteAsync("Create an employee");
	});

	endpoints.MapPut("/employees", async (HttpContext context) =>
	{
		await context.Response.WriteAsync("Update an employee");
	});

	endpoints.MapDelete("/employees/{position}/{id}", async (HttpContext context) =>
	{
		await context.Response.WriteAsync($"Delete the employee {context.Request.RouteValues["id"]} width position {context.Request.RouteValues["position"]}");
	});

	endpoints.MapGet("/optional/{isOptional=false}/{id?}", async (HttpContext context) =>
	{
		string isOptional = context.Request.RouteValues["isOptional"].ToString() == "false" ? "optional" : "not optional";
		string id = context.Request.RouteValues["id"]?.ToString() ?? "not set";

		await context.Response.WriteAsync($"This request is {isOptional} and the id is {id}!");
	});

	endpoints.MapGet("/{category=shirts}/{size=medium}/{id=0}", async (HttpContext context) =>
	{
		await context.Response.WriteAsync($"Get {context.Request.RouteValues["category"]} in size: {context.Request.RouteValues["size"]} and id is {context.Request.RouteValues["id"]}");
	});
});

app.Run();
