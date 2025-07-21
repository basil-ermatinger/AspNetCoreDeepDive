using Microsoft.Extensions.Primitives;
using System.Text.Json;
using WebApp.Modules.Employees.Handlers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
	if(context.Request.Path.StartsWithSegments("/"))
	{
		context.Response.Headers["Content-Type"] = "text/html";

		await context.Response.WriteAsync($"The method is: {context.Request.Method}<br />");
		await context.Response.WriteAsync($"The Url is: {context.Request.Path}<br />");

		await context.Response.WriteAsync($"<b>Headers</b>:<br />");

		await context.Response.WriteAsync("<ul>");
		foreach(var key in context.Request.Headers.Keys)
		{
			await context.Response.WriteAsync(
				$"<li><b>{key}</b>: {context.Request.Headers[key]}</li>");
		}
		await context.Response.WriteAsync("</ul>");
	}
	else if(context.Request.Path.StartsWithSegments("/employees"))
	{
		await EmployeeRequestHandler.HandleAsync(context);
	}
	else if(context.Request.Path.StartsWithSegments("/redirection"))
	{
		context.Response.Redirect("/employees");
	}
	else
	{
		context.Response.StatusCode = 404;
	}
});

app.Run();
