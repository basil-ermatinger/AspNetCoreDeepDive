using System.Text;

namespace _07_CodeOrganizationAndDependencyInjection.Results
{
	public class HtmlResult : IResult
	{
		private readonly string _html;

		public HtmlResult(string html)
		{
			_html = html;
		}

		public async Task ExecuteAsync(HttpContext httpContext)
		{
			httpContext.Response.ContentType = "text/html";
			httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);

			await httpContext.Response.WriteAsync(_html);
		}

	}
}
