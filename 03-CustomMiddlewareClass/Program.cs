using _03_CustomMiddlewareClass.MiddleComponents;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<MyCustomExceptionHandler>();

WebApplication app = builder.Build();

app.UseMiddleware<MyCustomExceptionHandler>();

// Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
	context.Response.Headers["MyHeader"] = "My header content";
	await context.Response.WriteAsync("Middleware #1: Before calling next\r\n");

	await next(context);

	await context.Response.WriteAsync("Middleware #1: After calling next\r\n");
});

// Middlware #5 and #6
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
	Random random = new Random();
	int randomNumber = random.Next(1,3);

	if(randomNumber == 2)
	{
		throw new ApplicationException("Exception for testing.");
	}

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
