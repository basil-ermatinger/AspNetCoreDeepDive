var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
	await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");

	await next(context);

	await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
});

app.Map("/employees", (appBuilder) =>
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

app.MapWhen((context) =>
	{
		return context.Request.Path.StartsWithSegments("/employee")
			&& context.Request.Query.ContainsKey("id");
	},
	(appBuilder) =>
	{
		// Middleware #7
		appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
		{
			await context.Response.WriteAsync("Middleware #7: Before calling next\r\n");

			await next(context);

			await context.Response.WriteAsync("Middleware #7: After calling next\r\n");
		});

		// Middleware #8
		appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
		{
			await context.Response.WriteAsync("Middleware #8: Before calling next\r\n");

			await next(context);

			await context.Response.WriteAsync("Middleware #8: After calling next\r\n");
		});
	}
);

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
