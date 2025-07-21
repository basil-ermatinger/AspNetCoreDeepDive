using Microsoft.Extensions.Primitives;
using System.Text.Json;
using WebApp.Modules.Employees.Models;
using WebApp.Modules.Employees.Repositories;

namespace WebApp.Modules.Employees.Handlers
{
	public static class EmployeeRequestHandler
	{
		public static async Task HandleAsync(HttpContext context)
		{
			switch(context.Request.Method)
			{
				case "GET":
					await HandleGet(context);
					break;
				case "POST":
					await HandlePost(context);
					break;
				case "PUT":
					await HandlePut(context);
					break;
				case "DELETE":
					await HandleDelete(context);
					break;
				default:
					context.Response.StatusCode = 405;
					break;
			}
		}

		public static async Task HandleGet(HttpContext context)
		{
			context.Response.Headers["Content-Type"] = "text/html";

			if(context.Request.Query.ContainsKey("id"))
			{
				StringValues id = context.Request.Query["id"];

				if(int.TryParse(id, out int employeeId))
				{
					Employee employee = EmployeeRepository.GetEmployee(employeeId);					

					if(employee is not null)
					{
						await context.Response.WriteAsync($"Name: {employee.Name}<br/>");
						await context.Response.WriteAsync($"Position: {employee.Position}<br/>");
						await context.Response.WriteAsync($"Salary: {employee.Salary}");
					}
					else
					{
						context.Response.StatusCode = 404;
						await context.Response.WriteAsync("Employee not found.");
					}
				}
			}
			else
			{
				List<Employee> employees = EmployeeRepository.GetEmployees();

				context.Response.StatusCode = 200;

				await context.Response.WriteAsync("<ul>");

				foreach(Employee employee in employees)
				{
					await context.Response.WriteAsync(
						$"<li><b>{employee.Name}</b>: {employee.Position}</li>");
				}

				await context.Response.WriteAsync("</ul>");
			}
		}

		public static async Task HandlePost(HttpContext context)
		{
			using StreamReader reader = new StreamReader(context.Request.Body);
			string body = await reader.ReadToEndAsync();

			try
			{
				Employee employee = JsonSerializer.Deserialize<Employee>(body)!;

				if(employee is null || employee.Id <= 0)
				{
					context.Response.StatusCode = 400;
					return;
				}

				EmployeeRepository.AddEmployee(employee);

				context.Response.StatusCode = 201;
				await context.Response.WriteAsync("Employee added successfully");
			}
			catch(Exception ex)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(ex.ToString());
				return;
			}
		}

		public static async Task HandlePut(HttpContext context)
		{
			using StreamReader reader = new StreamReader(context.Request.Body);
			string body = await reader.ReadToEndAsync();
			Employee employee = JsonSerializer.Deserialize<Employee>(body)!;

			bool result = EmployeeRepository.UpdateEmployee(employee);

			if(result)
			{
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync("Employee updated successfully!");
				return;
			}
			else
			{
				await context.Response.WriteAsync("Employee not found");
			}
		}

		public static async Task HandleDelete(HttpContext context)
		{
			if(context.Request.Query.ContainsKey("id"))
			{
				StringValues id = context.Request.Query["id"];

				if(int.TryParse(id, out int employeeId))
				{
					if(context.Request.Headers["Authorization"] == "frank")
					{
						bool result = EmployeeRepository.DeleteEmployee(employeeId);

						if(result)
						{

							await context.Response.WriteAsync("Employee is deleted successfully.");
						}
						else
						{
							context.Response.StatusCode = 404;
							await context.Response.WriteAsync("Employee not found.");
						}
					}
					else
					{
						context.Response.StatusCode = 401;
						await context.Response.WriteAsync("You are not authorized to delete.");
					}
				}
			}
		}
	}
}
