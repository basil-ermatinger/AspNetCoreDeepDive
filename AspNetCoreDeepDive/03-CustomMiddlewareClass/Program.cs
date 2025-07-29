using _03_CustomMiddlewareClass.MiddleComponents;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();

WebApplication app = builder.Build();

// Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
	context.Response.Headers["MyHeader"] = "My header content";
	await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");

	await next(context);

	await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
});

app.Map("/map", (appBuilder) =>
{
	// Middleware #5
	appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
	{
		await context.Response.WriteAsync("Middleware #5: Before calling next\r\n");

		await next(context);

		await context.Response.WriteAsync("Middleware #5: After calling next\r\n");
	});

	// Middleware #6
	appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
	{
		await context.Response.WriteAsync("Middleware #6: Before calling next\r\n");

		await next(context);

		await context.Response.WriteAsync("Middleware #6: After calling next\r\n");
	});
});

// My custom middleware
app.UseMiddleware<MyCustomMiddleware>();

// Middleware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
	await context.Response.WriteAsync("Middleware #2: Before calling next\r\n");

	await next(context);

	await context.Response.WriteAsync("Middleware #2: After calling next\r\n");
});

// Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
	await context.Response.WriteAsync("Middleware #3: Before calling next\r\n");

	await next(context);

	await context.Response.WriteAsync("Middleware #3: After calling next\r\n");
});

app.Run();
