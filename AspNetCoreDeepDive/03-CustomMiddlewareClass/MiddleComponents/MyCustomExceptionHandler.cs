
namespace _03_CustomMiddlewareClass.MiddleComponents
{
	public class MyCustomExceptionHandler : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				context.Response.ContentType = "text/html";
				await next(context);
			}
			catch (Exception ex) 
			{
				await context.Response.WriteAsync("<h2>Error:</h2>");
				await context.Response.WriteAsync("<h3>Message</h3>");
				await context.Response.WriteAsync($"<p>{ex.Message}</p>");
				await context.Response.WriteAsync("<h3>Data</h3>");
				await context.Response.WriteAsync($"<p>{ex.Data}</p>");
				await context.Response.WriteAsync("<h3>StackTrace</h3>");
				await context.Response.WriteAsync($"<p>{ex.StackTrace}</p>");
			}
		}
	}
}
